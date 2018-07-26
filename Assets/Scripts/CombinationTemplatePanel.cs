using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class CombinationTemplatePanel : MonoBehaviour {

    private List<CombinationModuleSlot> __comboSlots;
    private List<CombinationModuleSlot> _comboSlots
    {
        get
        {
            if (__comboSlots == null)
            {
                __comboSlots = GetComponentsInChildren<CombinationModuleSlot>().ToList();
            }
            return __comboSlots;
        }
    }


    public void Init(ComplexModule module)
    {
        Regex rgx = new Regex("[^a-zA-Z0-9 -]");
        string alphCombinationString = rgx.Replace(module.CombinationString, "");

        List<char> alphaChars = alphCombinationString.OrderBy(c => c).ToList();

        for (int i = 0;i<_comboSlots.Count;i++)
        {
            bool negate = false;
            int alphaIndex = module.CombinationString.IndexOf(alphaChars[i]);
            if (alphaIndex>0)
            {
                if (module.CombinationString[alphaIndex - 1] == '!')
                {
                    negate = true;
                }
            }
            
            _comboSlots[i].Init(module.Submodules[i], !negate);
        }
    }

    public void UpdateSubmodule(LogicModules newModule, int id)
    {
        _comboSlots[id].Init(newModule);
    }
}
