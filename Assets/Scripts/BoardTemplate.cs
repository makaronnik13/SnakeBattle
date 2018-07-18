﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Snake/Board")]
public class BoardTemplate : ScriptableObject
{
    public int SnakesCount
    {
        get
        {
            return Heads.Count;
        }
    }
    public List<Vector2> Heads
    {
        get
        {
            List<Vector2> heads = new List<Vector2>();
            for (int i = 0; i < Cells.Count; i++)
            {
                for (int j = 0; j < Cells[0].raw.Count; j++)
                {
                    if (Cells[i].raw[j].element == LogicElement.LogicElementType.MyHead)
                    {
                        heads.Add(new Vector2(i, j));
                    }
                }
            }
            return heads;
        }
    }

    [SerializeField]
    private List<ListColumn> _cells;

    [SerializeField]
    private List<ElementPair> _tiles;

    public List<ElementPair> Tiles
    {
        get
        {
            if (_tiles == null)
            {
                _tiles = new List<ElementPair>();
                _tiles.Add(new ElementPair(LogicElement.LogicElementType.None, DefaultResources.GetElementByEnum(LogicElement.LogicElementType.None).Img));
                _tiles.Add(new ElementPair(LogicElement.LogicElementType.Wall, DefaultResources.GetElementByEnum(LogicElement.LogicElementType.Wall).Img));
                _tiles.Add(new ElementPair(LogicElement.LogicElementType.MyHead, DefaultResources.GetElementByEnum(LogicElement.LogicElementType.MyHead).Img));
                _tiles.Add(new ElementPair(LogicElement.LogicElementType.MyBody, DefaultResources.GetElementByEnum(LogicElement.LogicElementType.MyBody).Img));
                _tiles.Add(new ElementPair(LogicElement.LogicElementType.MyTail, DefaultResources.GetElementByEnum(LogicElement.LogicElementType.MyTail).Img));
            }
            return _tiles;
        }
    }

    public void Setize(int w, int h)
    {
        List<ListColumn> newCells = new List<ListColumn>();

        for (int i = 0; i<w; i++)
        {
            ListColumn l = new ListColumn(new List<ElementPair>());
            for (int j = 0; j < h; j++)
            {
                l.raw.Add(Tiles.FirstOrDefault(p => p.element == LogicElement.LogicElementType.None));
                if (i == 0 || j == 0 || i == w-1 || j == h-1)
                {
                    l.raw[l.raw.Count-1] = Tiles.FirstOrDefault(p => p.element == LogicElement.LogicElementType.Wall);
                }
            }
            newCells.Add(l);
        }

        _cells = newCells;
    }

    public List<ListColumn> Cells
    {
        get
        {
            if (_cells == null)
            {
                Debug.Log("new cells");
                _cells = new List<ListColumn>();
                for (int i = 0; i < 10; i++)
                {
                    ListColumn l = new ListColumn(new List<ElementPair>());
                    for (int j = 0; j < 10; j++)
                    {
                        l.raw.Add(Tiles.FirstOrDefault(p=>p.element == LogicElement.LogicElementType.None));
                        if (i == 0 || j == 0 || i == 10 - 1 || j == 10 - 1)
                        {
                            l.raw[l.raw.Count - 1] = Tiles.FirstOrDefault(p => p.element == LogicElement.LogicElementType.Wall);
                        }
                    }
                    _cells.Add(l);
                }


                SetWalls();
            }
            return _cells;
        }
    }

    private void SetWalls()
    {
        for (int i = 0; i < Cells.Count; i++)
        {
            _cells[0].raw[i] = Tiles.FirstOrDefault(p => p.element == LogicElement.LogicElementType.Wall);
            _cells[Cells[0].raw.Count-1].raw[i] = Tiles.FirstOrDefault(p => p.element == LogicElement.LogicElementType.Wall);
        }

        for (int i = 0; i < Cells[0].raw.Count; i++)
        {
            _cells[i].raw[0] = Tiles.FirstOrDefault(p => p.element == LogicElement.LogicElementType.Wall);
            _cells[i].raw[Cells.Count - 1] = Tiles.FirstOrDefault(p => p.element == LogicElement.LogicElementType.Wall);
        }
    }

    public List<Vector2> GetSnakeBasePositions(int id, int length)
    {
        List<Vector2> v = new List<Vector2>();
        Vector2 head = Heads[id];
        Vector2 lastPos = head;
        for (int i = 0; i< length; i++)
        {
            v.Add(lastPos);
            lastPos = GetNextPosition(lastPos);
        }
        return v;
    }

    private Vector2 GetNextPosition(Vector2 head)
    {
        if (Cells[(int)head.x - 1].raw[(int)head.y].element == LogicElement.LogicElementType.MyBody)
        {
            return new Vector2((int)head.x - 1, (int)head.y);
        }

        if (Cells[(int)head.x + 1].raw[(int)head.y].element == LogicElement.LogicElementType.MyBody)
        {
            return new Vector2((int)head.x + 1, (int)head.y);
        }

        if (Cells[(int)head.x].raw[(int)head.y+1].element == LogicElement.LogicElementType.MyBody)
        {
            return new Vector2((int)head.x, (int)head.y + 1);
        }

        if (Cells[(int)head.x].raw[(int)head.y - 1].element == LogicElement.LogicElementType.MyBody)
        {
            return new Vector2((int)head.x, (int)head.y - 1);
        }

        throw new Exception("No avaliable slots on map!");
    }

    public void SetCell(ElementPair t, int x, int y)
    {
        _cells[x].raw[y] = t;
    }
}

    [System.Serializable]
    public class ElementPair
    {
        public LogicElement.LogicElementType element;
        public Sprite image;

        public ElementPair(LogicElement.LogicElementType element, Sprite image)
        {
            this.element = element;
            this.image = image;
        }
    }

[System.Serializable]
public class ListColumn
{
    public List<ElementPair> raw;
    public ListColumn(List<ElementPair> raw)
    {
        this.raw = raw;
    }
}