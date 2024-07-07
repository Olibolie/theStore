using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    public float sensX;
    public float sensY;

    public Transform orientation;
    public Transform cameraPosition;

    public float xRotation; 
    public float yRotation;

    Ray ray;

    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    private void Update()
    {
        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        //max rotation 
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        RaycastHit hit;

        if (Physics.Raycast(cameraPosition.position, transform.forward, out hit, 100))
        {
            Debug.DrawRay(cameraPosition.position, transform.forward * 10, Color.red);
           // Debug.Log(hit.collider);
        } else
        {
            Debug.DrawRay(cameraPosition.position, transform.forward * 10, Color.green);
        }
    }
}
