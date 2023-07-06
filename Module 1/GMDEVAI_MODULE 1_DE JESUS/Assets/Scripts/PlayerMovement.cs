using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float xDirection = Input.GetAxis("Horizontal");
        float zDirection = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(xDirection, 0.0f, zDirection).normalized;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(move), Time.deltaTime * rotSpeed);

        transform.position += move * speed;
    }
}
