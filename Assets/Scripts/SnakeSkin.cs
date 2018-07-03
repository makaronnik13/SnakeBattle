using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Snake/skin")]
public class SnakeSkin : ScriptableObject
{
    public string SkinName;
    public int SkinCost;

    public Sprite Head;
    public Sprite Body;
    public Sprite Tail;
    public Sprite Angle;
}
