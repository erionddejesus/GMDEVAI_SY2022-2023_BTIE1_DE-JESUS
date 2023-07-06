using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelForwardToGoal : MonoBehaviour
{
    public Transform goal;
    public float speed = 3;
    public float rotSpeed = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);

        //transform.LookAt(lookAtGoal);

        //Spherical Linear Interpolation
        Vector3 direction = lookAtGoal - transform.position;

        //slowly turns the player to the direction of the goal
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);

        //distance function of vector 3
        if (Vector3.Distance(lookAtGoal, transform.position) > 1.5)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }
}
