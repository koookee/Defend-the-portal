using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManagerUI GameUI;
    public Camera mainCamera;
    private Rigidbody playerRB;
    private int numOfRocks = 0;
    private int numOfWood = 0;
    private float playerSpeed = 2.0f;
    private float jumpForce = 5.0f;
    public int numOfJumps = 2;
    private int jumpsLeft;
    public int health = 10;
    private float weaponRange = 50f;
    public float axeRange = 70f;
    //Inventory arr
    public string[] inventory = new string[] {"Harvesting tool","Gun", "empty" , "empty" , "empty" };
    public string inventorySlotSelected = "";
    public int inventorySlotNum = 0; //inventorySlotNum is basically just the inventory array index
    public int availableSlotNum = 1; //The index of the available slot to be occupied by purchased weapons
    
    // Start is called before the first frame update
    void Start()
    {
        //GameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameUI = GameObject.Find("GameManagerUI").GetComponent<GameManagerUI>();
        inventorySlotSelected = inventory[inventorySlotNum];
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
        MoveFunc();
        JumpFunc();
        HotbarSelector();
        CheckPlayerHealth();
        ShootingFunc();
        HarvestingFunc();
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
        if (Input.GetButtonDown("Fire1") && inventorySlotSelected == "Gun")
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
    private void HarvestingFunc()
    {
        //Allows the player to damage enemies
        if (Input.GetButtonDown("Fire1") && inventorySlotSelected == "Harvesting tool")
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, axeRange))
            {
                WorldObjects worldObject = hit.transform.GetComponent<WorldObjects>();
                //Checks to see if the gameobject is an enemy/has EnemyScript as a component
                if (worldObject != null)
                {
                    switch (worldObject.objectName)
                    {
                        case ("Rock"):
                            worldObject.TakeDamage();
                            numOfRocks++;
                            Debug.Log("Rocks: " + numOfRocks);
                            break;
                        case ("Tree"):
                            worldObject.TakeDamage();
                            numOfWood++;
                            Debug.Log("Wood: " + numOfWood);
                            break;
                        default:
                            Debug.Log("Can't find object type");
                            break;
                    }
                    
                }

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
    /*
    private void ItemSelected()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gunEquipped = false;
            Debug.Log("Harvesting tool equipped");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gunEquipped = true;
            Debug.Log("Gun equipped");
        }
    }
    */
    private void CheckPlayerHealth()
    {
        if(health <= 0)
        {
            Debug.Log("Player has died!");
        }
    }
    private void RotatePlayer()
    {
        /*
        //If inventory is open, player can't rotate or move
        if (!GameManagerScript.inventoryUI.activeSelf)
        {
            //Rotates player around the Y axis
            float horizontalInput = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up, horizontalInput * Time.deltaTime * rotateSpeed);
        }
        */
    }
    
    private void HotbarSelector()
    {

        if (Input.GetAxis("Mouse ScrollWheel") > 0f && inventorySlotNum < inventory.Length - 1)
        {
            inventorySlotNum++;
            inventorySlotSelected = inventory[inventorySlotNum];

            //GameUI part
            GameUI.selectedUI[inventorySlotNum].SetActive(true); //Moves the slot border to the new selected slot
            GameUI.selectedUI[inventorySlotNum - 1].SetActive(false); //Turns off the border of the previously selected slot
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && inventorySlotNum > 0)
        {
            inventorySlotNum--;
            inventorySlotSelected = inventory[inventorySlotNum];

            //GameUI part
            GameUI.selectedUI[inventorySlotNum].SetActive(true); //Moves the slot border to the new selected slot
            GameUI.selectedUI[inventorySlotNum + 1].SetActive(false); //Turns off the border of the previously selected slot
        }

    }
    
}
