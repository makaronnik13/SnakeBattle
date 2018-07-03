
using UnityEngine;

[System.Serializable]
public class InfoStruct
{
    public string Title, Description;
    public Sprite Img;

    public InfoStruct(string title, string description, Sprite img)
    {
        Title = title;
        Description = description;
        Img = img;
    }
}