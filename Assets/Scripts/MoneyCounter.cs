using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyCounter : MonoBehaviour
{
    private Text __moneyText;
    private Text _moneyText
    {
        get
        {
            if (!__moneyText)
            {
                __moneyText = GetComponent<Text>();
            }
            return __moneyText;
        }
    }

	// Use this for initialization
	private void OnEnable ()
    {
        MoneyChanged();
        Player.Instance.OnMoneyChanged += MoneyChanged;	
	}

    private void OnDisable()
    {
        if (Player.Instance)
        {
            Player.Instance.OnMoneyChanged -= MoneyChanged;
        }
    }

    private void MoneyChanged()
    {
        _moneyText.text = Player.Instance.Money + "";
    }
}
