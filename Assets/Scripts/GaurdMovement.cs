using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaurdMovement : MonoBehaviour {

    LevelManagement levelManagement;
    PlayerMovement playerMovement;

    [SerializeField] List<GameObject> wayPoints;
    [SerializeField] float enemyMoveSpeed;
    [SerializeField] float rotationSpeed = 25;

    int nextWaypointDirection;
    int waypointIndex = 0;
    int rotationDirection;
    bool turn = false;
    Vector3 targetPosition;

    // Use this for initialization
    void Start()
    {
        levelManagement = FindObjectOfType<LevelManagement>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        targetPosition = wayPoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private int FindNewDirection(Vector2 nextWaypoint)
    {
        float xDist;
        float yDist;
        float newDirectionFloat;
        int newDirection;

        xDist = nextWaypoint.x - transform.position.x;
        yDist = nextWaypoint.y - transform.position.y;
        Debug.Log("xNext " + nextWaypoint.x + " yNext " + nextWaypoint.y);
        Debug.Log("xDist " + xDist + " yDist " + yDist);
        newDirectionFloat = (Mathf.Atan(yDist / xDist)*180)/Mathf.PI;
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        playerMovement.lose = true;
        enemyMoveSpeed = 0;
        rotationSpeed = 0;
    }

    private int FindRotationDirection(int rotDir)
    {
        float rotDif;

        rotDif = transform.rotation.eulerAngles.z - rotDir;
        
        if (rotDif < 0)
        {
            rotDif = rotDif + 360;
        }
        if (rotDif < 180)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }

    private void Move()
     {
      
        var movementThisFrame = enemyMoveSpeed * Time.deltaTime;

        if (turn == true)
        {
            
            if (nextWaypointDirection >= 360)
            {
                nextWaypointDirection = 0;
            }
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            if (Mathf.RoundToInt(transform.rotation.eulerAngles.z) <= Mathf.RoundToInt(nextWaypointDirection) + 1 && Mathf.RoundToInt(transform.rotation.eulerAngles.z) >= Mathf.RoundToInt(nextWaypointDirection) - 1)
            {
                turn = false;
                if (rotationSpeed < 0)
                {
                    rotationSpeed = rotationSpeed * -1;
                }
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
        }
        if ((Vector2.Distance(transform.position, targetPosition) < 0.001f) && (turn == false))
        {
            Debug.Log("Time to turn: " + gameObject.name);
            turn = true;
            waypointIndex++;
            if (waypointIndex >= wayPoints.Count)
            {
                 waypointIndex = 0;
            }
            targetPosition = wayPoints[waypointIndex].transform.position;
            nextWaypointDirection = FindNewDirection(targetPosition);
            rotationDirection = FindRotationDirection(nextWaypointDirection);
            rotationSpeed = rotationSpeed * rotationDirection;
        }
     }
}
