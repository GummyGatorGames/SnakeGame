using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class ADManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Advertisement.Initialize("4071807",true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Hitting Ad Button");
            
        }


    }
}
