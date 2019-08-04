using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceScript : MonoBehaviour {

    public ItemList Item;
    public float speed = 3;
    public Vector3 direction;
    Ray ray;
    RaycastHit hit;


    [Header("Raycast Settings")]
    public Vector3 RaycastPlacement;
    public LayerMask onlyBuildings;
    public LayerMask onlyItems;

    //The item instantiates
    void Start()
    {
        direction = FindOutput();
        StartCoroutine(Movement());
    }

    Vector3 FindOutput()
    {

        Vector3[] dir = new Vector3[4] { Vector3.forward, -Vector3.forward, Vector3.right, Vector3.left };

        foreach (Vector3 d in dir)
        {
            ray = new Ray(transform.position, d);
            Debug.DrawRay(ray.origin, ray.direction, Color.white, 0.25f);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.collider.name == "Output")
            {
                return d;
            }
        }

        Debug.LogWarning("No output found");
        return new Vector3(0,0,0);
    }

    IEnumerator FindDirection()
    {
        //The item sends a raycast to the current building it's on and finds out the direction
        bool reset = false;
        ray = new Ray(transform.position + RaycastPlacement, -transform.up);
        Debug.DrawRay(ray.origin, ray.direction);

        if (Physics.Raycast(ray, out hit, onlyBuildings))
        {
            if (hit.collider.GetComponent<Building>() != null && hit.collider.GetComponent<Conveyor>() != null)
            {
                direction = hit.collider.GetComponent<Conveyor>().direction;

                //Then sends another raycast to see if there's an item ahead.
                ray = new Ray(transform.position, direction);
                Debug.DrawRay(ray.origin, ray.direction);

                //If there's no item ahead, move in a linear fasion
                if (!Physics.Raycast(ray, out hit, 0.2f, onlyItems))
                {
                    StartCoroutine(Movement());
                }
                else
                    reset = true;             
            }
            else
               reset = true;           
        }
        else
            reset = true;        

        //Repeat the process until it goes into an input.
        yield return new WaitForSeconds(0.3f);
        if (reset)
            StartCoroutine(FindDirection());
    }

    public float distanceBetweenItems;

    public IEnumerator Movement()
    {
        Vector3 startPos = transform.position;
        Vector3 goalPos = transform.position + direction;

        float timePercentage = 0f;
        while (timePercentage < 1)
        {
            ray = new Ray(transform.position, direction);
            if (!Physics.Raycast(ray, out hit, distanceBetweenItems, onlyItems))
            {
                timePercentage += Time.deltaTime / speed;
                transform.position = Vector3.Lerp(startPos, goalPos, timePercentage);
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FindDirection());
    }
}

/*class testing
{
    public static List<A> Test<A>() where A : class, new()
    {
        A entry = new A();
        var cols = entry.GetType().GetProperties();

        


        return null;
    }
}*/