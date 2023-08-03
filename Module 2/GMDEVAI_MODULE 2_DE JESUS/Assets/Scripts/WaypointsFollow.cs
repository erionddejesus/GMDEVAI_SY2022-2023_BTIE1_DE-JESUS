using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsFollow : MonoBehaviour
{
    //comment out since we using unity circuit
    //public GameObject[] waypoints;
    public UnityStandardAssets.Utility.WaypointCircuit circuit;

    int currentWaypointIndex = 0;

    public float speed = 5;
    public float rotSpeed = 3;
    float accuracy = 1;

    // Start is called before the first frame update
    void Start()
    {
        //waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (circuit.Waypoints.Length == 0) return;

        GameObject currentWaypoint = circuit.Waypoints[currentWaypointIndex].gameObject;

        //looking towards where the goal is
        Vector3 lookAtGoal = new Vector3(currentWaypoint.transform.position.x,
                                        this.transform.position.y,
                                        currentWaypoint.transform.position.z);

        //direction
        Vector3 direction = lookAtGoal - this.transform.position;

        //if near enough to the waypoint
        if (direction.magnitude < 1.0f)
        {
            currentWaypointIndex++;

            if (currentWaypointIndex >= circuit.Waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                    Quaternion.LookRotation(direction),
                                                    Time.deltaTime * rotSpeed);

        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
