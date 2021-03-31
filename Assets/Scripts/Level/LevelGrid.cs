using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    GameObject playerObject;

    public int width;
    public int height;
    public GameObject Objective;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");

        if (!playerObject)
        {
            return;
        }

        
        if (GameObject.FindGameObjectsWithTag("ObjectiveFood").Length < 3)
        {
            SpawnObjective();
        }
    }


    void SpawnObjective()
    {
        var foodPosition = new Vector2(Random.Range(1, width) + .5f, Random.Range(1, height) + .5f);
        var playerPosition = new Vector2(playerObject.transform.position.x, playerObject.transform.position.y);

        if (Vector2.Distance(foodPosition, playerPosition) >= 5)
        {
            Instantiate(Objective, new Vector3(foodPosition.x, foodPosition.y, 0), Quaternion.identity);
        }
    }
}
