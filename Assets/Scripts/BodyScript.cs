using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyScript : MonoBehaviour
{
    float moveTimer;
    float moveTimerMax;

    // Start is called before the first frame update
    void Start()
    {
        moveTimerMax = .2f;
        moveTimer = moveTimerMax;
    }

    // Update is called once per frame
    void Update()
    {
        moveTimer += Time.deltaTime;

        if (moveTimer < moveTimerMax)
        {
            return;
        }

        Debug.Log("Destroying");
        moveTimer -= moveTimerMax;
        Destroy(this);
    }
}
