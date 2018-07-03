using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsList : MonoBehaviour {

    public GameObject ElementVisual;
    private Dictionary<LogicElement, ElementVisual> _visuals = new Dictionary<LogicElement, ElementVisual>();

    public void UpdateList(Dictionary<LogicElement, int> elements)
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        _visuals.Clear();

        foreach (LogicElement lm in DefaultResources.Elements)
        {
            GameObject newModule = Instantiate(ElementVisual, Vector3.zero, Quaternion.identity, transform);
            newModule.transform.localScale = Vector3.one;
            newModule.GetComponent<ElementVisual>().Init(lm);
            _visuals.Add(lm, newModule.GetComponent<ElementVisual>());
        }

        foreach (KeyValuePair<LogicElement, int> lm in elements)
        {
            _visuals[lm.Key].Add(lm.Value);
        }
    }
}
