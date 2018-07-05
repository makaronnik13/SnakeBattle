using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinVisual : MonoBehaviour
{
    public Image SkinImg;
    private SnakeSkin _skin;

    public void Init(SnakeSkin lm)
    {    
        _skin = lm;     
        SkinImg.sprite = lm.Head;
        GetComponent<Button>().onClick.AddListener(ChooseSkin);
    }

    private void ChooseSkin()
    {
        GetComponentInParent<SnakeEditor>().ChooseSkin(_skin);
    }
}
