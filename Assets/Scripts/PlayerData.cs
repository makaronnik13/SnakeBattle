using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class PlayerData
{
    [SerializeField]
    public int _money = 500000;
    [SerializeField]
    public int _selectedSnakeId = -1;
    [SerializeField]
    public List<string> _skinsIds = new List<string>();
    [SerializeField]
    public List<LogicModules> _modules = new List<LogicModules>();
    [SerializeField]
    public List<PlayerElement> _elements = new List<PlayerElement>();
    
}
