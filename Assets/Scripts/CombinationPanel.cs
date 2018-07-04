using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class CombinationPanel : MonoBehaviour {

    private LogicModules _editingModule;

    private LogicModules ChoosedSubmodule;

	public void Init(LogicModules module)
    {
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(false);
        }

        int templateIndex = GetTemplateIndex(module.CombinationString);

        GameObject template = transform.GetChild(templateIndex).gameObject;
        template.SetActive(true);
        template.GetComponent<CombinationTemplatePanel>().Init(module);
    }

    public void SlotClicked(int v)
    {
        _editingModule.Submodules[v] = ChoosedSubmodule;
    }

    private int GetTemplateIndex(string combinationString)
    {
        Regex rgx = new Regex("[^a-zA-Z0-9 -]");
        string alphCombinationString = rgx.Replace(combinationString, "");

        int numberOfVariables = alphCombinationString.Length;

        switch (numberOfVariables)
        {
            case 1:
                return 0;
            case 2:
                return 1;
            case 3:
                if (Regex.Matches(combinationString, "(").Count==0)
                {
                    return 2;
                }
                return 3;
        }

        return 0;
    }

    public void Hide()
    {
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(false);
        }
    }
}
