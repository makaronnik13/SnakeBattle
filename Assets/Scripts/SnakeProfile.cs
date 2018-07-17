
using System;
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

    public int ModulesSlots = 3;

    public SnakeProfile()
    {
        Modules = new LogicModules[ModulesSlots];
        _skin = DefaultResources.RandomSkin();
        _nickName = DefaultResources.RandomName();
    }

}