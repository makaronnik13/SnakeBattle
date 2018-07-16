using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Player : Singleton<Player>
{
    public Action OnMoneyChanged = () => { };
    private int _money = 5000;
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

	public Action OnSnakeChanged = ()=>{};

	private SnakeProfile _selectedSnake;
	public SnakeProfile SelectedSnake
	{
		get
		{
			return _selectedSnake;
		}
		set
		{
			_selectedSnake = value;
			OnSnakeChanged ();
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
				_skins = new List<SnakeSkin>(DefaultResources.BaseSkins);
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

	public void CreateSnake()
	{
		Snakes.Add (new SnakeProfile());
		SelectedSnake = Snakes[Snakes.Count-1];
	}

	public void DeleteSnake(SnakeProfile currentSnake)
    {
		Snakes.Remove (currentSnake);
		if (Snakes.Count > 0) 
		{
			SelectedSnake = Snakes[0];	
		} 
		else 
		{
			SelectedSnake = null;
		}
    }

	public void ChangeSnake(int step)
	{
		SelectedSnake = Snakes[Snakes.IndexOf(SelectedSnake)+step];
	}
}
