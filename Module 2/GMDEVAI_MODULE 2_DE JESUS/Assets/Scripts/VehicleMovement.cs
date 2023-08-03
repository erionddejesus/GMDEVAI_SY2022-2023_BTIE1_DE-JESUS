using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    public Transform goal;

    public float speed = 0;
    public float rotSpeed = 1;

    public float acceleration = 5;
    public float deceleration = 5;

    public float minSpeed = 0;
    public float maxSpeed = 10;

    public float breakAngle = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //looking towards where the goal is
        Vector3 lookAtGoal = new Vector3(goal.transform.position.x,
                                        this.transform.position.y,
                                        goal.transform.position.z);

        //direction
        Vector3 direction = lookAtGoal - this.transform.position;

        //rotation
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                    Quaternion.LookRotation(direction),
                                                    Time.deltaTime * rotSpeed);

        //calculate velocity
        //speed = Mathf.Clamp(speed + (acceleration * Time.deltaTime), minSpeed, maxSpeed);

        if (Vector3.Angle(goal.forward, this.transform.forward) > breakAngle && speed > 2)
        {
            speed = Mathf.Clamp(speed - (deceleration * Time.deltaTime), minSpeed, maxSpeed);
        }
        else
        {
            speed = Mathf.Clamp(speed + (acceleration * Time.deltaTime), minSpeed, maxSpeed);
        }

        //movement
        this.transform.Translate(0, 0, speed);
    }
}
