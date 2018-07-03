using UnityEngine;

[System.Serializable]

public class RandomSprite
{
    public Sprite sprite;
    [Range(0.01f, 1)]
    public float weight;
    
    public static Sprite GetRandomSprite(RandomSprite[] sprites)
    {
        if (sprites.Length == 0)
        {
            return null;
        }

        float max = 0;

        foreach (RandomSprite s in sprites)
        {
            max += s.weight;
        }

        float rand = Random.Range(0, max);

        max = 0;

        foreach (RandomSprite s in sprites)
        {
            max += s.weight;
            if (max>=rand)
            {
                return s.sprite;
            }
        }

        Debug.LogWarning("Wrong random!");
        return null;
    }
}