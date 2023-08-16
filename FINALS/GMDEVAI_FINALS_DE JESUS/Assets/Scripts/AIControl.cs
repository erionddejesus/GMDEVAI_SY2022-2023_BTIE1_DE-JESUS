using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    public GameManager manager;

    public Material nonFrozenMaterial;

    NavMeshAgent agent;

    public GameObject target;

    public int agentType;
    public float range = 20;

    public bool isFrozen = false;

    public FirstPersonMovement playerMovement;

    Vector3 wanderTarget;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        playerMovement = target.GetComponent<FirstPersonMovement>();
    }

    void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    void Flee(Vector3 location)
    {
        Vector3 fleeDirection = location - this.transform.position;

        agent.SetDestination(this.transform.position - fleeDirection);
    }

    void Pursue()
    {
        Vector3 targetDirection = target.transform.position - this.transform.position;
        float lookAhead = targetDirection.magnitude / (agent.speed + playerMovement.speed);

        Seek(target.transform.position + target.transform.forward * lookAhead);
    }

    void Evade()
    {
        Vector3 targetDirection = target.transform.position - this.transform.position;
        float lookAhead = targetDirection.magnitude / (agent.speed + playerMovement.speed);

        Flee(target.transform.position + target.transform.forward * lookAhead);
    }

    void Wander()
    {
        float wanderRadius = 20;
        float wanderDistance = 10;
        float wanderJitter = 1;

        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, 0, Random.Range(-1.0f, 1.0f) * wanderJitter);
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        //transform into world space vector
        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = this.gameObject.transform.InverseTransformVector(targetLocal);

        Seek(targetWorld);
    }

    void Hide()
    {
        float distance = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;

        int hidingSpotsCount = World.Instance.GetHidingSpots().Length;

        for (int i = 0; i < hidingSpotsCount; i++)
        {
            Vector3 hideDirection = World.Instance.GetHidingSpots()[i].transform.position - target.transform.position;
            Vector3 hidePosition = World.Instance.GetHidingSpots()[i].transform.position + hideDirection.normalized * 5; //distance offset

            float spotDistance = Vector3.Distance(this.transform.position, hidePosition);

            if (spotDistance < distance)
            {
                chosenSpot = hidePosition;
                distance = spotDistance;
            }
        }

        Seek(chosenSpot);
    }

    void CleverHide()
    {
        float distance = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        Vector3 chosenDirection = Vector3.zero;
        GameObject chosenGameObject = World.Instance.GetHidingSpots()[0];

        int hidingSpotsCount = World.Instance.GetHidingSpots().Length;

        for (int i = 0; i < hidingSpotsCount; i++)
        {
            Vector3 hideDirection = World.Instance.GetHidingSpots()[i].transform.position - target.transform.position;
            Vector3 hidePosition = World.Instance.GetHidingSpots()[i].transform.position + hideDirection.normalized * 5; //distance offset

            float spotDistance = Vector3.Distance(this.transform.position, hidePosition);

            if (spotDistance < distance)
            {
                chosenSpot = hidePosition;
                chosenDirection = hideDirection;
                chosenGameObject = World.Instance.GetHidingSpots()[i];
                distance = spotDistance;
            }
        }

        Collider hideCol = chosenGameObject.GetComponent<Collider>();
        Ray back = new Ray(chosenSpot, -chosenDirection.normalized);
        RaycastHit info;
        float rayDistance = 75.0f;
        hideCol.Raycast(back, out info, rayDistance);

        Seek(info.point + chosenDirection.normalized * 5);
    }

    bool canSeeTarget()
    {
        RaycastHit raycastInfo;
        Vector3 rayToTarget = target.transform.position - this.transform.position;

        if (Physics.Raycast(this.transform.position, rayToTarget, out raycastInfo))
        {
            return raycastInfo.transform.gameObject.tag == "Player";
        }

        return false;
    }

    private void OnCollisionEnter(Collision col)
    {
        //AI unfreeze other AI
        if (col.gameObject.tag == "AI" && col.gameObject.GetComponent<AIControl>().enabled == false)
        {
            Debug.Log("AI colliding with each other");
            col.gameObject.GetComponent<AIControl>().enabled = true;
            col.gameObject.GetComponent<AIControl>().isFrozen = false;
            col.gameObject.GetComponent<Renderer>().material = nonFrozenMaterial;

             manager.numberOfFrozenAI--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isFrozen == false)
        {
            Wander();

            //pursue
            if (agentType == 1)
            {
                float spotDistance = Vector3.Distance(this.transform.position, target.transform.position);

                if (spotDistance < range)
                {
                    Pursue();
                }
            }
            //hide
            else if (agentType == 2)
            {
                float spotDistance = Vector3.Distance(this.transform.position, target.transform.position);

                //within range
                if (spotDistance < range)
                {
                    //within line of sight
                    if (canSeeTarget())
                    {
                        CleverHide();
                    }
                }
            }
            else if (agentType == 3)
            {
                float spotDistance = Vector3.Distance(this.transform.position, target.transform.position);

                if (spotDistance < range)
                {
                    Evade();
                }
            }
        }
    }
}
