using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChipsList : MonoBehaviour {

    public GameObject ModuleVisual;
    public bool SubmoduleList;

    private RectTransform __contentTransform;
    private RectTransform _contentTransform
    {
        get
        {
            if (!__contentTransform)
            {
                __contentTransform = GetComponent<ScrollRect>().content;
            }
            return __contentTransform;
        }
    }

	public void UpdateList(List<LogicModules> modules)
    {
        foreach (Transform t in _contentTransform)
        {
            Destroy(t.gameObject);
        }

        foreach (LogicModules lm in modules)
        {
            GameObject newModule = Instantiate(ModuleVisual, Vector3.zero, Quaternion.identity, _contentTransform);
            newModule.transform.localScale = Vector3.one;
            newModule.transform.localPosition = Vector3.zero;
            newModule.GetComponent<ModuleVisual>().Init(lm, SubmoduleList);
        }
    }
}
