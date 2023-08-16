using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager manager;
    public Material frozenMaterial;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "AI" && col.gameObject.GetComponent<AIControl>().isFrozen == false)
        {
            col.gameObject.GetComponent<AIControl>().enabled = false;
            col.gameObject.GetComponent<Renderer>().material = frozenMaterial;
            col.gameObject.GetComponent<AIControl>().isFrozen = true;

            manager.numberOfFrozenAI++;
            
        }
    }
}
