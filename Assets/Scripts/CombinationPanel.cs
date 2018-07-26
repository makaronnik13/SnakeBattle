using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class CombinationPanel : MonoBehaviour {

    private ComplexModule _editingModule
    {
        get
        {
            if (GetComponentInParent<ModulesEditor>().EditingModule!=null)
            {
                if (GetComponentInParent<ModulesEditor>().EditingModule.ModuleType == ModuleHolder.ModuleType.Combination)
                {
                    return GetComponentInParent<ModulesEditor>().EditingModule as ComplexModule;
                }
            }
            return null;
        }
    }
    private CombinationTemplatePanel _activeTemplate;
    private LogicModules ChoosedSubmodule;

	public void Init(ComplexModule module)
    {
        if (_editingModule!=null)
        {
            _editingModule.OnSubmoduleSeted -= SubmoduleSeted;
        }
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(false);
        }

        int templateIndex = GetTemplateIndex(module.CombinationString);

        GameObject template = transform.GetChild(templateIndex).gameObject;
        template.SetActive(true);
        _activeTemplate = template.GetComponent<CombinationTemplatePanel>();
        _activeTemplate.Init(module);

        if (_editingModule != null)
        {
            _editingModule.OnSubmoduleSeted += SubmoduleSeted;
        }
    }

    private void SubmoduleSeted(LogicModules newModule, int id)
    {
        _activeTemplate.UpdateSubmodule(newModule, id);
    }

    public void SlotClicked(int v)
    {
        if (_editingModule.Submodules[v]!=null)
        {
            Player.Instance.Modules.Add(_editingModule.Submodules[v]);
            GetComponentInParent<ModulesEditor>().UpdateModulesList();
        }

        _editingModule.SetSubmodule(v, (SimpleModule)GetComponentInParent<ModulesEditor>()._submoduleCounter.SelectedModule);
        GetComponentInParent<ModulesEditor>()._submoduleCounter.SelectedModule = null;
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
                if (combinationString.Count(f=>f == '(')==0)
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
