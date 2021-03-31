using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject SnakeHead;

    float stepTime = .3f;
    float delta = 0f;

    void Start()
    {
        Instantiate(SnakeHead, new Vector3(-10, -10, 0), Quaternion.identity);
    }

    void Update()
    {
        if (delta >= stepTime)
        {
            delta = 0f;
        }
        delta += Time.deltaTime;
    }
}
