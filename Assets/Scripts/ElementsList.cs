using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementsList : MonoBehaviour {

    public GameObject ElementVisual;
    private Dictionary<LogicElement, ElementVisual> _visuals = new Dictionary<LogicElement, ElementVisual>();

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

    public void UpdateList(List<LogicElement> elements)
    {
        Debug.Log("show elements");

        foreach (Transform t in _contentTransform)
        {
            Destroy(t.gameObject);
        }

        _visuals.Clear();

        foreach (LogicElement lm in DefaultResources.Elements)
        {
            GameObject newModule = Instantiate(ElementVisual, Vector3.zero, Quaternion.identity, _contentTransform);
            newModule.transform.localScale = Vector3.one;
            newModule.transform.localPosition = Vector3.zero;
            newModule.GetComponent<ElementVisual>().Init(lm);
            _visuals.Add(lm, newModule.GetComponent<ElementVisual>());
        }
    }
}
