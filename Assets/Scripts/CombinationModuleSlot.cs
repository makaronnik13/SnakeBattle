using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombinationModuleSlot : MonoBehaviour {

    public Image SubmoduleImg;
    public Text SubmoduleName;

    private LogicModules __submodule;
    private LogicModules _submodule
    {
        get
        {
            return __submodule;
        }
        set
        {
            __submodule = value;
            if (__submodule==null)
            {
                SubmoduleImg.enabled = false;
                SubmoduleName.enabled = false;
            }
            else
            {
                SubmoduleImg.enabled = true;
                SubmoduleName.enabled = true;
                SubmoduleImg.sprite = DefaultResources.GetModuleSprite(__submodule);
                SubmoduleName.text = __submodule.ModuleName;
            }
        }
    }

    public void Init(LogicModules module)
    {
        _submodule = module;
    }

    public void Init(LogicModules module, bool flag)
    {
        if (flag)
        {
            GetComponent<Image>().color = Color.green;
        }
        else
        {
            GetComponent<Image>().color = Color.red;
        }

        Init(module);
    }

    public void Click()
    {
        GetComponentInParent<CombinationPanel>().SlotClicked(transform.GetSiblingIndex());
    }
}
