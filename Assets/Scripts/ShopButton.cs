using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour {

    public Text ItemName, ItemCost;
    public Image ItemPreview;
    private object _shopObject;

    public Button ByeButton, InfoButton;

    public void Init(ShopBonus sb)
    {
        _shopObject = sb;
        ItemName.text = sb.BonusName;
        ItemCost.text = sb.BonusCost+"";
        ItemPreview.sprite = sb.BonusImg;
        ByeButton.interactable = Player.Instance.Money >= sb.BonusCost;
        InfoButton.onClick.AddListener(ShowInfo);
        ByeButton.onClick.AddListener(Buy);
    }

    public void Init(LogicElement le)
    {
        _shopObject = le;
        ItemName.text = le.ElementName;
        ItemCost.text = le.ElementCost+"";
        ItemPreview.sprite = le.Img;
        ByeButton.interactable = Player.Instance.Money >= le.ElementCost;
        InfoButton.onClick.AddListener(ShowInfo);
        ByeButton.onClick.AddListener(Buy);
    }

    public void Init(SnakeSkin ss)
    {
        _shopObject = ss;
        ItemName.text = ss.SkinName;
        ItemCost.text = ss.SkinCost + "";
        ItemPreview.sprite = ss.Head;
        ByeButton.interactable = Player.Instance.Money >= ss.SkinCost;
        InfoButton.onClick.AddListener(ShowInfo);
        ByeButton.onClick.AddListener(Buy);
    }

    public void Init(ModuleHolder lm)
    {
        _shopObject = lm;
        ItemName.text = lm.ModuleHolderName;
        ItemCost.text = lm.Cost+"";
        ItemPreview.sprite = lm.Img;
        ByeButton.interactable = Player.Instance.Money >= lm.Cost;
        InfoButton.onClick.AddListener(ShowInfo);
        ByeButton.onClick.AddListener(Buy);
    }

    public void ShowInfo()
    {

    }

    public void Buy()
    {
        GetComponentInParent<Shop>().Buy(_shopObject);
    }

    private void PlayerMoneysChanged()
    {
        int cost = 0;
        switch (_shopObject.GetType().ToString())
        {
            case "ModuleHolder":
                cost = ((ModuleHolder)_shopObject).Cost;
                break;
            case "SnakeSkin":
                cost = ((SnakeSkin)_shopObject).SkinCost;
                break;
            case "LogicElement":
                cost = ((LogicElement)_shopObject).ElementCost;
                break;
            case "ShopBonus":
                cost = ((ShopBonus)_shopObject).BonusCost;
                break;
        }

        ByeButton.interactable = Player.Instance.Money >= cost;
    }

    private void OnDisable()
    {
        if (Player.Instance)
        {
            Player.Instance.OnMoneyChanged -= PlayerMoneysChanged;
        }
    }

    private void OnEnable()
    {
        if (Player.Instance)
        {
            Player.Instance.OnMoneyChanged += PlayerMoneysChanged;
        }
    }
}
