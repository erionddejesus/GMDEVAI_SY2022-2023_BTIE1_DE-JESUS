using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public GameObject explosion;
	
	public void OnCollisionEnter(Collision col)
    {
    	GameObject e = Instantiate(explosion, this.transform.position, Quaternion.identity);
    	Destroy(e,1.5f);
    	Destroy(this.gameObject);

		if (col.gameObject.tag == "Player")
        {
			Debug.Log("collided with player");
			col.gameObject.GetComponent<PlayerFire>().health -= 10;
		}

		if (col.gameObject.tag == "enemy")
		{
			Debug.Log("collided with enemy");
			col.gameObject.GetComponent<TankAI>().health -= 20;
			col.gameObject.GetComponent<Animator>().SetFloat("health", col.gameObject.GetComponent<TankAI>().health);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
