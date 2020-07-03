using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorLock : MonoBehaviour {

    int rotation;
    Vector3 nextPos;
    bool unlocked = false;
    bool pickedUp = false;

    Key key;

	// Use this for initialization
	void Start ()
    {
        key = FindObjectOfType<Key>();	
	}

    public void OpenDoor()
    {
        rotation = Mathf.RoundToInt(transform.eulerAngles.z);
        if (rotation == 90)
        {
            nextPos = transform.position + new Vector3(0, 1.2f, 0);
        }
        if (rotation == 0)
        {
            nextPos = transform.position + new Vector3(1.2f, 0, 0);
        }
        if (rotation == 180)
        {
            nextPos = transform.position + new Vector3(-1.2f, 0, 0);
        }
        if (rotation == 270)
        {
            nextPos = transform.position + new Vector3(0, -1.2f, 0);
        }
        transform.position = nextPos;
        unlocked = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        pickedUp = key.GetKeyPickedUp();
        if ((collision.gameObject.name == "Player") && (pickedUp == true))
        {
            Debug.Log("mmmmmm");
            OpenDoor();
        }
    }

    // Update is called once per frame
    void Update ()
    {
        
	}
}
