
using UnityEngine;

[System.Serializable]
public class SnakeProfile
{
    [SerializeField]
    private string _nickName = null;
    public string NickName
    {
        get
        {
            if (_nickName == null)
            {
                _nickName = DefaultResources.RandomName();
            }
            return _nickName;
        }
        set
        {
            _nickName = value;
        }
    }
    public SnakeSkin Skin
    {
        get
        {
            if (!_skin)
            {
                _skin = Resources.Load<SnakeSkin>("DefaultResources/Skin1");
            }
            return _skin;
        }
    }
    private SnakeSkin _skin;
    public LogicModules[] Modules;
    public int ModulesSlots = 3;

    public static SnakeSkin DefaultSnakeSkin;

}