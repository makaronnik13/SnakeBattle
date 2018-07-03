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

    private List<SnakeProfile> _playerSnakes = new List<SnakeProfile>();
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
            LeftButton.SetActive(__editingSnake != _playerSnakes[0]);
            RightButton.SetActive(__editingSnake != _playerSnakes[_playerSnakes.Count-1]);
            foreach (Image img in snakeVisual.GetComponentsInChildren<Image>())
            {
                img.sprite = EditingSnake.Skin.Body;
            }
            snakeVisual.GetChild(0).GetComponent<Image>().sprite = EditingSnake.Skin.Head;
            snakeVisual.GetChild(snakeVisual.childCount-1).GetComponent<Image>().sprite = EditingSnake.Skin.Tail;
            SnakeName.text = EditingSnake.NickName;
            if (__editingSnake!=null)
            {
                __choosingSnakeId = _playerSnakes.IndexOf(__editingSnake);
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
            if (_playerSnakes.Count!=0)
            {
                EditingSnake = _playerSnakes[__choosingSnakeId];
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
        SnakeProfile newSnake = new SnakeProfile();
        _playerSnakes.Add(newSnake);
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
        LoadProfile();
    }

    public void DeleteSnake()
    {
        _playerSnakes.Remove(EditingSnake);
        UpdateSnakeList();
        _choosingSnakeId = 0;
    }

    private void UpdateSnakeList()
    {
        CreateButton.SetActive(_playerSnakes.Count == 0);
        snakeVisual.gameObject.SetActive(_playerSnakes.Count != 0);
    }
}
