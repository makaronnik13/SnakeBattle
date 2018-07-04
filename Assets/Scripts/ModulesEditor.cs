using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModulesEditor : MonoBehaviour {

    private ElementsCounter __elementCounter;
    private ElementsCounter _elementCounter
    {
        get
        {
            if (!__elementCounter)
            {
                __elementCounter = GetComponentInChildren<ElementsCounter>();
            }
            return __elementCounter;
        }
    }

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
        GetComponentInChildren<ElementsList>().UpdateList(DefaultResources.Elements);
        GetComponentInChildren<ChipsList>().UpdateList(Player.Instance.Modules);
    }

    public void Hide()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void EditModule(LogicModules module)
    {
        if (module.ModuleType == ModuleHolder.ModuleType.Simple)
        {
            _chipPanel.Init(module);
            _combinationPanel.Hide();
        }
        else
        {
            _chipPanel.Hide();
            _combinationPanel.Init(module);
        }
        
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
        _chipPanel.EditingModule.SetElement(previousHeadPosition, LogicElement.LogicElementType.Any);
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
            for (int i = 0; i < _chipPanel.EditingModule.Size; i++)
            {
                for (int j = 0; j < _chipPanel.EditingModule.Size; j++)
                {
                    if (i != pos.x && j != pos.y)
                    {
                        avaliablePositions.Add(new Vector2(i, j));
                    }

                }
            }
            newHeadPos = avaliablePositions.OrderBy(s => Vector2.Distance(s, pos)).First();
            Player.Instance.AddElements(DefaultResources.GetElementByEnum((LogicElement.LogicElementType)_chipPanel.EditingModule.Elements[(int)newHeadPos.x, (int)newHeadPos.y]), 1);
        }
        _chipPanel.EditingModule.SetElement(newHeadPos, LogicElement.LogicElementType.MyHead);
    }

    public void SetSelectedElement(LogicElement element)
    {
        _elementCounter.SelectedElement = element;
    }
}
