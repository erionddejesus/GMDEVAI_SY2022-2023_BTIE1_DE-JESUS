using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    GameObject[] goalLocations;
    NavMeshAgent agent;
    Animator animator;

    float speedMultiplier;

    float detectionRadius = 20;
    float fleeRadius = 5;

    float flockRadius = 10;

    void ResetAgent()
    {
        speedMultiplier = Random.Range(0.1f, 1.5f);
        agent.speed = 2 * speedMultiplier;
        agent.angularSpeed = 120;
        animator.SetFloat("speedMultiplier", speedMultiplier);
        animator.SetTrigger("isWalking");
        agent.ResetPath();
    }

    // Start is called before the first frame update
    void Start()
    {
        goalLocations = GameObject.FindGameObjectsWithTag("goal");
        agent = this.GetComponent<NavMeshAgent>();
        agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        animator = this.GetComponent<Animator>();
        animator.SetFloat("wOffset", Random.Range(0, 1));
        ResetAgent();  
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (agent.remainingDistance < 1)
        {
            ResetAgent();
            agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        }
    }

    //detect new obstacle flee
    public void DetectNewObstacleFlee(Vector3 location)
    {
        if (Vector3.Distance(location, this.transform.position) < detectionRadius)
        {
            Vector3 fleeDirection = (this.transform.position - location).normalized;
            Vector3 newGoal = this.transform.position + fleeDirection * fleeRadius;

            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(newGoal, path);

            if (path.status != NavMeshPathStatus.PathInvalid)
            {
                agent.SetDestination(path.corners[path.corners.Length - 1]);
                animator.SetTrigger("isRunning");
                agent.speed = 10;
                agent.angularSpeed = 500;
            }
        }
    }

    //detect new obstacle flock
    public void DetectNewObstacleFlock(Vector3 location)
    {
        if (Vector3.Distance(location, this.transform.position) < detectionRadius)
        {
            Vector3 fleeDirection = (this.transform.position - location).normalized;
            Vector3 newGoal = this.transform.position + fleeDirection * flockRadius;

            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(newGoal, path);

            if (path.status != NavMeshPathStatus.PathInvalid)
            {
                agent.SetDestination(location);
                animator.SetTrigger("isRunning");
                agent.speed = 10;
                agent.angularSpeed = 500;
            }
        }
    }
}
