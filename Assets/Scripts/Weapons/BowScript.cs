using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScript : MonoBehaviour
{
    public GameObject arrow;
    public GameObject arrowPos; //Empty game object that specifies the arrow's position
    public Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject arr = Instantiate(arrow, arrowPos.transform.position, mainCamera.transform.rotation);
            arr.GetComponent<Rigidbody>().AddForce(arr.transform.forward * 10, ForceMode.Impulse);
        }
    }
}
