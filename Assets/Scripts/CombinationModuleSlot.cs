using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombinationModuleSlot : MonoBehaviour {

    public void Init(LogicModules module, bool flag)
    {
        if (flag)
        {
            GetComponent<Image>().color = Color.green;
        }
        else
        {
            GetComponent<Image>().color = Color.red;
        }
    }

    public void Click()
    {
        GetComponentInParent<CombinationPanel>().SlotClicked(transform.GetSiblingIndex());
    }
}
