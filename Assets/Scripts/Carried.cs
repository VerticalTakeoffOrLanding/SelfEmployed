using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carried : MonoBehaviour {

    PlayerMovement playerMovement;

    float carryX = 0.3f;
    float carryY = 0.21f;
    bool pickedUp = false;
    
    public bool GetPickedUp()
    {
        return pickedUp;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        pickedUp = true;
        //Make the diamond smaller
    }

    // Use this for initialization
    void Start () {
        playerMovement = FindObjectOfType<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        if (pickedUp)
        {
            Vector2 offSet = new Vector2(carryX, carryY);
            Vector2 newPos = playerMovement.transform.position;
            transform.position = newPos - offSet;
        }    
	}
}
