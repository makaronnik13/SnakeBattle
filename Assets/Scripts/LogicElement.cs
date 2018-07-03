using UnityEngine;

[CreateAssetMenu(menuName = "Snake/Element")]
public class LogicElement:ScriptableObject
{
    public string ElementName;
    public Sprite Img;
    public string Description;
    public int ElementCost;
}