using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SnakeEditor : MonoBehaviour
{
    private List<float> _bodyRotations = new List<float>()
    {
        90f,
        90f,
        90f,
        -90f,
        0f,
        -90f,
        -90,
        -90f,
        -90f
    };
    private List<float> _tailRotations = new List<float>()
    {
        180f,
        90f,
        90f,
        90f,
        -180f,
        -90f,
        -90f,
        -90f,
        -90f
    };


    public Button CreateButton, AddSlotButton;
    public Transform SnakeView;
    public Button Left, Right, Create;
    public InputField SnakeName;

    public ChipsList Modules;
    public SkinsList Skins;

    public SelectedModuleCounter counter;

    private LogicModules __selectedModule;
    private LogicModules _selectedModule
    {
        get
        {
            return __selectedModule;
        }
        set
        {
            __selectedModule = value;
            counter.SelectedModule = __selectedModule;
        }
    }
		
    private SnakeProfile CurrentSnake
    {
        get
        {
			return Player.Instance.SelectedSnake;
        }
        set
        {
			Player.Instance.SelectedSnake = value;
            UpdateAddSlotButton();
        }
    }

	private void SnakeUpdated()
    {
		if (Player.Instance.SelectedSnake!=null)
		{
			SnakeName.interactable = true;
			SnakeName.text = Player.Instance.SelectedSnake.NickName;
			Modules.gameObject.SetActive(true);
			Skins.gameObject.SetActive(true);
			Skins.UpdateList(Player.Instance.Skins);
			Modules.UpdateList(Player.Instance.Modules);
			Create.gameObject.SetActive(false);
			Left.gameObject.SetActive(Player.Instance.SelectedSnake!=Player.Instance.Snakes[0]);
			Right.gameObject.SetActive(Player.Instance.SelectedSnake!= Player.Instance.Snakes[Player.Instance.Snakes.Count-1]);
		}
		else
		{
			SnakeName.interactable = false;
			Modules.gameObject.SetActive(false);
			Skins.gameObject.SetActive(false);
			Create.gameObject.SetActive(true);
			Left.gameObject.SetActive(false);
			Right.gameObject.SetActive(false);
		}

        UpdateSkins();
        UpdateSnakeView();
        UpdateAddSlotButton();
    }

    public void SetSelectedModule(LogicModules module)
    {
        _selectedModule = module;
    }

    private void UpdateSnakeView()
    {
        foreach (Transform t in SnakeView)
        {
            t.gameObject.SetActive(false);
        }

        if (CurrentSnake!=null)
        {
            SnakeView.transform.GetChild(0).gameObject.SetActive(true);
            SnakeView.transform.GetChild(0).GetComponent<Image>().sprite = CurrentSnake.Skin.Head;

            for (int i = 1; i<CurrentSnake.ModulesSlots+2;i++)
            {
                SnakeView.transform.GetChild(i).gameObject.SetActive(true);
                if (i == CurrentSnake.ModulesSlots+1)
                {
                    SnakeView.transform.GetChild(i).localRotation = Quaternion.Euler(0, 0, _tailRotations[i-4]);
                    SnakeView.transform.GetChild(i).GetComponent<Image>().sprite = CurrentSnake.Skin.Tail;
                    SnakeView.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    if (i>3)
                    {
                        SnakeView.transform.GetChild(i).localRotation = Quaternion.Euler(0, 0, _bodyRotations[i - 4]);
                    }
                    if (i%4 == 1 || i%4 == 2)
                    {
                        SnakeView.transform.GetChild(i).GetComponent<Image>().sprite = CurrentSnake.Skin.Body;
                    }
                    else
                    {
                        SnakeView.transform.GetChild(i).GetComponent<Image>().sprite = CurrentSnake.Skin.Angle;
                    }
                    SnakeView.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                }

            }
        }


        int j = 0;
        foreach (SnakeModuleSlot s in SnakeView.GetComponentsInChildren<SnakeModuleSlot>())
        {
            LogicModules lm = null;
            if (CurrentSnake!=null && j<CurrentSnake.ModulesSlots)
            {
                lm = CurrentSnake.Modules[j];
            }
            s.SetSlot(lm);
            j++;
        }
        UpdateModules();
    }

    public void OpenEditor()
    {
		Player.Instance.OnSnakeChanged += SnakeUpdated;
        transform.GetChild(0).gameObject.SetActive(true);
        if (Player.Instance.Snakes.Count==0)
        {
            CurrentSnake = null;
        }
        else
        {
            CurrentSnake = Player.Instance.Snakes[0];
            SnakeUpdated();
        }
        UpdateModules();

        Player.Instance.OnMoneyChanged += MoneyChanged;
        UpdateNewSnakeButton();
        UpdateAddSlotButton();
    }

    private void Start()
    {
        UpdateAddSlotButton();
    }

    public void AddSlot()
    {
        Player.Instance.Money-= DefaultResources.GetSlotCost(CurrentSnake.ModulesSlots);
        CurrentSnake.ModulesSlots++;
        UpdateSnakeView();
    }

    private void MoneyChanged()
    {
        UpdateAddSlotButton();
    }

    private void UpdateAddSlotButton()
    {
        AddSlotButton.gameObject.SetActive(CurrentSnake!=null && CurrentSnake.ModulesSlots!=9);
        if (AddSlotButton.isActiveAndEnabled)
        {
            AddSlotButton.transform.GetChild(3).GetComponent<Text>().text = "" + DefaultResources.GetSlotCost(CurrentSnake.ModulesSlots);
        }       
    }

    private void UpdateNewSnakeButton()
    {
        CreateButton.gameObject.SetActive(Player.Instance.Snakes.Count<5);
        if (CreateButton.isActiveAndEnabled)
        {
            CreateButton.transform.GetChild(2).GetComponent<Text>().text = "" + DefaultResources.GetSnakeCost(Player.Instance.Snakes.Count);
        }
    }

    public void CloseEditor()
    {
		Player.Instance.OnSnakeChanged -= SnakeUpdated;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void ChooseSkin(SnakeSkin skin)
    {
		if(CurrentSnake == null)
		{
			return;
		}

		if (!CurrentSnake.Skin.Base)
		{
			Player.Instance.AddSkin(CurrentSnake.Skin);
		}

        CurrentSnake.Skin = skin;
        if (!skin.Base)
        {
            Player.Instance.RemoveSkin(skin);
        }
       
		SnakeUpdated ();
    }
		

    private void UpdateSkins()
    {
        Skins.UpdateList(Player.Instance.Skins);
    }

    private void UpdateModules()
    {
        Modules.UpdateList(Player.Instance.Modules);
    }

    public void DeleteSnake()
    {
        Player.Instance.DeleteSnake(CurrentSnake);
    }

    public void CreateSnake()
    {
		Player.Instance.CreateSnake();
        UpdateNewSnakeButton();
    }

    public void SetModule(SnakeModuleSlot slot)
    {
        if (slot.Module != null)
        {
            Player.Instance.Modules.Add(slot.Module);
            Modules.UpdateList(Player.Instance.Modules);
            CurrentSnake.Modules[slot.transform.parent.GetSiblingIndex() - 1] = null;
            slot.SetSlot(null);
        }


        if (_selectedModule!=null)
        {
            Player.Instance.Modules.Remove(_selectedModule);
            slot.SetSlot(_selectedModule);
            CurrentSnake.Modules[slot.transform.parent.GetSiblingIndex()-1] = _selectedModule;
            _selectedModule = null;
            Modules.UpdateList(Player.Instance.Modules);
        }

        
    }

	public void ChangeSnake(int step)
	{
		Player.Instance.ChangeSnake (step);
	}

    public void SetName()
    {
        if (CurrentSnake!=null)
        {
            CurrentSnake.NickName = SnakeName.text;
        }
    }

}
