using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    PlayerMovement playerMovement;

    [SerializeField] float carryX = 0.3f;
    [SerializeField] float carryY = 0.21f;
    bool keyPickedUp = false;
    bool doorUnlocked = false;

    public bool GetKeyPickedUp()
    {
        return keyPickedUp;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        keyPickedUp = true;
    }

    // Use this for initialization
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (keyPickedUp)
        {
            Vector2 offSet = new Vector2(carryX, carryY);
            Vector2 newPos = playerMovement.transform.position;
            transform.position = newPos - offSet;
        }
    }
}
