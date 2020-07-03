using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

    PlayerMovement playerMovement;
    [SerializeField] float cameraDistance = 10f;
    int currentFloor = 0;
    float xMaxLimit = 5.4f;
    float xMinLimit = -5.4f;

    // Use this for initialization
    void Start () {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    public void CamGoUpStairs()
    {
        currentFloor += 1;
        transform.position = transform.position + new Vector3(24, 0, 0);   
    }

    public void CamGoDownStairs()
    {
        currentFloor -= 1;
        transform.position = transform.position + new Vector3(-24, 0, 0);
    }

    // Update is called once per frame
    void Update () {
        Vector3 offSet = new Vector3(0,0,cameraDistance);
        Vector3 newPos = playerMovement.transform.position;
        xMaxLimit = 5.4f + (24 * currentFloor);
        xMinLimit = -5.4f + (24 * currentFloor);
        newPos.x = Mathf.Clamp(newPos.x, xMinLimit, xMaxLimit);
        newPos.y = Mathf.Clamp(newPos.y, -3.5f, 3.5f);
        transform.position = newPos - offSet;

    }
}
