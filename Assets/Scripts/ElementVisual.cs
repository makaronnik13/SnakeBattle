using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementVisual : MonoBehaviour {

    public Text ElementsCounter;
    public Image ElementImage;
    private LogicElement _element;

    public void Init(LogicElement lm)
    {
        _element = lm;
        ElementImage.sprite = lm.Img;
        UpdateCounter();
    }


    public void Click()
    {
        GetComponentInParent<ModulesEditor>().SetSelectedElement(_element);
    }

    private void UpdateCounter()
    {  
        int count = Player.Instance.GetElementCount(_element);
        if (count == int.MaxValue)
        {
            ElementsCounter.text = "inf";
        }
        else
        {
            ElementsCounter.text = "" + count;
        }
    }

    private void OnEnable()
    {
        if (Player.Instance)
        {
            Player.Instance.OnElementsListChanged += UpdateCounter;
        }
    }

    private void OnDisable()
    {
        if (Player.Instance)
        {
            Player.Instance.OnElementsListChanged -= UpdateCounter;
        }
    }
}
