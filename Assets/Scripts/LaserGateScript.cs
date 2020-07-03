using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGateScript : MonoBehaviour {

    [SerializeField] float offTime = 10f;
    [SerializeField] float midTime = 10f;
    [SerializeField] float onTime = 10f;
    [SerializeField] List<Sprite> stage;
    [SerializeField] bool TwoBlock;
    
    int count;
    bool isOn = false;
    Vector2 collisionPosition;

    BoxCollider2D collider;
    LevelManagement levelManagement;
    SpriteRenderer rend;
    PlayerMovement playerMovement;
    BoxCollider2D lasers;

    // Use this for initialization
    void Start () {
        rend = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        levelManagement = FindObjectOfType<LevelManagement>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        lasers = GetComponent<BoxCollider2D>();

        if (TwoBlock)
        {
            collisionPosition = new Vector2(2.4f,0);
        }
        else
        {
            collisionPosition = new Vector2(1.2f,0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerMovement.lose = true;
    }

    // Update is called once per frame
    void Update ()
    {
        LaserStages();
    }

    private void LaserStages()
    {
        if (count > offTime + midTime + onTime)
        {
            count = 0;
            rend.sprite = stage[0];
            isOn = false;
            collider.offset = collisionPosition;
        }
        if ((count > offTime + midTime) && (isOn == false))
        {
            isOn = true;
            collider.offset = new Vector2(0, 0);
        }
        if ((isOn == false) && (count < offTime))
        {
            lasers.isTrigger = false;
            rend.sprite = stage[0];
        }
        if ((isOn == false) && (count < midTime + offTime) && (count > offTime))
        {
            rend.sprite = stage[1];
        }
        if ((isOn == true) && (count < midTime + offTime + onTime))
        {
            lasers.isTrigger = true;
            rend.sprite = stage[2];
        }
        count += 1;
    }
}
