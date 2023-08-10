using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAI : MonoBehaviour
{
    Animator anim;
    public GameObject player;

    public GameObject bullet;
    public GameObject turret;

    public float health = 100;

    public GameObject GetPlayer()
    {
        return player;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.gameObject != null)
        {
            anim.SetFloat("distance", Vector3.Distance(transform.position, player.transform.position));
        }
        

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Fire()
    {
        if (this.gameObject != null || player.gameObject != null)
        {
            GameObject b = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
            b.GetComponent<Rigidbody>().AddForce(turret.transform.forward * 500);
        }
        else
        {
            StopFiring();
        }
    }

    public void StopFiring()
    {
        CancelInvoke("Fire");
    }

    public void StartFiring()
    {
        if (player.gameObject == null)
        {
            StopFiring();
        }
        else
        {
            InvokeRepeating("Fire", 0.5f, 0.5f);
        }
    }
}
