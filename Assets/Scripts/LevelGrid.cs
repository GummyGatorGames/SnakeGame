using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{

    private Vector2 foodGridPosi;
    public int width;
    public int height;
    private float SpawnTimer;
    private float SpawnTimerMax;

    public GameObject Objective;
    private GameObject playerObj;


    // Start is called before the first frame update
    void Start()
    {

        SpawnTimerMax = 2f;
        SpawnTimer = SpawnTimerMax;
    }

        private void Awake()
    {

    }



    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            playerObj = GameObject.FindGameObjectWithTag("Player");

            if (GameObject.FindGameObjectsWithTag("ObjectiveFood").Length < 3)
            {
                SpawnObjective();
            }
        }

        /*
        SpawnTimer += Time.deltaTime;

        if (SpawnTimer >= SpawnTimerMax)
        {
            SpawnObjective();
            SpawnTimer -= SpawnTimerMax;
        }*/
    }


    private void SpawnObjective()
    {
        foodGridPosi = new Vector2(Random.Range(1, width) + .5f, Random.Range(1, height) + .5f);
        
        if(Vector2.Distance(foodGridPosi,new Vector2(playerObj.transform.position.x, playerObj.transform.position.y)) >= 5)
        {
            Instantiate(Objective, new Vector3(foodGridPosi.x, foodGridPosi.y, 0), Quaternion.identity);
        }


    }
}
