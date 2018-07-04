﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChipPanel : MonoBehaviour {

    public GameObject ElementSlot;

    private float _size;
    private GridLayoutGroup __grid;
    private GridLayoutGroup _grid
    {
        get
        {
            if (!__grid)
            {
                __grid = GetComponent<GridLayoutGroup>();
            }
            return __grid;
        }
    }

    private LogicModules _editingModule;
    public LogicModules EditingModule
    {
        get
        {
            return _editingModule;
        }
    }

	public void Init(LogicModules chip)
    {     
        _editingModule = chip;
 
        foreach (Transform t in transform)
        {
            t.GetComponent<ModuleSlot>().enabled = false;
            Destroy(t.gameObject);
        }

        _grid.constraintCount = chip.size;
        _grid.cellSize = Mathf.RoundToInt(_size / chip.size)*Vector2.one;
        for (int i = 0; i<chip.size;i++)
        {
            for (int j = 0; j < chip.size; j++)
            {
                GameObject slotGo = Instantiate(ElementSlot, Vector3.zero, Quaternion.identity, transform);
                slotGo.transform.localScale = Vector3.one;
                slotGo.transform.localPosition = Vector3.zero;
                slotGo.GetComponent<ModuleSlot>().Init(DefaultResources.GetElementByEnum((LogicElement.LogicElementType)chip.elements[i,j]), new Vector2(i,j));
            }
        }
    }

    
    public void Click(Vector2 position, LogicElement currentElement)
    {
        GetComponentInParent<ModulesEditor>().ElementClicked(_editingModule, position, currentElement);
    }

    private void OnEnable()
    {
        _size = GetComponent<RectTransform>().rect.height*0.9f;
    }

   

 

    public List<Vector2> GetEmptySlots()
    {
        List<Vector2> slots = new List<Vector2>();

        for (int i = 0; i < EditingModule.size; i++)
        {
            for (int j = 0; j < EditingModule.size; j++)
            {
                if (((LogicElement.LogicElementType)EditingModule.elements[i,j])== LogicElement.LogicElementType.Any)
                {
                    slots.Add(new Vector2(i,j));
                }
            }
        }
        return slots;
    }

    public Vector2 GetHeadPosition()
    {
        for (int i = 0; i < EditingModule.size; i++)
        {
            for (int j = 0; j < EditingModule.size; j++)
            {
                if (((LogicElement.LogicElementType)EditingModule.elements[i, j]) == LogicElement.LogicElementType.MyHead)
                {
                    return new Vector2(i,j);
                }
            }
        }

        return Vector2.zero;
    }
}
