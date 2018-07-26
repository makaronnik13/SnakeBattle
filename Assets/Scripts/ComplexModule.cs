using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ComplexModule : LogicModules {

    [SerializeField]
    private SimpleModule[] _submodules;

    public LogicModules[] Submodules
    {
        get
        {
            return _submodules;
        }
    }

    [NonSerialized]
    public Action<LogicModules, int> OnSubmoduleSeted = (LogicElementType, Vector2) => { };
    private string _combinationString;
    public string CombinationString
    {
        get
        {
            return _combinationString;
        }
    }

    public void SetSubmodule(int id, SimpleModule submodule)
    {
        _submodules[id] = submodule;
        OnSubmoduleSeted(submodule, id);
    }

    public ComplexModule()
    {
    }

    public ComplexModule(ModuleHolder mholder)
    {
        Init(mholder);
        _combinationString = mholder.LogicString;
        _submodules = new SimpleModule[Size];
    }
}
