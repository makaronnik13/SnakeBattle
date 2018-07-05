using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedModuleCounter : MonoBehaviour
{

    public Image ModuletImage;
    public Text ModuleName;

    private LogicModules _selectedModule;
    public LogicModules SelectedModule
    {
        get
        {
            return _selectedModule;
        }
        set
        {
            if (value == _selectedModule)
            {
                value = null;
            }

            _selectedModule = value;
            if (_selectedModule!=null)
            {
                ModuletImage.enabled = true;
                ModuleName.enabled = true;
                UpdateCounter();
            }
            else
            {
                ModuletImage.enabled = false;
                ModuleName.enabled = false;
            }
        }
    }

    private void UpdateCounter()
    {
        if (SelectedModule == null)
        {
            return;
        }
        ModuletImage.sprite = DefaultResources.GetModuleSprite(SelectedModule);
        ModuleName.text = SelectedModule.ModuleName;
      
    }


    private void Start()
    {
        SelectedModule = null;
    }

    public void Cklick()
    {
        GetComponentInParent<ModulesEditor>().SetSubmodule(null);
    }
}
