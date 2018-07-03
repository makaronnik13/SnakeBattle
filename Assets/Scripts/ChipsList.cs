using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipsList : MonoBehaviour {

    public GameObject ModuleVisual;

	public void UpdateList(List<LogicModules> modules)
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        foreach (LogicModules lm in modules)
        {
            GameObject newModule = Instantiate(ModuleVisual, Vector3.zero, Quaternion.identity, transform);
            newModule.transform.localScale = Vector3.one;
            newModule.GetComponent<ModuleVisual>().Init(lm);
        }
    }
}
