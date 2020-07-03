using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    WinLoseScript winLose;
    GaurdMovement gaurdMovement;
    LevelManagement levelManagement;
    followCursor FollowCursor;

    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float runSpeed = 20f;
    Carried carried;
    Transform currentPos;
    Quaternion cursorDirection;
    bool pickedUp;
    bool running = false;
    public bool win = false;
    public bool lose = false;
    int count = 0;
    float speed;

    private void Move()
    {
        //Walk animation
        //Always look at cursor

        var direction = FindNewDirection(FollowCursor.transform.position);

        //Quaternion roootate = Quaternion.Euler(0, 0, direction);
        //transform.rotation = roootate;

        //var deltaX = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        //var deltaY = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

        //var newXPos = transform.position.x + deltaX;
        //var newYPos = transform.position.y + deltaY;
        //transform.position = new Vector2(newXPos, newYPos);

        //transform.position = Vector2.MoveTowards(transform.position, FollowCursor.transform.position, 2f * Time.deltaTime);

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            transform.position += transform.right * speed * Time.deltaTime * -1;
        }

        if ((Input.GetAxisRaw("Fire1") > 0) && ((Input.GetAxisRaw("Vertical") == 0) && (Input.GetAxisRaw("Horizontal") == 0)))
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }
        else if ((Input.GetAxisRaw("Fire1") < 0) && ((Input.GetAxisRaw("Vertical") == 0) && (Input.GetAxisRaw("Horizontal") == 0)))
        {
            transform.position += transform.up * speed * Time.deltaTime * -1;
        }

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            Quaternion roootate = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - 4);
            transform.rotation = roootate;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            Quaternion roootate = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 4);
            transform.rotation = roootate;
        }
    }

    // Use this for initialization
    void Start () {
        levelManagement = FindObjectOfType<LevelManagement>();
        gaurdMovement = FindObjectOfType<GaurdMovement>();
        FollowCursor = FindObjectOfType<followCursor>();
        winLose = FindObjectOfType<WinLoseScript>();
        carried = FindObjectOfType<Carried>();
        speed = moveSpeed;
    }

    // Update is called once per frame
    void Update ()
    {
        Move();
        WinFunction();
        LoseFunction();
    }

    public void LoseFunction()
    {
        if (lose == true)
        {
            winLose.ShowCaught();
            speed = 0;
            if (count < 200)
            {
                count += 1;
            }
            else
            {
                lose = false;
                levelManagement.ReloadScene();
            }
        }
    }

    public void WinFunction()
    {
        if (win == true)
        {
            winLose.ShowPayday();
            speed = 0;
            if (count < 100)
            {
                count += 1;
            }
            else
            {
                win = false;
                levelManagement.LoadNextScene();
            }
        }
    }

    private int FindNewDirection(Vector2 nextWaypoint)
    {
        float xDist;
        float yDist;
        float newDirectionFloat;
        int newDirection;

        xDist = nextWaypoint.x - transform.position.x;
        yDist = nextWaypoint.y - transform.position.y;
        newDirectionFloat = (Mathf.Atan(yDist / xDist) * 180) / Mathf.PI;
        newDirection = Mathf.RoundToInt(newDirectionFloat);
        if (newDirection < 0)
        {
            newDirection = newDirection * -1;
        }
        //Debug.Log("This is the direction Between 0-90: " + newDirection);
        if ((xDist <= 0) && (yDist >= 0))
        {
            newDirection = 180 - newDirection;
        }
        else if ((xDist <= 0) && (yDist <= 0))
        {
            newDirection = 180 + newDirection;
        }
        else if ((xDist >= 0) && (yDist <= 0))
        {
            newDirection = 360 - newDirection;
        }
        //Debug.Log("This is the direction I need to face" + newDirection);
        return newDirection;
    }

    public void GoUpStairs()
    {
        transform.position = transform.position + new Vector3(24, 0, 0);
    }

    public void GoDownStairs()
    {    
        transform.position = transform.position + new Vector3(-24, 0, 0);
    }

    public Transform GetPosition()
    {
        return currentPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pickedUp = carried.GetPickedUp();
        if (pickedUp == true)
        {
            if (collision.gameObject.name == "WinBox")
            {
                win = true;
            }
        }
    }
}
