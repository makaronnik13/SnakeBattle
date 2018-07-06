using System;
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


    private List<SnakeProfile> _playerSnakes = new List<SnakeProfile>();
    public List<SnakeProfile> Snakes
    {
        get
        {
            return _playerSnakes;
        }
        set
        {
            _playerSnakes = value;
        }
    }

    public List<SnakeSkin> Skins
    {
        get
        {
            if (_skins==null)
            {
                _skins = DefaultResources.BaseSkins;
            }
            return _skins;
        }
    }
    private List<SnakeSkin> _skins;


    public List<LogicModules> Modules = new List<LogicModules>();
    public Dictionary<LogicElement, int> Elements = new Dictionary<LogicElement, int>();
    public Action OnElementsListChanged = ()=> { };
    public Action OnSkinListChanged = () => { };

    public void AddElements(LogicElement logicElement, int v)
    {
        if (!Elements.ContainsKey(logicElement))
        {
            Elements.Add(logicElement, 0);
        }
        Elements[logicElement] += v;
        OnElementsListChanged();
    }

    public void AddBonus(ShopBonus bonus)
    {
        
    }

    public int GetElementCount(LogicElement element)
    {
        if (element.ElementType == LogicElement.LogicElementType.MyHead)
        {
            return 1;
        }

        if (element.ElementType == LogicElement.LogicElementType.Any)
        {
            return int.MaxValue;
        }
        if (!Elements.ContainsKey(element))
        {
            AddElements(element, 0);
        }

        return Elements[element];
    }

    public void AddSkin(SnakeSkin ss)
    {
        Skins.Add(ss);
        OnSkinListChanged();
    }

    public void RemoveSkin(SnakeSkin ss)
    {
        Skins.Remove(ss);
        OnSkinListChanged();
    }

    internal void DeleteSnake(SnakeProfile currentSnake)
    {
        throw new NotImplementedException();
    }
}
