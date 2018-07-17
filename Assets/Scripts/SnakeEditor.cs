using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeEditor : MonoBehaviour
{
    public Button CreateButton, DeleteButton;
    public Transform SnakeView;
    public Button Left, Right, Create;
    public InputField SnakeName;

    public ChipsList Modules;
    public SkinsList Skins;

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
        }
    }

	private void SnakeUpdated()
    {
		if (Player.Instance.SelectedSnake!=null)
		{
			SnakeName.gameObject.SetActive(true);
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
			SnakeName.gameObject.SetActive(false);
			Modules.gameObject.SetActive(false);
			Skins.gameObject.SetActive(false);
			Create.gameObject.SetActive(true);
			Left.gameObject.SetActive(false);
			Right.gameObject.SetActive(false);
		}

        UpdateSkins();
        UpdateSnakeView();
    }

    private void SetSelectedModule(LogicModules module)
    {
        _selectedModule = module;
    }

    private void UpdateSnakeView()
    {
        foreach (Transform t in SnakeView)
        {
            t.gameObject.SetActive(CurrentSnake != null);
        }

        if (CurrentSnake!=null)
        {
            SnakeView.GetChild(0).GetComponent<Image>().sprite = CurrentSnake.Skin.Head;
            SnakeView.GetChild(1).GetComponent<Image>().sprite = CurrentSnake.Skin.Body;
            SnakeView.GetChild(2).GetComponent<Image>().sprite = CurrentSnake.Skin.Angle;
            SnakeView.GetChild(3).GetComponent<Image>().sprite = CurrentSnake.Skin.Angle;
            SnakeView.GetChild(4).GetComponent<Image>().sprite = CurrentSnake.Skin.Body;
            SnakeView.GetChild(5).GetComponent<Image>().sprite = CurrentSnake.Skin.Angle;
            SnakeView.GetChild(6).GetComponent<Image>().sprite = CurrentSnake.Skin.Angle;
            SnakeView.GetChild(7).GetComponent<Image>().sprite = CurrentSnake.Skin.Body;
            SnakeView.GetChild(8).GetComponent<Image>().sprite = CurrentSnake.Skin.Tail;
        }
			
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
			Player.Instance.Skins.Add(CurrentSnake.Skin);
		}

        CurrentSnake.Skin = skin;
        if (!skin.Base)
        {
            Player.Instance.Skins.Remove(skin);
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
    }

    public void SetModule(int id)
    {
        if (_selectedModule!=null)
        {
            //place module in slot
        }

        if (CurrentSnake.Modules[id]!=null)
        {
            //move module to inventory
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
