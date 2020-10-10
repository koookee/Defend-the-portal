using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera mainCamera;
    private Rigidbody playerRB;
    private float playerSpeed = 2.0f;
    private float jumpForce = 5.0f;
    public int numOfJumps = 2;
    private int jumpsLeft;
    public int health = 10;
    private float weaponRange = 50f;
    //Inventory arr
    public string[] inventory = new string[] {"Gun","empty", "empty" , "empty" , "empty" };
    public string inventorySlotSelected = "";
    private int inventorySlotNum = 0; //inventorySlotNum is basically just the inventory array index
    public int availableSlotNum = 1; //The index of the available slot to be occupied by purchased weapons
    


    // Start is called before the first frame update
    void Start()
    {
        //GameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        inventorySlotSelected = inventory[inventorySlotNum];
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
        MoveFunc();
        JumpFunc();
        InventorySelector();
        ShootingFunc();
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            health--;
            Destroy(other.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Checks to see if player is on the ground to reset doubleJump to 2
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpsLeft = numOfJumps;
        }
    }
    private void ShootingFunc()
    {
        //Allows the player to damage enemies
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, weaponRange))
            {
                EnemyScript enemy = hit.transform.GetComponent<EnemyScript>();
                //Checks to see if the gameobject is an enemy/has EnemyScript as a component
                if (enemy != null) enemy.TakeDamage(1);
            }
        }
    }
    private void JumpFunc()
    {
        //Takes the input to make the player jump
        if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpsLeft -= 1;
        }
    }
    
    
    private void MoveFunc()
    {
        //Makes the player move left and right
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 playerMovement = new Vector3(horizontal, 0, vertical).normalized * playerSpeed * Time.deltaTime;
        //normalized prevents player at moving at twice the speed diagonally 
        transform.Translate(playerMovement, Space.Self);
    }
    private void RotatePlayer()
    {
        /*
        //If inventory is open, player can't rotate
        if (!GameManagerScript.inventoryUI.activeSelf)
        {
            //Rotates player around the Y axis
            float horizontalInput = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up, horizontalInput * Time.deltaTime * rotateSpeed);
        }
        */
    }
    
    private void InventorySelector()
    {
        /*
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && inventorySlotNum < inventory.Length - 1)
        {
            inventorySlotNum++;
            inventorySlotSelected = inventory[inventorySlotNum];
            Debug.Log(inventorySlotNum); Debug.Log(inventorySlotSelected);
            GameManagerScript.selectedUI[inventorySlotNum].SetActive(true); //Moves the slot border to the new selected slot
            GameManagerScript.selectedUI[inventorySlotNum - 1].SetActive(false);//Turns off the border of the previously selected slot
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && inventorySlotNum > 0)
        {
            inventorySlotNum--;
            inventorySlotSelected = inventory[inventorySlotNum];
            Debug.Log(inventorySlotNum); Debug.Log(inventorySlotSelected);
            GameManagerScript.selectedUI[inventorySlotNum].SetActive(true);
            GameManagerScript.selectedUI[inventorySlotNum + 1].SetActive(false);
        }
        */
    }
}
