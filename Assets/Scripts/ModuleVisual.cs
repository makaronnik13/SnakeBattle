using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ModuleVisual : MonoBehaviour
{

    public static GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;

    public Image ModuleImg;
    public Text ModuleName;
    private bool _isSubmodule;
    private LogicModules _module;

    public void Init(LogicModules lm, bool isSubmodule = false)
    {
        _isSubmodule = isSubmodule;
        _module = lm;
        ModuleName.text = lm.ModuleName;
        ModuleImg.sprite = DefaultResources.GetModuleSprite(lm);
        GetComponent<Button>().onClick.AddListener(EditModule);
    }

    private void EditModule()
    {
        if (GetComponentInParent<ModulesEditor>())
        {
            if (_isSubmodule)
            {
                GetComponentInParent<ModulesEditor>().SetSubmodule(_module);
            }
            else
            {
                GetComponentInParent<ModulesEditor>().EditModule(_module);
            }
        }

        if (GetComponentInParent<SnakeEditor>())
        {
            GetComponentInParent<SnakeEditor>().SetSelectedModule(_module);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }
    
    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
     
    }
}
