using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class Tile : MonoBehaviour
{
    /// <summary>
    /// Sprite for empty tile.
    /// </summary>
    public RandomSprite[] Empty;

    public Sprite Bug, Wall, Batery, Crustling;

    /// <summary>
    /// Image component of this GameObject.
    /// </summary>
    private Image image;

    /// <summary>
    /// Holds last displayed image.
    /// </summary>
    private Sprite lastUsedImage;

    private RectTransform _rectTransform;
    private LogicElement.LogicElementType _content;
    private bool _contentHidden;

    /// <summary>
    /// Rect Transform component of this tile.
    /// </summary>
    public RectTransform RectTransform
    {
        get
        {
            return _rectTransform;
        }
    }

    /// <summary>
    /// Contents of this tile.
    /// </summary>
    public LogicElement.LogicElementType Content
    {
        get
        {
            return _content;
        }
    }

    public void SetTile(LogicElement.LogicElementType content, SnakeSkin skin = null)
    {
        _content = content;
        ZRotation = 0;
        switch (_content)
        {
            case LogicElement.LogicElementType.None:
                image.sprite = RandomSprite.GetRandomSprite(Empty);
                break;
            case LogicElement.LogicElementType.Bug:
                image.sprite = Bug;
                break;
            case LogicElement.LogicElementType.Crustling:
                image.sprite = Crustling;
                break;
            case LogicElement.LogicElementType.Batery:
                image.sprite = Batery;
                break;
            case LogicElement.LogicElementType.MyBody:
                if (skin == null)
                {
                    image.sprite = RandomSprite.GetRandomSprite(Empty);
                }
                image.sprite = skin.Body;
                break;
            case LogicElement.LogicElementType.MyHead:
                if (skin == null)
                {
                    image.sprite = RandomSprite.GetRandomSprite(Empty);
                }
                image.sprite = skin.Head;
                break;
            case LogicElement.LogicElementType.MyTail:
                if (skin == null)
                {
                    image.sprite = RandomSprite.GetRandomSprite(Empty);
                }
                image.sprite = skin.Tail;
                break;
            case LogicElement.LogicElementType.Wall:
                image.sprite = Wall;
                break;
        }
        lastUsedImage = image.sprite;
    }

    private float _zRotation = 0;

    /// <summary>
    /// Sets or gets tile's Z-rotation
    /// </summary>
    public float ZRotation
    {
        get
        {
            return _zRotation;
        }
        set
        {
            _zRotation = value;
            transform.rotation = Quaternion.Euler(0, 0, value);
        }
    }

    /// <summary>
    /// If true tile's content will be hidden (as if content was empty). Used for blinking.
    /// </summary>
    public bool ContentHidden
    {
        get
        {
            return _contentHidden;
        }
        set
        {
            _contentHidden = value;
            if (value)
            {
                image.sprite = RandomSprite.GetRandomSprite(Empty); ;
            }
            else
            {
                image.sprite = lastUsedImage;
            }
        }
    }

    // Use this for initialization
    void Awake()
    {
        image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
        SetTile(LogicElement.LogicElementType.None);
        _contentHidden = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Resets tile to original conditions.
    /// </summary>
    public void Reset()
    {
        SetTile(LogicElement.LogicElementType.None);
    }
}
