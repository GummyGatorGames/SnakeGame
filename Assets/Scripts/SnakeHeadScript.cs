using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadScript : MonoBehaviour
{
    Vector2Int gridPosition;
    float moveTimer;
    float moveTimerMax;
    Vector2Int moveDirection;
    bool canMove;
    bool isAlive = true;
    int snakeBodySize;
    List<Vector2Int> snakeMovePositionList;
    List<Quaternion> snakeMoveRotList;
    List<GameObject> bodySegList;
    float swipeStartTime;
    float swipeEndTime;
    float swipeTime;
    float swipeLength;
    Vector2 startSwipePosition;
    Vector2 endSwipePosition;
    int score = 0;

    public float MaxSwipetime;
    public float MinSwipeDistance;
    public GameObject BodySegment;
    public Sprite TailSPR;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Body" && isAlive)
        {
            isAlive = false;
            Debug.Log($"GridPosi: {gridPosition}, MoveDirection: {moveDirection}");
            gridPosition -= moveDirection;
            transform.position = new Vector3(gridPosition.x + .5f, gridPosition.y + .5f);
            Debug.Log("Head has been bonked by body");
        }

        if (other.tag == "ObjectiveFood")
        {
            Destroy(other.gameObject);
            score++;
            snakeBodySize++;
            Debug.Log("Score is: " + score);
        }

        if (other.tag == "Wall" && isAlive)
        {
            isAlive = false;
            Debug.Log($"GridPosi: {gridPosition}, MoveDirection: {moveDirection}");
            gridPosition -= moveDirection;
            transform.position = new Vector3(gridPosition.x + .5f, gridPosition.y + .5f);
            Debug.Log("Head has been bonked by a wall");
        }
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        snakeBodySize = 3;
        snakeMovePositionList = new List<Vector2Int>();
        snakeMoveRotList = new List<Quaternion>();
        bodySegList = new List<GameObject>();
        gridPosition = new Vector2Int(19, 9);
        moveTimerMax = .2f;
        moveTimer = moveTimerMax;
        moveDirection = new Vector2Int(-1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        SwipeTest();

        if (!isAlive)
        {
            return;
        }

        if (canMove)
        {
            Control();
        }
        Movement();
    }

    void Movement()
    {
        moveTimer += Time.deltaTime;

        if (moveTimer < moveTimerMax)
        {
            return;
        }

        moveTimer -= moveTimerMax;
        snakeMovePositionList.Insert(0, gridPosition);
        snakeMoveRotList.Insert(0, transform.rotation);
        gridPosition += moveDirection;
        
        if (bodySegList.Count < snakeBodySize)
        {
            bodySegList.Insert(0, GameObject.Instantiate(BodySegment, new Vector3(snakeMovePositionList[0].x + .5f, snakeMovePositionList[0].y + .5f, 0), snakeMoveRotList[0]));
            if (bodySegList.Count == 1)
            {
                bodySegList[0].GetComponent<SpriteRenderer>().sprite = TailSPR;
            }
        }

        if (snakeMovePositionList.Count >= snakeBodySize + 1)
        {
            snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);
            snakeMoveRotList.RemoveAt(snakeMoveRotList.Count - 1);
        }

        transform.position = new Vector3(gridPosition.x + .5f, gridPosition.y + .5f);

        for (int i = 0; i < bodySegList.Count; i++)
        {
            Vector2Int snakeMovePosition = snakeMovePositionList[i];
            bodySegList[i].transform.position = new Vector3(snakeMovePosition.x + .5f, snakeMovePosition.y + .5f, 0);
            bodySegList[i].transform.rotation = snakeMoveRotList[i];
        }

        canMove = true;
    }

    void Control()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && moveDirection.y == 0)
        {
            moveDirection.x = 0;
            moveDirection.y = 1;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, -90);
            Debug.Log("Up");
            canMove = false;
        }
        else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && moveDirection.y == 0)
        {
            moveDirection.x = 0;
            moveDirection.y = -1;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 90);
            Debug.Log("Down");
            canMove = false;
        }
        else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && moveDirection.x == 0)
        {
            moveDirection.x = 1;
            moveDirection.y = 0;
            transform.eulerAngles = new Vector3(0, -180, 0);
            Debug.Log("Right");
            canMove = false;
        }
        else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && moveDirection.x == 0)
        {
            moveDirection.x = -1;
            moveDirection.y = 0;
            transform.eulerAngles = new Vector3(0, 0, 0);
            Debug.Log("Left");
            canMove = false;
        }

    }

    void SwipeTest()
    {
        if(Input.touchCount<=0)
        {
            return;
        }
        Debug.Log("touched");
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            swipeStartTime = Time.time;
            startSwipePosition = touch.position;
            return;
        }

        swipeEndTime = Time.time;
        endSwipePosition = touch.position;
        swipeTime = swipeEndTime - swipeStartTime;
        swipeLength = (endSwipePosition - startSwipePosition).magnitude;
        if (swipeTime < MaxSwipetime && swipeLength > MinSwipeDistance)
        {
            swipeControl();
        }
    }

    void swipeControl()
    {
        var distance = endSwipePosition - startSwipePosition;
        var xDistance =Mathf.Abs(distance.x);
        var yDistance =Mathf.Abs(distance.y);

        if (yDistance < xDistance)
        {
            if (distance.x > 0 && moveDirection.x == 0)
            {
                moveDirection.x = 1;
                moveDirection.y = 0;
                transform.eulerAngles = new Vector3(0, -180, 0);
                canMove = false;
                Debug.Log("Right");
            }

            else if (distance.x < 0 && moveDirection.x == 0)
            {
                moveDirection.x = -1;
                moveDirection.y = 0;
                transform.eulerAngles = new Vector3(0, 0, 0);
                canMove = false;
                Debug.Log("Left");
            }
        }
        else if (yDistance > xDistance)
        {
            if (distance.y > 0 && moveDirection.y == 0)
            {
                moveDirection.x = 0;
                moveDirection.y = 1;
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, -90);
                canMove = false;
                Debug.Log("Up");
            } 

            else if (distance.y < 0 && moveDirection.y == 0)
            {
                moveDirection.x = 0;
                moveDirection.y = -1;
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 90);
                canMove = false;
                Debug.Log("Down");
            }
        }
    }
}
