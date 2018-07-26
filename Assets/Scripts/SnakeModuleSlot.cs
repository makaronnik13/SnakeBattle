using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeModuleSlot : MonoBehaviour {

    public Image ModuleImage;
    private LogicModules __module;
    public LogicModules Module
    {
        get
        {
            return __module;
        }
        set
        {
            __module = value;
            if (__module==null)
            {
                ModuleImage.color = new Color(1,1,1,0);
            }
            else
            {
                ModuleImage.sprite = DefaultResources.GetModuleSprite(__module);
                ModuleImage.color = new Color(1, 1, 1, 1);
            }
        }
    }

	public void SetSlot(LogicModules module)
    {
        Module = module;
    }

    public void Push()
    {
        GetComponentInParent<SnakeEditor>().SetModule(this);
    }
}
