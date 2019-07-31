using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceScript : MonoBehaviour {

    public ItemList Item;
    public float speed = 3;
    public Vector3 direction;
    Ray ray;
    RaycastHit hit;
    public LayerMask onlyItems;
    public LayerMask onlyBuildings;
    //TO-DO: Add a hierarchy manager object

    //The item instantiates
    void Start()
    {
        StartCoroutine(FindDirection());
    }



    IEnumerator FindDirection()
    {
        //The item sends a raycast to the current building it's on and finds out the direction
        ray = new Ray(transform.position + transform.up + direction/2.5f, -transform.up);
        Debug.DrawRay(ray.origin, ray.direction);

        if (Physics.Raycast(ray, out hit, onlyBuildings))
        {
            if (hit.collider.GetComponent<Building>() != null)
            {
                direction = hit.collider.GetComponent<Building>().direction;

                //Then sends another raycast to see if there's an item ahead.
                ray = new Ray(transform.position, direction);
                Debug.DrawRay(ray.origin, ray.direction);

                //If there is a direction and there's no item ahead, move in a linear fasion
                if (!Physics.Raycast(ray, out hit, 0.2f, onlyItems))
                {
                    Vector3 startPos = transform.position;
                    Vector3 goalPos = transform.position + direction;

                    float timePercentage = 0f;
                    while (timePercentage < 1)
                    {
                        ray = new Ray(transform.position, direction);
                        if (!Physics.Raycast(ray, out hit, 0.2f, onlyItems))
                        {
                            timePercentage += Time.deltaTime / speed;
                            transform.position = Vector3.Lerp(startPos, goalPos, timePercentage);
                        }
                        yield return null;
                    }
                }
            }
        }

        //Repeat the process until it goes into an input.
        yield return new WaitForSeconds(0.1f);
        yield return StartCoroutine(FindDirection());
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