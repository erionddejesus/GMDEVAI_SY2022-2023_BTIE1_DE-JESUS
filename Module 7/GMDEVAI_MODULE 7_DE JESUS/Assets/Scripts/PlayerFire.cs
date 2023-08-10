using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : TankAI
{
    //Animator anim;
    public GameObject[] enemy;
    public float cooldown = 0.1f;
    public float cooldownCountdown = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);

            for (int i = 0; i <enemy.Length; i++)
            {
                enemy[i].GetComponent<Animator>().SetBool("playerIsDead", true);
            }
        }

        else
        {
            if (cooldownCountdown <= 0.0f)
            {
                if (RightMouseButtonClicked())
                {
                    base.Fire();
                    cooldownCountdown = cooldown;
                }
            }
            else
            {

                cooldownCountdown -= Time.deltaTime;
                //base.StopFiring();
            }
        }
    }

    bool RightMouseButtonClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
