using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModuleVisual : MonoBehaviour {

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
        if (_isSubmodule)
        {
            GetComponentInParent<ModulesEditor>().SetSubmodule(_module);
        }
        else
        {
            GetComponentInParent<ModulesEditor>().EditModule(_module);
        }
    }
}
