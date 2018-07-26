using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerElement
{
    [SerializeField]
    private int _elementId;
    [SerializeField]
    private int _count;

    public LogicElement.LogicElementType ElementType
    {
        get
        {
            return (LogicElement.LogicElementType)_elementId;
        }
        set
        {
            _elementId = (int)value;
        }
    }

    public int Count
    {
        get
        {
            return _count;
        }
        set
        {
            _count = value;
        }
    }

   // private LogicElement _element;
    public LogicElement Element
    {
        get
        {
            return DefaultResources.GetElementByEnum(ElementType);
        }
    }

    public PlayerElement()
    {

    }

    public PlayerElement(LogicElement.LogicElementType elType, int count)
    {
        _count = count;
        ElementType = elType;
    }
}
