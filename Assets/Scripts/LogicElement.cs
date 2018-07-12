using UnityEngine;

[CreateAssetMenu(menuName = "Snake/Element")]
public class LogicElement:ScriptableObject
{

    public enum LogicElementType
    {
        Any = -1,
        None = 0,
        MyHead = 1,
        MyBody = 2,
        MyTail = 3,
        EnemyHead = 4,
        EnemyBody = 5,
        EnemyTail = 6,
        Wall = 7,
        Fruit = 8,
        Bug = 9,
        Crustling = 10
    }

    public LogicElementType ElementType;
    public string ElementName;
    public Sprite Img;
    public string Description;
    public int ElementCost;
    public bool HideInShop = false;
}