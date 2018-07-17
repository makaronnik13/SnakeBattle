using System;
using System.Collections;
using System.Collections.Generic;
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
                for (int j = 0; j < Cells[0].Count; j++)
                {
                    if (Cells[i][j] == LogicElement.LogicElementType.MyHead)
                    {
                        heads.Add(new Vector2(i, j));
                    }
                }
            }
            return heads;
        }
    }

    [SerializeField]
    private List<List<LogicElement.LogicElementType>> _cells;



    public void Setize(int w, int h)
    {
        List<List<LogicElement.LogicElementType>> newCells = new List<List<LogicElement.LogicElementType>>();

        for (int i = 0; i<w; i++)
        {
            List<LogicElement.LogicElementType> l = new List<LogicElement.LogicElementType>();
            for (int j = 0; j < h; j++)
            {
                l.Add(LogicElement.LogicElementType.None);
                if (i == 0 || j == 0 || i == w-1 || j == h-1)
                {
                    l[l.Count-1] = LogicElement.LogicElementType.Wall;
                }
            }
            newCells.Add(l);
        }

        _cells = newCells;
    }

    public List<List<LogicElement.LogicElementType>> Cells
    {
        get
        {
            if (_cells == null)
            {
                Debug.Log("new cells");
                _cells = new List<List<LogicElement.LogicElementType>>();
                for (int i = 0; i < 10; i++)
                {
                    List<LogicElement.LogicElementType> l = new List<LogicElement.LogicElementType>();
                    for (int j = 0; j < 10; j++)
                    {
                        l.Add(LogicElement.LogicElementType.None);
                        if (i == 0 || j == 0 || i == 10 - 1 || j == 10 - 1)
                        {
                            l[l.Count - 1] = LogicElement.LogicElementType.Wall;
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
            _cells[0][i] = LogicElement.LogicElementType.Wall;
            _cells[Cells[0].Count-1][i] = LogicElement.LogicElementType.Wall;
        }

        for (int i = 0; i < Cells[0].Count; i++)
        {
            _cells[i][0] = LogicElement.LogicElementType.Wall;
            _cells[i][Cells.Count - 1] = LogicElement.LogicElementType.Wall;
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
        if (Cells[(int)head.x - 1][(int)head.y] == LogicElement.LogicElementType.MyBody)
        {
            return new Vector2((int)head.x - 1, (int)head.y);
        }

        if (Cells[(int)head.x + 1][(int)head.y] == LogicElement.LogicElementType.MyBody)
        {
            return new Vector2((int)head.x + 1, (int)head.y);
        }

        if (Cells[(int)head.x][(int)head.y+1] == LogicElement.LogicElementType.MyBody)
        {
            return new Vector2((int)head.x, (int)head.y + 1);
        }

        if (Cells[(int)head.x][(int)head.y - 1] == LogicElement.LogicElementType.MyBody)
        {
            return new Vector2((int)head.x, (int)head.y - 1);
        }

        throw new Exception("No avaliable slots on map!");
    }

    public void SetCell(LogicElement.LogicElementType t, int x, int y)
    {
        _cells[x][y] = t;
    }
}
