using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

public class Snake : IEnumerable<Vector2Int>
{
    /// <summary>
    /// Queue holding snake's body parts positions.
    /// </summary>
    private LinkedList<Vector2Int> body;

  
    /// <summary>
    /// Game board.
    /// </summary>
    private Board board;

    public SnakeProfile Profile;

    /// <summary>
    /// Gets snake's head position.
    /// </summary>
    public Vector2Int Head
    {
        get
        {
            return body.Last.Value;
        }
    }

    /// <summary>
    /// All body parts positions without last element.
    /// </summary>
    public IEnumerable<Vector2Int> WithoutTail
    {
        get
        {
            return this.Where((p) => { return p != body.First.Value; });
        }
    }

    public Snake(Board board, SnakeProfile profile)
    {
        Profile = profile;
        this.board = board;
        body = new LinkedList<Vector2Int>();
    }

   
    /// <summary>
    /// Resets snake to original position.
    /// </summary>
    public void Reset(List<Vector2Int> positions)
    {
        foreach (var p in body)
        {
            board[p].SetTile(LogicElement.LogicElementType.None);
        }

        body.Clear();

        foreach (Vector2Int position in positions)
        {
            body.AddFirst(position);
        }

        UpdateSnakeState();
    }

    /// <summary>
    /// Moves snake to new position according to direction of movement.
    /// </summary>
    /// <param name="direction">direction of movement</param>
    /// <param name="extend">if true snake will be extended by one segment</param>
    public void Move(Vector2Int direction)
    {

        var newHead = NextHeadPosition(direction);

        Debug.Log(newHead);

        if (!LogicElement.IsWalkable(board[newHead].Content))
        {
            return;
        }

        bool extend = LogicElement.IsExtend(board[newHead].Content);

        body.AddLast(newHead);


        if (extend)
        {
           // body.AddLast(newHead);
        }
        else
        {
            board[body.First.Value].SetTile(LogicElement.LogicElementType.None, true);
            body.RemoveFirst();
        }

        UpdateSnakeState();
    }

    /// <summary>
    /// Updates snake to display correctly
    /// </summary>
    private void UpdateSnakeState()
    {

        // Handle head
        var headPosition = body.Last.Value;
        var nextPosition = body.Last.Previous.Value;

        var tile = board[headPosition];
        tile.SetTile(LogicElement.LogicElementType.MyHead, true, Profile.Skin);

        if (nextPosition.y > headPosition.y)
        {
            tile.ZRotation = 0;
        }
        else if (nextPosition.y < headPosition.y)
        {
            tile.ZRotation = 180;
        }
        else if (nextPosition.x > headPosition.x)
        {
            tile.ZRotation = 90;
        }
        else if (nextPosition.x < headPosition.x)
        {
            tile.ZRotation = -90;
        }

        // Handle middle section
        var previous = body.Last;
        var current = body.Last.Previous;
        while (current != body.First)
        {
            var next = current.Previous;
            tile = board[current.Value];
            if (previous.Value.x == next.Value.x)
            {
                
                tile.SetTile(LogicElement.LogicElementType.MyBody, true, Profile.Skin);
     
                tile.ZRotation = 0;
            }
            else if (previous.Value.y == next.Value.y)
            {        
                tile.SetTile(LogicElement.LogicElementType.MyBody, true, Profile.Skin);
                tile.ZRotation = 90;
            }
            else
            {
              
                tile.SetTile(LogicElement.LogicElementType.MyAngle, true, Profile.Skin);

                if ((previous.Value.x > current.Value.x && next.Value.y < current.Value.y) || (next.Value.x > current.Value.x && previous.Value.y < current.Value.y))
                {
                    tile.ZRotation = 0;
                }
                else if ((previous.Value.x < current.Value.x && next.Value.y < current.Value.y) || (next.Value.x < current.Value.x && previous.Value.y < current.Value.y))
                {
                    tile.ZRotation = 90;
                }
                else if ((previous.Value.x < current.Value.x && next.Value.y > current.Value.y) || (next.Value.x < current.Value.x && previous.Value.y > current.Value.y))
                {
                    tile.ZRotation = 180;
                }
                else if ((previous.Value.x > current.Value.x && next.Value.y > current.Value.y) || (next.Value.x > current.Value.x && previous.Value.y > current.Value.y))
                {
                    tile.ZRotation = 270;
                }
                else
                {
                    tile.SetTile(LogicElement.LogicElementType.MyHead, true, Profile.Skin);
                }
            }
            previous = current;
            current = current.Previous;
        }


        // Handle tail
        var tailPosition = body.First.Value;
        var previousPosition = body.First.Next.Value;

        tile = board[tailPosition];
        tile.SetTile(LogicElement.LogicElementType.MyTail, true, Profile.Skin);

        if (previousPosition.y > tailPosition.y)
        {
            tile.ZRotation = 0;
        }
        else if (previousPosition.y < tailPosition.y)
        {
            tile.ZRotation = 180;
        }
        else if (previousPosition.x > tailPosition.x)
        {
            tile.ZRotation = 90;
        }
        else if (previousPosition.x < tailPosition.x)
        {
            tile.ZRotation = -90;
        }
    }

