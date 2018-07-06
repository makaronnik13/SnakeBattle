using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ModulesEditor : MonoBehaviour {

    public ElementsList Elements;
    public ChipsList Modules;
    public ChipsList Submodules;
    public InputField ModuleName;
    public SelectedModuleCounter _submoduleCounter;  
    public ElementsCounter _elementCounter;
   

    private ChipPanel __chipPanel;
    private ChipPanel _chipPanel
    {
        get
        {
            if (!__chipPanel)
            {
                __chipPanel = GetComponentInChildren<ChipPanel>();
            }
            return __chipPanel;
        }
    }

    public LogicModules EditingModule;

    public void UpdateModulesList()
    {
        Submodules.UpdateList(Player.Instance.Modules.Where(m => m.ModuleType == ModuleHolder.ModuleType.Simple).ToList());
        Modules.UpdateList(Player.Instance.Modules);
    }

    public void SetSubmodule(LogicModules module)
    {
        if (_submoduleCounter.SelectedModule!=null)
        {
            Player.Instance.Modules.Add(_submoduleCounter.SelectedModule);    
        }
        Player.Instance.Modules.Remove(module);

        UpdateModulesList();
        if (_submoduleCounter.SelectedModule == module)
        {
            _submoduleCounter.SelectedModule = null;
        }
        else
        {
            _submoduleCounter.SelectedModule = module;
        }
    }

    private CombinationPanel __combinationPanel;
    private CombinationPanel _combinationPanel
    {
        get
        {
            if (!__combinationPanel)
            {
                __combinationPanel = GetComponentInChildren<CombinationPanel>();
            }
            return __combinationPanel;
        }
    }

    public void Show()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        UpdateModulesList();
    }

    public void Hide()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void EditModule(LogicModules module)
    {
        if (module==EditingModule)
        {
            module = null;
        }

        EditingModule = module;
        ModuleName.gameObject.SetActive(module != null);
        if (module!=null)
        {
            ModuleName.text = module.ModuleName;
        }

        _combinationPanel.Hide();
        Submodules.gameObject.SetActive(false);
        _submoduleCounter.gameObject.SetActive(false);
        _chipPanel.Hide();
        Elements.gameObject.SetActive(false);
        _elementCounter.gameObject.SetActive(false);

        if (module!=null)
        {

            if (module.ModuleType == ModuleHolder.ModuleType.Simple)
            {
                _chipPanel.Init(module);
                Elements.gameObject.SetActive(true);
                _elementCounter.gameObject.SetActive(true);
                Elements.UpdateList(DefaultResources.Elements);
            }
            else
            {
                _combinationPanel.Init(module);
                Submodules.gameObject.SetActive(true);
                _submoduleCounter.gameObject.SetActive(true);
            }
        }

        UpdateModulesList();
    }

    public void ElementClicked(LogicModules editingModule, Vector2 position, LogicElement currentElement)
    {
        if (!_elementCounter.SelectedElement || Player.Instance.GetElementCount(_elementCounter.SelectedElement)==0)
        {
            return;
        }

        if (currentElement.ElementType != LogicElement.LogicElementType.MyHead)
        {
            if (_elementCounter.SelectedElement.ElementType == LogicElement.LogicElementType.MyHead)
            {
                //click on any with head
                RemovePreviousHead();
            }
        }
        else
        {
            if (_elementCounter.SelectedElement.ElementType == LogicElement.LogicElementType.MyHead)
            {
                //click with head on head
                return;
            }
        }

        LogicElement newElement;

        if (_elementCounter.SelectedElement.ElementType == currentElement.ElementType)
        {
            newElement = DefaultResources.GetElementByEnum(LogicElement.LogicElementType.Any);
            Player.Instance.AddElements(currentElement, 1);
        }
        else
        {
            if (Player.Instance.GetElementCount(_elementCounter.SelectedElement) >0)
            {
                Player.Instance.AddElements(_elementCounter.SelectedElement, -1);
                Player.Instance.AddElements(currentElement, 1);
                newElement = _elementCounter.SelectedElement;   
            }
            else
            {
                newElement = currentElement;
                Debug.LogWarning("Not enough elements!");
            }
        }


        editingModule.SetElement(position, newElement.ElementType);
       
        if (currentElement.ElementType == LogicElement.LogicElementType.MyHead)
        {
            PlaceHeadNear(position);
        }
        
    }

    private void RemovePreviousHead()
    {
        Vector2 previousHeadPosition = _chipPanel.GetHeadPosition();
        EditingModule.SetElement(previousHeadPosition, LogicElement.LogicElementType.Any);
    }

    private void PlaceHeadNear(Vector2 pos)
    {
        Vector2 newHeadPos = Vector2.zero;

        List<Vector2> emptySlots = _chipPanel.GetEmptySlots();
        if (emptySlots.Count > 0)
        {
            newHeadPos = emptySlots.OrderBy(s => Vector2.Distance(s, pos)).First();
        }
        else
        {
            List<Vector2> avaliablePositions = new List<Vector2>();
            for (int i = 0; i < EditingModule.Size; i++)
            {
                for (int j = 0; j < EditingModule.Size; j++)
                {
                    if (i != pos.x && j != pos.y)
                    {
                        avaliablePositions.Add(new Vector2(i, j));
                    }

                }
            }
            newHeadPos = avaliablePositions.OrderBy(s => Vector2.Distance(s, pos)).First();
            Player.Instance.AddElements(DefaultResources.GetElementByEnum((LogicElement.LogicElementType)EditingModule.Elements[(int)newHeadPos.x, (int)newHeadPos.y]), 1);
        }
        EditingModule.SetElement(newHeadPos, LogicElement.LogicElementType.MyHead);
    }

    public void SetSelectedElement(LogicElement element)
    {
        _elementCounter.SelectedElement = element;
    }

    private void Start()
    {
        ModuleName.gameObject.SetActive(false);
        ModuleName.onEndEdit.AddListener(NameEdited);
    }

    private void NameEdited(string inputString)
    {
        EditingModule.ModuleName = inputString;
        UpdateModulesList();
    }
}
