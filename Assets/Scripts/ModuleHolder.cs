using UnityEngine;

[CreateAssetMenu(menuName = "Snake/Module")]
public class ModuleHolder: ScriptableObject
{
    public enum ModuleType
    {
        Simple,
        Combination
    }

    public ModuleType moduleType;

    public int Size;
    public int Cost;
    public string ModuleHolderName;
    public Sprite Img;

    public string LogicString;
}