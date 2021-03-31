using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            Debug.Log("Food in wall fixing");
            Destroy(this.gameObject);
        }

        if (other.tag == "Body")
        {
            Debug.Log("Food in body fixing");
            Destroy(this.gameObject);
        }
    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}