using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChipPanel : MonoBehaviour {

    public GameObject ElementSlot;

    private GridLayoutGroup __grid;
    private GridLayoutGroup _grid
    {
        get
        {
            if (__grid)
            {
                __grid = GetComponent<GridLayoutGroup>();
            }
            return __grid;
        }
    }

	public void Init(LogicModules chip)
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        _grid.constraintCount = chip.size;
        _grid.cellSize = Mathf.RoundToInt(400f / chip.size)*Vector2.one;
        for (int i = 0; i<chip.size;i++)
        {
            for (int j = 0; j < chip.size; j++)
            {
                GameObject slotGo = Instantiate(ElementSlot, Vector3.zero, Quaternion.identity, transform);
                slotGo.transform.localScale = Vector3.one;
                slotGo.GetComponent<ElementVisual>().Init(chip.elements[i,j]);
            }
        }
    }
}
