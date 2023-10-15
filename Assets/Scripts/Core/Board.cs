using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Transform emptySquare;
    public int height =30;
    public int width =10;
    public int header = 8;
    public ParticlePlayer[] particlePlayer;
    public int completeRow=0;
    Transform[,] grid;
    private void Awake()
    {
        grid = new Transform[width, height];
    }
    void Start()
    {
        DrawEmptyCells();
    }

    // Update is called once per frame
    private void DrawEmptyCells()
    {
        if (emptySquare!=null)
        {
            for (int i = 0; i < height-header; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Transform clone = Instantiate(emptySquare, new Vector3(emptySquare.position.x +  j, emptySquare.position.y +  i), Quaternion.identity);
                    
                    //clone.parent = transform;
                }
            }
        }
        else
        {
            Debug.Log("Null Empty Square");
        }
    }
    bool IsWithinBoard(int x, int y)
    {
        return (x >= 0 && x < width && y >= 0);
    }
    bool isOccupied(int x, int y, Shape shape)
    {
        //return (grid[x, y] != null&&shape.transform!=grid[x,y].parent);
        return (grid[x, y] != null);
    }
    public bool IsValidPosition(Shape shape)
    {
        foreach(Transform s in shape.transform)
        {
            Vector2 vec2 = Vectorf.Round(s.position);
            if (!IsWithinBoard((int)vec2.x, (int)vec2.y))
            {
                return false;
            }
            if (isOccupied((int)vec2.x, (int)vec2.y, shape))
            {
                return false;
            }
        }
        return true;
    }
    public void StoreShapeInGrid(Shape shape)
    {
        if(shape == null)
        {
            return;
        }
        foreach(Transform childShape in shape.transform)
        {
            Vector2 vec2 = Vectorf.Round(childShape.position);
            grid[(int)vec2.x, (int)vec2.y] = childShape;
        }
    }
    bool IsComplete(int y)
    {
        for(int x=0; x < width; x++)
        {
            if (!grid[x, y]) return false;
        }
        return true;
    }
    void ClearRow(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y])
            {
                Destroy(grid[x, y].gameObject);
                grid[x, y] = null;
            }
        }
    }
    void ClearRowFX(int ind,int y)
    {
        if (particlePlayer[ind])
        {
            particlePlayer[ind].transform.position = new Vector3(0, y+ind, -1f);
            particlePlayer[ind].Play();
        }
    }
    void ShiftOneRowDown(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y])
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                //grid[x, y - 1].Translate(new Vector3(0, -1f, 0));
                grid[x, y - 1].position += new Vector3(0, -1f, 0);
            }
        }
    }
    void ShiftRowsDown(int startY)
    {
       
        for(int y = startY; y < height; y++)
        {
            ShiftOneRowDown(y);
        }
    }
    public void CleanAllRows()
    {
        for (int y = 0; y < height; y++)
        {
            if (IsComplete(y))
            {
                ClearRow(y);
                ClearRowFX(completeRow,y);
                completeRow++;
                ShiftRowsDown(y+1);
                y--;
            }
        }
    }
    public bool IsOverLimit(Shape shape)
    {
        foreach(Transform shapeChild in shape.transform) {
            if (shapeChild.position.y >= height - header - 1)
            {
                return true;
            }
        }
        return false;
    }
}
