﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Player : Singleton<Player>
{
    public Action OnMoneyChanged = () => { };
    private int _money = 1000;
    public int Money
    {
        get
        {
            return _money;
        }
        set
        {
            _money = value;
            OnMoneyChanged();
        }
    }


    public List<SnakeSkin> Skins = new List<SnakeSkin>();
    public List<LogicModules> Modules = new List<LogicModules>();
    public Dictionary<LogicElement, int> Elements = new Dictionary<LogicElement, int>();

    public void AddElements(LogicElement logicElement, int v)
    {
        if (!Elements.ContainsKey(logicElement))
        {
            Elements.Add(logicElement, 0);
        }
        Elements[logicElement] += v;
    }

    public void AddBonus(ShopBonus bonus)
    {
        
    }
}
