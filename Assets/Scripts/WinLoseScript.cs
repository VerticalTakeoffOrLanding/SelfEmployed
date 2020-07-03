using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseScript : MonoBehaviour {

    SpriteRenderer rend;
    MoveCamera moveCamera;

    [SerializeField] GameObject PaydayImage;
    [SerializeField] GameObject CaughtImage;

    // Use this for initialization
    void Start()
    {
        GetColourStuff();
        PaydayImage.transform.position = new Vector2(-30, -30);
        CaughtImage.transform.position = new Vector2(-30, -30);
    }

    public void ShowCaught()
    {
        Vector3 offSet = new Vector3(0, 0, 1);
        Vector3 camPos = moveCamera.transform.position;
        CaughtImage.transform.position = camPos + offSet;
    }

    public void ShowPayday()
    {
        Vector3 offSet = new Vector3(0, 0, 1);
        Vector3 camPos = moveCamera.transform.position;
        PaydayImage.transform.position = camPos + offSet;
    }

    private void GetColourStuff()
    {
        rend = GetComponent<SpriteRenderer>();
        moveCamera = FindObjectOfType<MoveCamera>();
        Color c = rend.material.color;
        //c.a = 0f;
        //rend.material.color = c;
    }
    
    void Update()
    {
        Move();

        if (Input.GetKeyDown("space"))
        {
            StartFadingOut();
        }
    }

    private void Move()
    {
        Vector3 offSet = new Vector3(0, 0, -8);
        Vector3 newPos = moveCamera.transform.position;
        transform.position = newPos - offSet;
    }



    IEnumerator FadeIn()
    {
        for (float f = 0.05f; f <= 1; f += 0.05f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator FadeOut()
    {
        for (float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void StartFadingOut()
    {
        StartCoroutine("FadeOut");
    }

    public void StartFadingIn()
    {
        StartCoroutine("FadeIn");
    }
}
