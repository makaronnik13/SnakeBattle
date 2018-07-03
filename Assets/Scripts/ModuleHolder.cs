using UnityEngine;

[CreateAssetMenu(menuName = "Snake/Module")]
public class ModuleHolder: ScriptableObject
{
    public int Size;
    public int Cost;
    public string ModuleHolderName;
    public Sprite Img;
}