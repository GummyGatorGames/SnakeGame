using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyScript : MonoBehaviour
{
    private float MoveTimer;
    private float MoveTimerMax;
    // Start is called before the first frame update
    void Start()
    {
        MoveTimerMax = .2f;
        MoveTimer = MoveTimerMax;

    }

    // Update is called once per frame
    void Update()
    {
        MoveTimer += Time.deltaTime;

        if (MoveTimer >= MoveTimerMax)
        {
            Debug.Log("Destroying");
            MoveTimer -= MoveTimerMax;
            Destroy(this);
        }
    }
}
