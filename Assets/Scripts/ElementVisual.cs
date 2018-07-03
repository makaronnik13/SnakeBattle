using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementVisual : MonoBehaviour {

    public Text ElementsCounter;
    public Image ElementImage;

    public void Init(LogicElement lm)
    {
        ElementImage.sprite = lm.Img;
        ElementsCounter.text = "0";
    }

    public void Add(int value)
    {
        ElementsCounter.text = int.Parse(ElementsCounter.text)+value+"";
    }
}
