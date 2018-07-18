using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class Tile : MonoBehaviour
{
    /// <summary>
    /// Image component of this GameObject.
    /// </summary>
    private Image image;
    private Image frontImage;
 
    private Sprite _baseSprite;

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
            if (!_rectTransform)
            {
                _rectTransform = GetComponent<RectTransform>();
            }
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

    public void SetTile(LogicElement.LogicElementType t, bool top = false, SnakeSkin skin = null)
    {
        Image img = image;
        if (top)
        {
            img = frontImage;
        }

        if (top)
        {
            img.color = new Color(1, 1, 1, 1);
        }


        _content = t;
        ZRotation = 0;
        switch (t)
        {
            case LogicElement.LogicElementType.MyBody:
                if (skin == null)
                {
                    img.sprite = _baseSprite;
                    break;
                }
                img.sprite = skin.Body;
                break;
            case LogicElement.LogicElementType.MyHead:
                if (skin == null)
                {
                    img.sprite = _baseSprite;
                    break;
                }
                img.sprite = skin.Head;
                break;
            case LogicElement.LogicElementType.MyTail:
                if (skin == null)
                {
                    img.sprite = _baseSprite;
                    break;
                }
                img.sprite = skin.Tail;
                break;
            case LogicElement.LogicElementType.MyAngle:
                if (skin == null)
                {
                    img.sprite = _baseSprite;
                    break;
                }
                img.sprite = skin.Angle;
                break;
            case LogicElement.LogicElementType.None:
                img.sprite = _baseSprite;
                if (img == frontImage)
                {
                    img.color = new Color(1,1,1,0);
                }
                break;
        }
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


    // Use this for initialization
    public void Init(ElementPair s)
    {
        _content = s.element;
        image = GetComponent<Image>();
        frontImage = transform.GetChild(0).GetComponent<Image>();
        _baseSprite = s.image;
        image.sprite = _baseSprite;
    }

 
}
