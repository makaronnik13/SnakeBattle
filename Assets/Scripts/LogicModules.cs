using UnityEngine;

[System.Serializable]
public class LogicModules
{
    public int size;
    public LogicElement[,] elements;

    public LogicModules(int size)
    {
        this.size = size;
        elements = new LogicElement[size,size];
    }
}