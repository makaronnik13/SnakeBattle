using System;
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

    public LogicModules _editingModule
    {
            get
            {
                return GetComponentInParent<ModulesEditor>().EditingModule;
            }
    }

	public void Init(LogicModules chip)
    {     
 
        foreach (Transform t in transform)
        {
            t.GetComponent<ModuleSlot>().enabled = false;
            Destroy(t.gameObject);
        }

        _grid.constraintCount = chip.Size;
        _grid.cellSize = Mathf.RoundToInt(_size / chip.Size)*Vector2.one;
        for (int i = 0; i<chip.Size;i++)
        {
            for (int j = 0; j < chip.Size; j++)
            {
                GameObject slotGo = Instantiate(ElementSlot, Vector3.zero, Quaternion.identity, transform);
                slotGo.transform.localScale = Vector3.one;
                slotGo.transform.localPosition = Vector3.zero;
                slotGo.GetComponent<ModuleSlot>().Init(DefaultResources.GetElementByEnum((LogicElement.LogicElementType)chip.Elements[i,j]), new Vector2(i,j));
            }
        }
    }

    
    public void Click(Vector2 position, LogicElement currentElement)
    {
        GetComponentInParent<ModulesEditor>().ElementClicked(_editingModule, position, currentElement);
    }

    public void Hide()
    {
        foreach (Transform t in transform)
        {
            t.GetComponent<ModuleSlot>().enabled = false;
            Destroy(t.gameObject);
        }
    }

    private void OnEnable()
    {
        _size = GetComponent<RectTransform>().rect.height*0.9f;
    }
 

    public List<Vector2> GetEmptySlots()
    {
        List<Vector2> slots = new List<Vector2>();

        for (int i = 0; i < _editingModule.Size; i++)
        {
            for (int j = 0; j < _editingModule.Size; j++)
            {
                if (((LogicElement.LogicElementType)_editingModule.Elements[i,j])== LogicElement.LogicElementType.Any)
                {
                    slots.Add(new Vector2(i,j));
                }
            }
        }
        return slots;
    }

    public Vector2 GetHeadPosition()
    {
        for (int i = 0; i < _editingModule.Size; i++)
        {
            for (int j = 0; j < _editingModule.Size; j++)
            {
                if (((LogicElement.LogicElementType)_editingModule.Elements[i, j]) == LogicElement.LogicElementType.MyHead)
                {
                    return new Vector2(i,j);
                }
            }
        }

        return Vector2.zero;
    }
}
