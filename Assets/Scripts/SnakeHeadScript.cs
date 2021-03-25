using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadScript : MonoBehaviour
{
    public GameObject bodySegment;
    private Vector2Int gridPosition;
    private float MoveTimer;
    private float MoveTimerMax;
    private Vector2Int MoveDirection;
    private bool CanMove;
    private bool Alive = true;
    private int snakeBodySize;
    private List<Vector2Int> snakeMovePositionList;
    private List<Quaternion> snakeMoveRotList;
    private List<GameObject> bodySegList;

    public float maxSwipetime;
    public float minSwipeDistance;

    private float swipeStartTime;
    private float swipeEndTime;
    private float swipeTime;
    private float swipeLength;

    private Vector2 startSwipePosition;
    private Vector2 endSwipePosition;

    public Sprite tailSPR;
    private int score = 0;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Body" && Alive)
        {
            Alive = false;
            Debug.Log("GridPosi:" + gridPosition);
            Debug.Log("MoveDirection:" + MoveDirection);
            gridPosition -= MoveDirection;
            transform.position = new Vector3(gridPosition.x + .5f, gridPosition.y + .5f);
            Debug.Log("Head has been bonked by body");


        }
        if (other.tag == "ObjectiveFood")
        {
            Destroy(other.gameObject);
            score++;
            snakeBodySize++;
            Debug.Log("Score is: " + score);


            
            //Vector2Int snakeMovePosition = snakeMovePositionList[0];
            //bodySegList.Add(GameObject.Instantiate(bodySegment, new Vector3(snakeMovePosition.x + .5f, snakeMovePosition.y + .5f, 0), snakeMoveRotList[0]));
            

        }
        if (other.tag == "Wall" && Alive)
        {
            Alive = false;
            Debug.Log("GridPosi:" + gridPosition);
            Debug.Log("MoveDirection:" + MoveDirection);
            gridPosition -= MoveDirection;
            transform.position = new Vector3(gridPosition.x + .5f, gridPosition.y + .5f);
            Debug.Log("Head has been bonked by a wall");


        }
    }

    


    // Start is called before the first frame update
    private void Awake()
    {
        snakeBodySize = 3;
        snakeMovePositionList = new List<Vector2Int>();
        snakeMoveRotList = new List<Quaternion>();
        bodySegList = new List<GameObject>();
        gridPosition = new Vector2Int(19, 9);
        MoveTimerMax = .2f;
        MoveTimer = MoveTimerMax;
        MoveDirection = new Vector2Int(-1, 0);  

    }

    // Update is called once per frame
    void Update()
    {
        SwipeTest();
        if (Alive)
        {
            if (CanMove)
            {
                Control();
            }
            Movement();
        }
    }

    private void Movement()
    {
        MoveTimer += Time.deltaTime;

        if (MoveTimer >= MoveTimerMax)
        {
            MoveTimer -= MoveTimerMax;
            snakeMovePositionList.Insert(0,gridPosition);
            snakeMoveRotList.Insert(0, transform.rotation);
            gridPosition += MoveDirection;
            if(bodySegList.Count < snakeBodySize)
            {
                bodySegList.Insert(0,GameObject.Instantiate(bodySegment, new Vector3(snakeMovePositionList[0].x + .5f, snakeMovePositionList[0].y + .5f, 0), snakeMoveRotList[0]));
                if(bodySegList.Count == 1)
                {
                    bodySegList[0].GetComponent<SpriteRenderer>().sprite = tailSPR;
                }
            }
            if (snakeMovePositionList.Count >= snakeBodySize + 1)
            {
                snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);
                snakeMoveRotList.RemoveAt(snakeMoveRotList.Count - 1);
            }
            for (int i = 0; i < bodySegList.Count; i++)
            {
                Vector2Int snakeMovePosition = snakeMovePositionList[i];
                bodySegList[i].transform.position = new Vector3(snakeMovePosition.x+.5f,snakeMovePosition.y+.5f,0);
                bodySegList[i].transform.rotation = snakeMoveRotList[i];

            }



                transform.position = new Vector3(gridPosition.x+.5f, gridPosition.y+.5f);
                CanMove = true;
            }

    }

    private void Control()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (MoveDirection.y != -1) 
            {
                MoveDirection.x = 0;
                MoveDirection.y = 1;
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, -90);
                CanMove = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (MoveDirection.y != 1)
            {
                MoveDirection.x = 0;
                MoveDirection.y = -1;
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 90);
                CanMove = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) )
        {
            if (MoveDirection.x != -1)
            {
                MoveDirection.x = 1;
                MoveDirection.y = 0;
                transform.eulerAngles = new Vector3(0, -180, 0);
                CanMove = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (MoveDirection.x != 1)
            {
                MoveDirection.x = -1;
                MoveDirection.y = 0;
                transform.eulerAngles = new Vector3(0, 0, 0);
                CanMove = false;
            }
        }

    }

    private void SwipeTest()
    {
        if(Input.touchCount>0)
        {
            Debug.Log("touched");
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                swipeStartTime = Time.time;
                startSwipePosition = touch.position;
            }
            else
            {
                swipeEndTime = Time.time;
                endSwipePosition = touch.position;
                swipeTime = swipeEndTime - swipeStartTime;
                swipeLength = (endSwipePosition - startSwipePosition).magnitude;
                if(swipeTime<maxSwipetime && swipeLength > minSwipeDistance)
                {
                    swipeControl();
                }
            }
        }
    }

    private void swipeControl()
    {
        Vector2 Distance = endSwipePosition - startSwipePosition;
        float xDistance =Mathf.Abs(Distance.x);
        float yDistance =Mathf.Abs(Distance.y);
        if (yDistance < xDistance)
        {
            if (Distance.x > 0)
            {
                if (MoveDirection.x != -1)
                {
                    MoveDirection.x = 1;
                    MoveDirection.y = 0;
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    CanMove = false;
                    Debug.Log("Right");
                }
            }
            else if (Distance.x < 0)
            {
                if (MoveDirection.x != 1)
                {
                    MoveDirection.x = -1;
                    MoveDirection.y = 0;
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    CanMove = false;
                    Debug.Log("Left");
                }
            }

        }
        else if (yDistance > xDistance)
        {
            if (Distance.y > 0)
            {
                if (MoveDirection.y != -1)
                {
                    MoveDirection.x = 0;
                    MoveDirection.y = 1;
                    transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, -90);
                    CanMove = false;
                    Debug.Log("Up");
                }
            } 
            else if (Distance.y < 0)
            {
                if (MoveDirection.y != 1)
                {
                    MoveDirection.x = 0;
                    MoveDirection.y = -1;
                    transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 90);
                    CanMove = false;
                    Debug.Log("Down");
                }
            }    

        }


    }
}
