using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModuleVisual : MonoBehaviour {

    public Image ModuleImg;
    public Text ModuleName;

    private LogicModules _module;

    public void Init(LogicModules lm)
    {
        _module = lm;
        ModuleName.text = lm.ModuleName;
        ModuleImg.sprite = DefaultResources.GetModuleSprite(lm);
        GetComponent<Button>().onClick.AddListener(EditModule);
    }

    private void EditModule()
    {
        GetComponentInParent<ModulesEditor>().EditModule(_module);
    }
}
