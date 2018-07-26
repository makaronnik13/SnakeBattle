using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeChooser : Singleton<SnakeChooser> {

    public Action<SnakeProfile> OnSnakeChanged = (S) => { };

    public GameObject LeftButton, RightButton, CreateButton;

    public Text SnakeName;

    public Transform snakeVisual;

    private SnakeProfile __editingSnake;
    public SnakeProfile EditingSnake
    {
        get
        {
            return __editingSnake;
        }
        set
        {
            __editingSnake = value;
            OnSnakeChanged(__editingSnake);
            LeftButton.SetActive(__editingSnake != Player.Instance.Snakes[0]);
            RightButton.SetActive(__editingSnake != Player.Instance.Snakes[Player.Instance.Snakes.Count-1]);
            foreach (Image img in snakeVisual.GetComponentsInChildren<Image>())
            {
                img.sprite = EditingSnake.Skin.Body;
            }
            snakeVisual.GetChild(0).GetComponent<Image>().sprite = EditingSnake.Skin.Head;
            snakeVisual.GetChild(snakeVisual.childCount-1).GetComponent<Image>().sprite = EditingSnake.Skin.Tail;
            SnakeName.text = EditingSnake.NickName;
            if (__editingSnake!=null)
            {
                __choosingSnakeId = Player.Instance.Snakes.IndexOf(__editingSnake);
            }
        }
    }

    private int __choosingSnakeId = 0;
    private int _choosingSnakeId
    {
        get
        {
            return __choosingSnakeId;
        }
        set
        {
            __choosingSnakeId = value;
            if (Player.Instance.Snakes.Count!=0)
            {
                EditingSnake = Player.Instance.Snakes[__choosingSnakeId];
            }
            else
            {
                EditingSnake = null;
            }
        }
    }

    public void Previous()
    {
        _choosingSnakeId--;
    }

    public void Next()
    {
        _choosingSnakeId++;
    }

    public void CreateSnake()
    {
        SnakeProfile newSnake = new SnakeProfile(DefaultResources.RandomSkin().SkinName);
        Player.Instance.Snakes.Add(newSnake);
        UpdateSnakeList();
        EditingSnake = newSnake;
    }

    public void LoadProfile()
    {
        //fake
        CreateSnake();
        //
    }

    private void Start()
    {
        //LoadProfile();
    }

    public void DeleteSnake()
    {
        Player.Instance.Snakes.Remove(EditingSnake);
        UpdateSnakeList();
        _choosingSnakeId = 0;
    }

    private void UpdateSnakeList()
    {
        CreateButton.SetActive(Player.Instance.Snakes.Count == 0);
        snakeVisual.gameObject.SetActive(Player.Instance.Snakes.Count != 0);
    }
}
