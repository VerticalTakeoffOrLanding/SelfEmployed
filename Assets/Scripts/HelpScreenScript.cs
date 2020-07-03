using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpScreenScript : MonoBehaviour {

    MoveCamera moveCamera;

    bool escaped = false;

    // Use this for initialization
    void Start () {
        moveCamera = FindObjectOfType<MoveCamera>();	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (escaped == true)
            {
                escaped = false;
            }
            else if (escaped == false)
            {
                escaped = true;
            }
        }
        if (escaped == false)
        {
            transform.position = new Vector2(0, 40);
        }
        else if (escaped == true)
        {
            Move();
        }
	}

    private void Move()
    {
        Vector3 offSet = new Vector3(0, 0, -8);
        Vector3 newPos = moveCamera.transform.position;
        transform.position = newPos - offSet;
    }
}
