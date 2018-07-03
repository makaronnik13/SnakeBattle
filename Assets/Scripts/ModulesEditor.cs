using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulesEditor : MonoBehaviour {

	public void Show()
    {
        GetComponentInChildren<ElementsList>().UpdateList(Player.Instance.Elements);
        GetComponentInChildren<ChipsList>().UpdateList(Player.Instance.Modules);
    }
}
