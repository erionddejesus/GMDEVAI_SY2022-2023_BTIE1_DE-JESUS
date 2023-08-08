using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public GameObject[] obstacle;
    GameObject[] agents;

    // Start is called before the first frame update
    void Start()
    {
        agents = GameObject.FindGameObjectsWithTag("agent");
    }

    // Update is called once per frame
    void Update()
    {
        //left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                Instantiate(obstacle[0], hit.point, obstacle[0].transform.rotation);

                foreach (GameObject a in agents)
                {
                    a.GetComponent<AIControl>().DetectNewObstacleFlee(hit.point);
                }
            }
        }
        //right mouse button
        else if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                Instantiate(obstacle[1], hit.point, obstacle[1].transform.rotation);

                foreach (GameObject a in agents)
                {
                    a.GetComponent<AIControl>().DetectNewObstacleFlock(hit.point);
                }
            }
        }
    }
}
