using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SimpleModule : LogicModules
{
    [SerializeField]
    private int[,] _elements;

    public int[,] Elements
    {
        get
        {
            return _elements;
        }
    }

    [NonSerialized]
    public Action<LogicElement.LogicElementType, Vector2> OnElementSeted = (LogicElementType, Vector2) => { };

    public SimpleModule()
    {
    }

    public SimpleModule(ModuleHolder mholder)
    {
        Init(mholder);
        _elements = new int[Size, Size];
        int center = Mathf.FloorToInt(Size / 2f);
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if (i == center && j == center)
                {
                    //place head
                    _elements[i, j] = (int)LogicElement.LogicElementType.MyHead;
                }
                else
                {
                    //place empty
                    _elements[i, j] = (int)LogicElement.LogicElementType.Any;
                }
            }
        }
    }

    public void SetElement(Vector2 pos, LogicElement.LogicElementType type)
    {
        _elements[(int)pos.x, (int)pos.y] = (int)type;
        OnElementSeted(type, pos);
    }
}
