using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shop : MonoBehaviour {

    public GameObject ShopButtonPrefab;
    public Transform ShopContent;

    private enum ShopTabs
    {
        Skins,
        Modules,
        Elements,
        Bonuses
    }

	public void Show()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        ShowTab(0);
    }

    public void Hide()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void ShowTab(int i)
    {
        foreach (Transform t in ShopContent)
        {
            Destroy(t.gameObject);
        }

        switch ((ShopTabs)i)
        {
            case ShopTabs.Bonuses:
                foreach (ShopBonus sb in DefaultResources.Bonuses.OrderBy(b=>b.BonusCost))
                {
                    GameObject newShopButton = Instantiate(ShopButtonPrefab, Vector3.zero, Quaternion.identity, ShopContent);
                    newShopButton.transform.localScale = Vector3.one;
                    newShopButton.transform.localPosition = Vector3.zero;
                    newShopButton.GetComponent<ShopButton>().Init(sb);
                }
                break;
            case ShopTabs.Elements:
                foreach (LogicElement le in DefaultResources.Elements.OrderBy(b => b.ElementCost))
                {
                    GameObject newShopButton = Instantiate(ShopButtonPrefab, Vector3.zero, Quaternion.identity, ShopContent);
                    newShopButton.transform.localScale = Vector3.one;
                    newShopButton.transform.localPosition = Vector3.zero;
                    newShopButton.GetComponent<ShopButton>().Init(le);
                }
                break;
            case ShopTabs.Modules:
                foreach (ModuleHolder lm in DefaultResources.Modules.OrderBy(b => b.Cost))
                {
                    GameObject newShopButton = Instantiate(ShopButtonPrefab, Vector3.zero, Quaternion.identity, ShopContent);
                    newShopButton.transform.localScale = Vector3.one;
                    newShopButton.transform.localPosition = Vector3.zero;
                    newShopButton.GetComponent<ShopButton>().Init(lm);
                }
                break;
            case ShopTabs.Skins:
                Debug.Log(DefaultResources.Skins.Count);
                foreach (SnakeSkin ss in DefaultResources.Skins.OrderBy(b => b.SkinCost))
                {
                    GameObject newShopButton = Instantiate(ShopButtonPrefab, Vector3.zero, Quaternion.identity, ShopContent);
                    newShopButton.transform.localScale = Vector3.one;
                    newShopButton.transform.localPosition = Vector3.zero;
                    newShopButton.GetComponent<ShopButton>().Init(ss);
                }
                break;
        }
    }

    public void Buy(object shopObject)
    {
        switch (shopObject.GetType().ToString())
        {
            case "ModuleHolder":
                ModuleHolder moduleHolder = (ModuleHolder)shopObject;
                Player.Instance.Money -= moduleHolder.Cost;
                Player.Instance.Modules.Add(new LogicModules(moduleHolder.Size));
                break;
            case "SnakeSkin":
                SnakeSkin snakeSkin = (SnakeSkin)shopObject;
                Player.Instance.Money -= snakeSkin.SkinCost;
                Player.Instance.Skins.Add(snakeSkin);
                break;
            case "LogicElement":
                LogicElement logicElement = (LogicElement)shopObject;
                Player.Instance.Money -= logicElement.ElementCost;
                Player.Instance.AddElements(logicElement, 1);
                break;
            case "ShopBonus":
                ShopBonus bonus = (ShopBonus)shopObject;
                Player.Instance.Money -= bonus.BonusCost;
                Player.Instance.AddBonus(bonus);
                break;
        }

    }


}
