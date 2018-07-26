using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FieldColumn
{
    [SerializeField]
    private int[] _elements;

    public int[] Elements
    {
        get
        {
            return _elements;
        }
    }

    public FieldColumn()
    {

    }

    public FieldColumn(int[] el)
    {
        _elements = el;
    }
}
