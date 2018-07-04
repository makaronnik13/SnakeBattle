using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementsCounter : MonoBehaviour {

    public Image ElementImage;
    public Text Counter;

    private LogicElement _selectedelement;
    public LogicElement SelectedElement
    {
        get
        {
            return _selectedelement;
        }
        set
        {
            _selectedelement = value;
            if (_selectedelement)
            {
                ElementImage.enabled = true;
                Counter.enabled = true;
                UpdateCounter();
            }
            else
            {
                ElementImage.enabled = false;
                Counter.enabled = false;
            }
        }
    }

    private void UpdateCounter()
    {
        if (!SelectedElement)
        {
            return;
        }
        ElementImage.sprite = SelectedElement.Img;
        int count = Player.Instance.GetElementCount(SelectedElement);
        if (count == int.MaxValue)
        {
            Counter.text = "inf";
        }
        else
        {
            Counter.text = "" + count;
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

    private void Start()
    {
        SelectedElement = null;        
    }
}
