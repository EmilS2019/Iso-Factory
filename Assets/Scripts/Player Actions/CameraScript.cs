using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public float CameraSpeed;


    void Update () {
        Zoom();
        Movement();
    }

    void Start()
    {
        from = transform.rotation;
        StartCoroutine(Rotate());

    }

    
    float ZoomLevel = 8;

    void Zoom()
    {
        ZoomLevel -= Input.GetAxis("Mouse ScrollWheel") * 3;

        if (ZoomLevel <= 1)
        {
            ZoomLevel = 1;
        }
        else if (ZoomLevel >= 20)
        {
            ZoomLevel = 20;
        }

        Camera.main.orthographicSize = ZoomLevel;
    }

    void Movement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float w = Input.GetAxisRaw("Vertical");

        if (transform.eulerAngles.y < 45 && transform.eulerAngles.y > 44)
        {
            transform.position += new Vector3(h, 0, -h) * Time.deltaTime * CameraSpeed;
            transform.position += new Vector3(w, 0, w) * Time.deltaTime * CameraSpeed;
        }
        else if (transform.eulerAngles.y == 135)
        {
            transform.position += new Vector3(-h, 0, -h) * Time.deltaTime * CameraSpeed;
            transform.position += new Vector3(w, 0, -w) * Time.deltaTime * CameraSpeed;
        }
        else if (transform.eulerAngles.y < 226 && transform.eulerAngles.y > 200)
        {
            transform.position += new Vector3(-h, 0, h) * Time.deltaTime * CameraSpeed;
            transform.position += new Vector3(-w, 0, -w) * Time.deltaTime * CameraSpeed;
        }
        else if (transform.eulerAngles.y < 316 && transform.eulerAngles.y > 300)
        {
            transform.position += new Vector3(h, 0, h) * Time.deltaTime * CameraSpeed;
            transform.position += new Vector3(-w, 0, w) * Time.deltaTime * CameraSpeed;
        }
    }
    public float speed;
    Quaternion from;
    Quaternion too;
    int YAxis;
    IEnumerator Rotate()
    {

        //TO-DO Optimize this maybe:

        if (Input.GetKeyDown(KeyCode.Q))
        {
            YAxis = 90;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            YAxis = -90;
        }

        if (Input.GetKeyDown(KeyCode.E) | Input.GetKeyDown(KeyCode.Q))
        { 

            too = transform.rotation * Quaternion.Euler(0, YAxis, 0);

            float timePercentage = 0f;
            while (timePercentage < 1)
            {
                timePercentage += Time.deltaTime / speed;
                transform.rotation = Quaternion.Lerp(from, too, timePercentage);
                yield return null;
            }
            from = transform.rotation;
        }

        yield return new WaitForFixedUpdate();
        StartCoroutine(Rotate());

        /*if (Input.GetKeyDown(KeyCode.E))
        {
            transform.Rotate(0,90,0);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.Rotate(0,-90,0);
        }*/

    }
}
