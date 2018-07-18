﻿using System;
using UnityEngine;

[System.Serializable]
public class LogicModules
{

    public ModuleHolder.ModuleType ModuleType;

    public ModuleHolder.BaseModuleOperator Operator;

    public string CombinationString;
    public LogicModules[] Submodules;

    public int Size;
    public int[,] Elements;
    public string ModuleName;

    public Action<LogicElement.LogicElementType, Vector2> OnElementSeted = (LogicElementType, Vector2) => { };
    public Action<LogicModules, int> OnSubmoduleSeted = (LogicElementType, Vector2) => { };
    public string IdName;

    public LogicModules(ModuleHolder mholder)
    {
        IdName = mholder.ModuleHolderName;
        this.Size = mholder.Size;

        ModuleType = mholder.moduleType;

        if (ModuleType == ModuleHolder.ModuleType.Simple)
        {
            Elements = new int[Size, Size];
            int center = Mathf.FloorToInt(Size / 2f);
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (i == center && j == center)
                    {
                        //place head
                        Elements[i, j] = (int)LogicElement.LogicElementType.MyHead;
                    }
                    else
                    {
                        //place empty
                        Elements[i, j] = (int)LogicElement.LogicElementType.Any;
                    }
                }
            }

            Operator = mholder.Operator;
        }
        else
        {
            CombinationString = mholder.LogicString;
            Submodules = new LogicModules[Size];
        }

        ModuleName = mholder.ModuleHolderName;
    }

    public void SetElement(Vector2 pos, LogicElement.LogicElementType type)
    {
        Elements[(int)pos.x, (int)pos.y] = (int)type;
        OnElementSeted(type, pos);
    }

    public void SetSubmodule(int id, LogicModules submodule)
    {
        Submodules[id] = submodule;
        OnSubmoduleSeted(submodule, id);
    }

    public bool CanMoveInThisDirection(Vector2Int head, Vector2Int dir, Board board)
    {
        if (ModuleType == ModuleHolder.ModuleType.Simple)
        {
            LogicElement.LogicElementType[,] boardCells = new LogicElement.LogicElementType[Size, Size];
     
        }

        return false;
    }


}