using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    int rotationSpeed = 2;
    public Transform Target, Player;
    float horizontalRotation, verticalRotation;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        RotationController();
    }
    void RotationController()
    {
        horizontalRotation += Input.GetAxis("Mouse X") * rotationSpeed;
        verticalRotation -= Input.GetAxis("Mouse Y") * rotationSpeed;
        //Limits the player's vertical rotation
        verticalRotation = Mathf.Clamp(verticalRotation, -23, 12);
        transform.LookAt(Target);
        //Rotates camera and target up and down
        Target.rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);
        //Rotates the player left and right
        Player.rotation = Quaternion.Euler(0, horizontalRotation, 0);
    }
}
