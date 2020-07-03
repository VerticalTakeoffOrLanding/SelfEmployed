using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoDown : MonoBehaviour {

    PlayerMovement playerMovement;
    MoveCamera moveCamera;

    // Use this for initialization
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        moveCamera = FindObjectOfType<MoveCamera>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerMovement.GoDownStairs();
        moveCamera.CamGoDownStairs();
    }
}
