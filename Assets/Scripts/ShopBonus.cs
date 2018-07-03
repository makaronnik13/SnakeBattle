using UnityEngine;

[CreateAssetMenu(menuName = "Snake/ShopBonus")]
public class ShopBonus : ScriptableObject
{
    public string BonusName;
    public int BonusCost;
    public Sprite BonusImg;
}