    /// <summary>
    /// Gets next snake's head position
    /// </summary>
    /// <param name="direction">direction of movement</param>
    /// <returns></returns>
    public Vector2Int NextHeadPosition(Vector2Int direction)
    {
        return Head + new Vector2Int(direction.x, direction.y);
    }

    public IEnumerator<Vector2Int> GetEnumerator()
    {
        return ((IEnumerable<Vector2Int>)body).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<Vector2Int>)body).GetEnumerator();
    }

    public Vector2Int GetDirection()
    {
        List<Vector2Int> avaliableDirections = new List<Vector2Int>
        {
            Vector2Int.left,
            Vector2Int.right,
            Vector2Int.up
        };

        List<Vector2Int> worldAvaliableDirections = new List<Vector2Int>();

        //remove not walkable tiles
        for (int i = 2; i>= 0; i--)
        {
            Vector2Int dir = avaliableDirections[i];
            Vector2Int worldDir = HeadToBoard(this, dir);

            Debug.Log((Head + worldDir)+" "+ board[Head + worldDir].Content);
            if (LogicElement.IsWalkable(board[Head+ worldDir].Content))
            {
                worldAvaliableDirections.Add(worldDir);
            }
        }

        //return if ther is no walkable tiles near
        if (worldAvaliableDirections.Count == 0)
        {
            return Vector2Int.zero;
        }

        worldAvaliableDirections = GetDirectionByModules(worldAvaliableDirections, this);

       
        int randomValue = Mathf.RoundToInt(UnityEngine.Random.Range(0, worldAvaliableDirections.Count));
     
        return worldAvaliableDirections[randomValue];
    }

    private List<Vector2Int> GetDirectionByModules(List<Vector2Int> avaliableDirections, Snake snake)
    {
        foreach (LogicModules module in Profile.Modules)
        {
            if (module == null)
            {
                continue;
            }

            foreach (Vector2Int dir in avaliableDirections)
            {
                Debug.Log(dir);
                if (module.CanMoveInThisDirection(Head, dir, board, snake))
                {
                    Debug.Log("=>");
                    Debug.Log(dir);
                    return new List<Vector2Int>() { dir};
                }
            }
        }

        Debug.Log(avaliableDirections.Count);

        return avaliableDirections;
    }

    public static Vector2Int HeadToBoard(Snake snake, Vector2Int dir)
    {
        Vector2Int worldDir = dir;
        var headPosition = snake.body.Last.Value;
        var nextPosition = snake.body.Last.Previous.Value;

        if (nextPosition.y < headPosition.y)
        {
            if (dir == Vector2Int.up)
            {
                worldDir = Vector2Int.up;
            }
            if (dir == Vector2Int.down)
            {
                worldDir = Vector2Int.down;
            }
            if (dir == Vector2Int.left)
            {
                worldDir = Vector2Int.left;
            }
            if (dir == Vector2Int.right)
            {
                worldDir = Vector2Int.right;
            }
        }

        if (nextPosition.y > headPosition.y)
        {
            if (dir == Vector2Int.up)
            {
                worldDir = Vector2Int.down;
            }
            if (dir == Vector2Int.down)
            {
                worldDir = Vector2Int.up;
            }
            if (dir == Vector2Int.left)
            {
                worldDir = Vector2Int.left;
            }
            if (dir == Vector2Int.right)
            {
                worldDir = Vector2Int.right;
            }
        }
        else if (nextPosition.x < headPosition.x)
        {
            if (dir == Vector2Int.up)
            {
                worldDir = Vector2Int.right;
            }
            if (dir == Vector2Int.down)
            {
                worldDir = Vector2Int.left;
            }
            if (dir == Vector2Int.left)
            {
                worldDir = Vector2Int.up;
            }
            if (dir == Vector2Int.right)
            {
                worldDir = Vector2Int.down;
            }
        }
        else if (nextPosition.x > headPosition.x)
        {
            if (dir == Vector2Int.up)
            {
                worldDir = Vector2Int.left;
            }
            if (dir == Vector2Int.down)
            {
                worldDir = Vector2Int.right;
            }
            if (dir == Vector2Int.left)
            {
                worldDir = Vector2Int.down;
            }
            if (dir == Vector2Int.right)
            {
                worldDir = Vector2Int.up;
            }
        }
        return worldDir;
    }
}
