using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] transformsQueue = new Transform[3];
    public Shape[] shapeQueue = new Shape[3];
    public Shape[] shapes;

    private void Awake()
    {
        InitQueue();
    }
    Shape RandomShape()
    {
        int a = Random.Range(0,shapes.Length);
        if (shapes[a]) return shapes[a];
        else return null;
    }
    public Shape SpawnShape()
    {
        Shape shape = GetQueueShape();
        if (shape)
        {
            shape.transform.localScale = Vector3.one;
            shape.transform.position = transform.position;
            return shape;
        }
        else return null;
    }
    void InitQueue()
    {
        for (int i = 0; i < transformsQueue.Length; i++)
        {
            shapeQueue[i] = null;
        }
        FillQueue();
    }
    void FillQueue()
    {
        for (int i = 0; i < transformsQueue.Length; i++)
        {
            if (!shapeQueue[i])
            {
                shapeQueue[i] = Instantiate(RandomShape(), transform.position, Quaternion.identity);
                shapeQueue[i].transform.position = transformsQueue[i].position + shapeQueue[i].queueOffset;
                shapeQueue[i].transform.localScale = new Vector3(0.4f, 0.4f);
            }
        }
    }

    Shape GetQueueShape()
    {
        Shape firstShape = shapeQueue[0];
        for (int i = 1; i < transformsQueue.Length; i++)
        {
            shapeQueue[i-1] = shapeQueue[i];
            shapeQueue[i-1].transform.position = transformsQueue[i-1].position + shapeQueue[i].queueOffset;
        }
        shapeQueue[transformsQueue.Length - 1] = null;
        FillQueue();
        return firstShape;
    }
}
