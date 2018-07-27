using System;
using UnityEngine;

[Serializable]
public class LogicModules
{
    [SerializeField]
    private string _moduleName;
    
   
    [SerializeField]
    private string _holderName;

    

    public string HolderName
    {
        get
        {
            return _holderName;
        }
    }

    public string ModuleName
    {
        get
        {
            return _moduleName;
        }
        set
        {
            _moduleName = value;
        }
    }

    public ModuleHolder.ModuleType ModuleType { get; set; }

    private int _size;
    public int Size
    {
        get
        {
            return _size;
        }
    }

   

  

    public LogicModules()
    {
    }

    public void Init(ModuleHolder mholder)
    {
        _holderName = mholder.ModuleHolderName;
        this._size = mholder.Size;

        ModuleType = mholder.moduleType;

        _moduleName = mholder.ModuleHolderName;
    }
   
    public bool CanMoveInThisDirection(Vector2Int head, Vector2Int dir, Board board)
    {
        if (ModuleType == ModuleHolder.ModuleType.Simple)
        {
            LogicElement.LogicElementType[,] boardCells = new LogicElement.LogicElementType[Size, Size];
     
        }

        return false;
    }

    private bool Check

}