using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class DefaultResources
{
    private static List<string> _randomNames;
    public static string RandomName()
    {
        if (_randomNames == null)
        {
            _randomNames = new List<string>();

            string path = "Assets/Resources/DefaultResources/Names.txt";
            //Read the text from directly from the test.txt file
            StreamReader reader = new StreamReader(path);
            string namesString = reader.ReadToEnd();
            reader.Close();
            _randomNames = namesString.Split('\n').ToList();
        }

        return _randomNames[UnityEngine.Random.Range(0, _randomNames.Count - 1)];
    }

    private static List<SnakeSkin> __baseSkins;
    public static List<SnakeSkin> BaseSkins
    {
        get
        {
            if (__baseSkins == null)
            {
                __baseSkins = Skins.Where(s=>s.Base).ToList();
            }

            return __baseSkins;
        }
    }

    public static SnakeSkin RandomSkin()
    {
        return BaseSkins[UnityEngine.Random.Range(0, BaseSkins.Count-1)];
    }

    public static LogicElement GetElementByEnum(LogicElement.LogicElementType elType)
    {
        return Elements.FirstOrDefault(e=>e.ElementType == elType);
    }


    public static Sprite GetModuleSprite(LogicModules lm)
    {
        //compare modules by size
        return Modules.FirstOrDefault(l=>l.Size == lm.Size).Img;
    }

    private static List<LogicElement> _elements;
    public static List<LogicElement> Elements
    {
        get
        {
            if (_elements == null)
            {
                _elements = Resources.LoadAll<LogicElement>("DefaultResources/Elements").ToList();
            }
            return _elements;
        }
    }


    private static List<ShopBonus> _bonuses;
    public static List<ShopBonus> Bonuses
    {
        get
        {
            if (_bonuses == null)
            {
                _bonuses = Resources.LoadAll<ShopBonus>("DefaultResources/Bonuses").ToList();
            }
            return _bonuses;
        }
    }

    private static List<ModuleHolder> _modules;
    public static List<ModuleHolder> Modules
    {
        get
        {
            if (_modules == null)
            {
                _modules = Resources.LoadAll<ModuleHolder>("DefaultResources/Modules").ToList();
            }
            return _modules;
        }
    }

    private static List<SnakeSkin> _skins;
    public static List<SnakeSkin> Skins
    {
        get
        {
            if (_skins == null)
            {
                _skins = Resources.LoadAll<SnakeSkin>("DefaultResources/Skins").ToList();
            }
            return _skins;
        }
    }
}
