using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public bool canRotate = true;
    public bool canRotateMul = true;
    public Vector3 queueOffset;
    void Move(Vector3 moveDirection)
    {
        transform.position += moveDirection;
    }
    public void MoveLeft()
    {
        Move(new Vector3(-1, 0));
    }
    public void MoveRight()
    {
        Move(new Vector3(1, 0));
    }
    public void MoveDown()
    {
        Move(new Vector3(0, -1));
    }
    public void MoveUp()
    {
        Move(new Vector3(0, 1));
    }
    public void RotateRight()
    {
        if (canRotate)
        {
            transform.Rotate(0, 0, -90);
        }
    }
    public void RotateLeft()
    {
        if (canRotate)
        {
            transform.Rotate(0, 0, 90);
        }
    }

    public void RotateClockwise(bool rotateClockwise)
    {
        if (rotateClockwise)
        {
            RotateRight();
        }
        else
        {
            RotateLeft();
        }
    }
}
