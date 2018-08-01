using System;
using UnityEngine;

[Serializable]
public class LogicModules
{
    [SerializeField]
    private string _moduleName;
    
   
    [SerializeField]
    private string _holderName;

    

    public string HolderName
    {
        get
        {
            return _holderName;
        }
    }

    public string ModuleName
    {
        get
        {
            return _moduleName;
        }
        set
        {
            _moduleName = value;
        }
    }

    public ModuleHolder.ModuleType ModuleType { get; set; }

    private int _size;
    public int Size
    {
        get
        {
            return _size;
        }
    }
  

    public LogicModules()
    {
    }

    public void Init(ModuleHolder mholder)
    {
        _holderName = mholder.ModuleHolderName;
        this._size = mholder.Size;

        ModuleType = mholder.moduleType;

        _moduleName = mholder.ModuleHolderName;
    }
   
    public bool CanMoveInThisDirection(Vector2Int head, Vector2Int dir, Board board, Snake snake)
    {

        if (ModuleType == ModuleHolder.ModuleType.Simple)
        {
            //Debug.Log("World dir: " + dir);

            int[,] boardCells = new int[Size, Size];
            int[,] moduleCells = RotateElementsByDirection(((SimpleModule)this).Elements,  Snake.HeadToBoard(snake, dir));


            Vector2Int headModulePosition = Vector2Int.zero;
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                  

                    if ( (LogicElement.LogicElementType)moduleCells[i,j] == LogicElement.LogicElementType.MyHead)
                    {
                        headModulePosition = new Vector2Int(i,j);
                    }
                }
            }


            Vector2Int leftTopCorner = head - headModulePosition;

            for (int j = 0; j<Size; j++)
            {
                for (int i = 0; i < Size; i++)
                {
                    Vector2Int pos = new Vector2Int(leftTopCorner.x+i, leftTopCorner.y+j);


                    if (pos.x>0 && pos.y>0 && pos.x<board.Columns && pos.y<board.Rows)
                    {
                        boardCells[i, j] = (int)board[pos]._content;
                    }
                    else
                    {
                        boardCells[i, j] = (int)LogicElement.LogicElementType.Wall;
                    }
                }
            }


            Debug.Log(Snake.HeadToBoard(snake, dir)+" "+dir);

            return CheckTemplate(boardCells, moduleCells);
        }

        else
        {
            switch (((ComplexModule)this).CombinationString)
            {
                case ("!1"):
                    {
                        return !((ComplexModule)this).Submodules[0].CanMoveInThisDirection(head, dir, board, snake);
                    }
            }
        }

        return false;
    }

    private int[,] RotateElementsByDirection(int[,] elements, Vector2Int dir)
    {
        if (dir == Vector2Int.up)
        {
            return RotateMatrixCounterClockwise(RotateMatrixCounterClockwise(elements));
        }

        if (dir == Vector2Int.left)
        {
            return RotateMatrixCounterClockwise(RotateMatrixCounterClockwise(RotateMatrixCounterClockwise(elements)));
        }

        if (dir == Vector2Int.right)
        {
            return RotateMatrixCounterClockwise(elements);
        }

        return elements;
    }

    private bool CheckTemplate(int[,] board, int[,] chip)
    {
  
        for (int j =0;j<board.GetLength(0);j++)
        {
            for (int i = 0; i < board.GetLength(1); i++)
            {
                Debug.Log(i+"/"+j+" "+ (LogicElement.LogicElementType)board[i, j]+"/"+ (LogicElement.LogicElementType)chip[i, j]+"     "+ (board[i, j] == chip[i, j] && ((LogicElement.LogicElementType)board[i, j]) != LogicElement.LogicElementType.MyHead));

                if (board[i,j] == chip[i,j] && ((LogicElement.LogicElementType)board[i,j])!= LogicElement.LogicElementType.MyHead)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private int[,] RotateMatrixCounterClockwise(int[,] oldMatrix)
    {
        Debug.Log("_______");
        int[,] newMatrix = new int[oldMatrix.GetLength(1), oldMatrix.GetLength(0)];
        int newColumn, newRow = 0;
        for (int oldColumn = oldMatrix.GetLength(1) - 1; oldColumn >= 0; oldColumn--)
        {
            newColumn = 0;
            for (int oldRow = 0; oldRow < oldMatrix.GetLength(0); oldRow++)
            {
               // Debug.Log(new Vector2Int(oldRow, oldColumn)+" "+ oldMatrix[oldRow, oldColumn]);
                newMatrix[newRow, newColumn] = oldMatrix[oldRow, oldColumn];
                newColumn++;
            }
            newRow++;
        }
        return newMatrix;
    }
}