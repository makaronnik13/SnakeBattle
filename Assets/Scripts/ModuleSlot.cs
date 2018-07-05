using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModuleSlot : MonoBehaviour
{
    public Image ElementImage;
    private Vector2 position;


    private LogicElement __currentElement;
    private LogicElement _currentElement
    {
        get
        {
            return __currentElement;
        }
        set
        {
            __currentElement = value;
            ElementImage.sprite = _currentElement.Img;
        }
    }

    public void Init(LogicElement logicElement, Vector2 pos)
    {
        position = pos;
        _currentElement = logicElement;
    }

    public void Click()
    {
        if (Input.GetMouseButton(0))
        {
            GetComponentInParent<ChipPanel>().Click(position, _currentElement);
        }
        
    }

    public void UpdateElement(LogicElement.LogicElementType elType)
    {
        _currentElement = DefaultResources.GetElementByEnum(elType);
    }

    private void OnEnable()
    {
        GetComponentInParent<ModulesEditor>().EditingModule.OnElementSeted += ElementChanged;
    }

    private void ElementChanged(LogicElement.LogicElementType elType, Vector2 pos)
    {
        int y = transform.GetSiblingIndex() % GetComponentInParent<ModulesEditor>().EditingModule.Size;
        int x = transform.GetSiblingIndex() / GetComponentInParent<ModulesEditor>().EditingModule.Size;
        Vector2 elPos = new Vector2(x,y);
        if (elPos == pos)
        {
            UpdateElement(elType);
        }
    }

    private void OnDisable()
    {
        if (GetComponentInParent<ChipPanel>())
        {
            GetComponentInParent<ModulesEditor>().EditingModule.OnElementSeted -= ElementChanged;
        }
    }
}
