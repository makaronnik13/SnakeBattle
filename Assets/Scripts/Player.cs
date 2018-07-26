using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public  class Player : Singleton<Player>
{

    private PlayerData _playerData = new PlayerData();
    public PlayerData PlayerData
    {
        get
        {
            return _playerData;
        }
        set
        {
            _playerData = value;
        }
    }

    //serialize in separate files
    private List<SnakeProfile> _playerSnakes = new List<SnakeProfile>();
    

    public Action OnMoneyChanged = () => { };

    public List<LogicModules> Modules
    {
        get
        {
            return PlayerData._modules;
        }
        set
        {
            PlayerData._modules = value;
        }
    }

    public int Money
    {
        get
        {
            return PlayerData._money;
        }
        set
        {
            PlayerData._money = value;
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
            if (_selectedSnake!=null)
            {
                PlayerData._selectedSnakeId = Snakes.IndexOf(value);
            }
            else
            {
                PlayerData._selectedSnakeId = -1;
            }
			OnSnakeChanged ();
		}
	}

    
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
            if (PlayerData._skinsIds.Count==0)
            {
                PlayerData._skinsIds = new List<SnakeSkin>(DefaultResources.BaseSkins).Select(s=>s.SkinName).ToList();
            }

            List<SnakeSkin> skins = DefaultResources.Skins.Where(s => PlayerData._skinsIds.Contains(s.SkinName)).ToList();

            return skins;
        }
        set
        {
            PlayerData._skinsIds = new List<string>();
            foreach (SnakeSkin ss in value)
            {
                PlayerData._skinsIds.Add(ss.SkinName);
            }

        }
    }

    public void AddSkin(SnakeSkin ss)
    {
        PlayerData._skinsIds.Add(ss.SkinName);
    }

    public Action OnElementsListChanged = ()=> { };
    public Action OnSkinListChanged = () => { };

    public void AddElements(LogicElement logicElement, int v)
    {
       
        if (PlayerData._elements.FirstOrDefault(le=>le.ElementType == logicElement.ElementType)==null)
        {
            
            PlayerData._elements.Add(new PlayerElement(logicElement.ElementType, 0));
        }
        PlayerData._elements.FirstOrDefault(le => le.ElementType == logicElement.ElementType).Count += v;
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

        if (PlayerData._elements.FirstOrDefault(le => le.ElementType == element.ElementType) == null)
        {
            AddElements(element, 0);
        }


        return PlayerData._elements.FirstOrDefault(le => le.ElementType == element.ElementType).Count;
    }

 

    public void RemoveSkin(SnakeSkin ss)
    {
        PlayerData._skinsIds.Remove(ss.SkinName);
        OnSkinListChanged();
    }

	public void CreateSnake()
	{
		Snakes.Add (new SnakeProfile(DefaultResources.RandomSkin().SkinName));
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
