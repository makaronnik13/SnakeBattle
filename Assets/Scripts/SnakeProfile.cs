
using System;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public class SnakeProfile
{
    private string _nickName = null;
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
            return _skin;
        }
        set
        {
            _skin = value;
            OnSkinShanged(_skin);
        }
    }

    public int Length
    {
        get
        {
            return ModulesSlots + 3;
        }
    }

    private SnakeSkin _skin;

    public LogicModules[] Modules;

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
                if (i<Modules.Length)
                {
                    lm = Modules[i];
                }
                newModules.Add(lm);
            }
            Modules = newModules.ToArray();
        }
    }

    public SnakeProfile()
    {
        Modules = new LogicModules[ModulesSlots];
        _skin = DefaultResources.RandomSkin();
        _nickName = DefaultResources.RandomName();
    }

}