using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    //public List<Transform> bodyparts = new List<Transform>();
    public GameObject snakeHead;

    public float minStepTime, MaxStepTime, StepTimeAdder;

    private float stepTime = .3f, step, delta = 0f;

    Transform head;
    Vector2 moveDirection = Vector2.up;

    void Start()
    {
        Instantiate(snakeHead, new Vector3(-10, -10, 0), Quaternion.identity);
    }

    void Update()
    {
        if (delta >= stepTime)
        {
            //Move();
            delta = 0f;
        }
        delta += Time.deltaTime;

    }
}
