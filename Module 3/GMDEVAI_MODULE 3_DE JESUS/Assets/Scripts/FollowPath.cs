using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    Transform goal;

    public float speed = 10.0f;
    float accuracy = 1.0f;
    public float rotSpeed = 7.0f;

    public GameObject wpManager;

    GameObject[] wps;
    GameObject currentNode;

    int currentWaypointIndex = 0;

    Graph graph;

    // Start is called before the first frame update
    void Start()
    {
        wps = wpManager.GetComponent<WaypointManager>().waypoints;
        graph = wpManager.GetComponent<WaypointManager>().graph;
        currentNode = wps[26];

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (graph.getPathLength() == 0 || currentWaypointIndex == graph.getPathLength())
        {
            return;
        }

        //the node we are closest to at the moment
        currentNode = graph.getPathPoint(currentWaypointIndex);

        //if we are close enough to the current waypoint, move to the next one
        if (Vector3.Distance(graph.getPathPoint(currentWaypointIndex).transform.position,
            transform.position) < accuracy)
        {
            currentWaypointIndex++;
        }

        //if we are not at the end of the path
        if (currentWaypointIndex < graph.getPathLength())
        {
            goal = graph.getPathPoint(currentWaypointIndex).transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x, transform.position.y, goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                        Quaternion.LookRotation(direction),
                                                        Time.deltaTime * rotSpeed);

            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }

    //HELIPAD
    public void GoToHelipad()
    {
        graph.AStar(currentNode, wps[13]);
        currentWaypointIndex = 0;
    }

    //RUINS
    public void GoToRuins()
    {
        graph.AStar(currentNode, wps[5]);
        currentWaypointIndex = 0;
    }

    //FACTORY
    public void GoToFactory()
    {
        graph.AStar(currentNode, wps[3]);
        currentWaypointIndex = 0;
    }

    //TWIN MOUTAINS
    public void GoToTwinMountains()
    {
        graph.AStar(currentNode, wps[21]);
        currentWaypointIndex = 0;
    }

    //BARRACKS
    public void GoToBarracks()
    {
        graph.AStar(currentNode, wps[9]);
        currentWaypointIndex = 0;
    }

    //COMMAND CENTER
    public void GoToCommandCenter()
    {
        graph.AStar(currentNode, wps[25]);
        currentWaypointIndex = 0;
    }

    //OIL REFINERY PUMPS
    public void GoToOilRefineryPumps()
    {
        graph.AStar(currentNode, wps[7]);
        currentWaypointIndex = 0;
    }
    //TANKERS
    public void GoToTankers()
    {
        graph.AStar(currentNode, wps[15]);
        currentWaypointIndex = 0;
    }

    //RADAR
    public void GoToRadar()
    {
        graph.AStar(currentNode, wps[19]);
        currentWaypointIndex = 0;
    }

    //COMMAND POST
    public void GoToCommandPost()
    {
        graph.AStar(currentNode, wps[14]);
        currentWaypointIndex = 0;
    }

    //MIDDLE
    public void GoToMiddle()
    {
        graph.AStar(currentNode, wps[22]);
        currentWaypointIndex = 0;
    }
}
