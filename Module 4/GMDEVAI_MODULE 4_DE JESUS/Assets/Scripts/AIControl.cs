using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControl : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;

    public Transform targetPlayer;
    public float speed = 20;
    public float rotSpeed = 15;

    // Start is called before the first frame update
    void Start()
    {
        this.agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 lookAtPlayer = new Vector3(targetPlayer.position.x, targetPlayer.transform.position.y, targetPlayer.position.z);
        this.agent.SetDestination(lookAtPlayer);

        /*
        Vector3 lookAtPlayer = new Vector3(targetPlayer.position.x, this.transform.position.y, targetPlayer.position.z);

        //Spherical Linear Interpolation
        Vector3 direction = lookAtPlayer - transform.position;

        //slowly turns the player to the direction of the goal
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);

        //distance function of vector 3
        if (Vector3.Distance(lookAtPlayer, transform.position) > 1.5)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        */

    }
}
