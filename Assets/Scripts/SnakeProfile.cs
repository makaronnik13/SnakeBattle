
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class SnakeProfile
{
    [SerializeField]
    private string _nickName = null;
    [SerializeField]
    private string _skinName;
    [SerializeField]
    private LogicModules[] _modules;

    public string NickName
    {
        get
        {
            return _nickName;
        }
        set
        {
            _nickName = value;
        }
    }

    public Action<SnakeSkin> OnSkinShanged = (ss)=> { };

    public SnakeSkin Skin
    {
        get
        {
            return DefaultResources.Skins.FirstOrDefault(s=>s.SkinName == _skinName);
        }
        set
        {
            _skinName = value.SkinName;
            OnSkinShanged(DefaultResources.Skins.FirstOrDefault(s => s.SkinName == _skinName));
        }
    }

    public int Length
    {
        get
        {
            return ModulesSlots + 3;
        }
    }


    public LogicModules[] Modules
    {
        get
        {
            return _modules;
        }
    }
    private int _modulesSlots = 3;
    public int ModulesSlots
    {
        get
        {
            return _modulesSlots;
        }
        set
        {
            _modulesSlots = value;
            List<LogicModules> newModules = new List<LogicModules>();
            for (int i = 0; i<_modulesSlots;i++)
            {
                LogicModules lm = null;
                if (i<_modules.Length)
                {
                    lm = _modules[i];
                }
                newModules.Add(lm);
            }
            _modules = newModules.ToArray();
        }
    }

    public SnakeProfile(string name)
    {
        _modules = new LogicModules[ModulesSlots];
        _skinName = name;
        _nickName = DefaultResources.RandomName();
    }

    public SnakeProfile()
    {

    }
}