using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostShape : MonoBehaviour
{
    public Color ghostColor = new Color(1f, 1f, 1f, 0.2f);
    bool hitBoard = false;
    Shape ghostShape;
    public void CreateGhostShape(Shape shapeObj, Board board)
    {
        if (!ghostShape)
        {
            ghostShape = Instantiate(shapeObj);
            SpriteRenderer[] childGhostShape = ghostShape.GetComponentsInChildren<SpriteRenderer>();
            foreach(SpriteRenderer s in childGhostShape)
            {
                s.color = ghostColor;
            }
        }
        else
        {
            ghostShape.transform.position = shapeObj.transform.position;
            ghostShape.transform.rotation = shapeObj.transform.rotation;
        }
        hitBoard = false;
        while (!hitBoard)
        {
            ghostShape.MoveDown();
            if (!board.IsValidPosition(ghostShape))
            {
                ghostShape.MoveUp();
                hitBoard = true;
            }
        }
    }
    public void ResetGhostShape()
    {
        Destroy(ghostShape.gameObject);
    }
}
