using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeEditor : MonoBehaviour
{
    public Button CreateButton, DeleteButton;
    public Transform SnakeView;
    public Button Left, Right, Create;
    public InputField SnakeName;

    private SnakeProfile _currentSnake;
    private SnakeProfile CurrentSnake
    {
        get
        {
            return _currentSnake;
        }
        set
        {
            _currentSnake = value;
        }
    }

    public void OpenEditor()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void CloseEditor()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void ChooseSkin(SnakeSkin skin)
    {
     
    }
}
