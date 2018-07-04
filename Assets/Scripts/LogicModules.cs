using System;
using UnityEngine;

[System.Serializable]
public class LogicModules
{
    public int size;
    public int[,] elements;
    public string ModuleName;

    public Action<LogicElement.LogicElementType, Vector2> OnElementSeted = (LogicElementType, Vector2) => { }; 

    public LogicModules(int size)
    {
        this.size = size;
        elements = new int[size,size];

        int center =  Mathf.FloorToInt(size / 2f);

        for (int i = 0; i<size;i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (i == center && j == center)
                {
                    //place head
                    elements[i, j] = (int)LogicElement.LogicElementType.MyHead;
                }
                else
                {
                    //place empty
                    elements[i, j] = (int)LogicElement.LogicElementType.Any;
                }
            }
        }
        ModuleName = size + "X" + size;
    }

    public void SetElement(Vector2 pos, LogicElement.LogicElementType type)
    {
        elements[(int)pos.x, (int)pos.y] = (int)type;
        OnElementSeted(type, pos);
    }
}