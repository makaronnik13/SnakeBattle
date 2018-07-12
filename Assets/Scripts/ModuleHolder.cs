using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Snake/Module")]
public class ModuleHolder: ScriptableObject
{
    public enum ModuleType
    {
        Simple,
        Combination
    }

    public ModuleType moduleType;

    public  enum BaseModuleOperator
    {
        Or,
        And
    }

    public BaseModuleOperator Operator;
    public int Size;
    public int Cost;
    public string ModuleHolderName;
    public Sprite Img;

    public string LogicString;
}