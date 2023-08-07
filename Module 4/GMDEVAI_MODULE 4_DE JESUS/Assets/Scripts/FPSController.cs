using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    public float speed = 1.5f;
    public float rotSpeed = 1.5f;

    public Camera playerCamera;

    public float mouseSensitivity = 2f;
    float cameraVerticalRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xDirection = Input.GetAxis("Horizontal");
        float zDirection = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(xDirection, 0.0f, zDirection).normalized;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(move), Time.deltaTime * rotSpeed);

        transform.position += move * speed;
    }
}
