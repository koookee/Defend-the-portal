using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEditor.UI;

public class GameManagerUI : MonoBehaviour
{
    private PlayerController Player;
    private SpawnManager SpawnManagerScript;

    //Crafting UI
    public GameObject[] CraftingPanels; //Weapons, ammo, etc.
    private int panelCurrentIndex = 0; //Index of the current crafting panel
    public bool isCraftingToggled = false;
    public GameObject craftingUI;
    public bool isCraftingUIActive = false;

    public bool gunPurchased = false;           //Gun
    public GameObject gunImage; //What was this used for again?
    public GameObject gunPrice;
    public GameObject gunCheckmark;

    public bool bowPurchased = false;           //Bow
    public GameObject bowImage; //What was this used for again?
    public GameObject bowPrice;
    public GameObject bowCheckmark;

    //Hotbar UI
    public Sprite gunSprite;
    public Sprite bowSprite;
    public UnityEngine.UI.Image[] slotsUI; //Image components for each hotbar slot
    public GameObject[] selectedUI; //Border around each hotbar slot. They're changed in the player controller script

    // Start is called before the first frame update
    void Start()
    {
        SpawnManagerScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        Player = GameObject.Find("Player").GetComponent<PlayerController>();
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        slotsUI[1].enabled = false; //Turns off the image component of the empty slots
        slotsUI[2].enabled = false;
        slotsUI[3].enabled = false;
        slotsUI[4].enabled = false;
    }
    public void ToggleCraftingUI()
    {
        isCraftingToggled = !isCraftingToggled;
        craftingUI.SetActive(isCraftingToggled);
        UnityEngine.Cursor.visible = isCraftingToggled;
    }

    public void SwitchCraftingPanel(int direction) //direction is either 1 or -1. 
    {
        CraftingPanels[panelCurrentIndex].SetActive(false);
        panelCurrentIndex += direction;
        CraftingPanels[panelCurrentIndex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        HotbarUI();
    }

    private void HotbarUI()
    {
        if (Player.inventorySlotSelected == "Gun")
        {
            
        }
        if (Player.inventorySlotSelected == "Rocket Launcher")
        {
            
        }
    }

    /*

{
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI roundNum;
    public TextMeshProUGUI roundsSurvived;
    
    
    public GameObject rocketCheckmarkImage;
    
    

    //Using Image instead of GameObject doesn't make it show up in the GameManager
    //game object like TextMeshProUGUI does

    public GameObject rayGunImage;
    
    public GameObject crossHair;
    public GameObject weaponsUI;
    public GameObject restartButton;
    public bool isGameOver = false;
    
    

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerController>();
        mainCam.enabled = true;
        deathCam.enabled = false;
        //Cursor.visible also works but I'm getting an ambiguous error between UnityEngine
        //and UnityEngine.UI
        UnityEngine.Cursor.visible = false;
        //Keeps the mouse in the playmode area
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        

    }

    // Update is called once per frame

    void Update()
    {
        displayUI();
        InventoryUI();
        roundUI();
    }

    
    
    
    private void roundUI()
    {
        //Displays round info, pop up messages, and anything to do with the 
        //changing state of the game
        roundNum.text = "Round: " + SpawnManager.roundCounter;
        if (doubleJumpStatus.gameObject.activeSelf == true) StartCoroutine(Timer(4,1));
        gemsUI.text = "Gems: " + Player.gems;
        if (Input.GetKeyDown(KeyCode.E))
        {
            //If the inventory UI is already visible and the player presses E,
            //turn off the UI. This is a shortcut to pressing on the close button
            if (inventoryUI.activeSelf)
            {
                inventoryUI.SetActive(false);
                UnityEngine.Cursor.visible = false;
            }
            else
            {
                UnityEngine.Cursor.visible = true;
                inventoryUI.SetActive(true);
            }
        }
    }
    
    
    private void RestartGameUI()
    {
        //Turns off all UI first
        playerHealthText.gameObject.SetActive(false);
        playerShieldText.gameObject.SetActive(false);
        playerGroundShatterText.gameObject.SetActive(false);
        roundNum.gameObject.SetActive(false);
        rayGunImage.gameObject.SetActive(false);
        rocketLauncherImage.gameObject.SetActive(false);
        crossHair.gameObject.SetActive(false);
        weaponsUI.gameObject.SetActive(false);
        gemsUI.gameObject.SetActive(false);
        //Turns on restart screen UI
        roundsSurvived.gameObject.SetActive(true);
        roundsSurvived.text = "Rounds survived: " + (SpawnManager.roundCounter - 1);
        restartButton.gameObject.SetActive(true);
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        //Turns off all UI first
        playerHealthText.gameObject.SetActive(true);
        playerShieldText.gameObject.SetActive(true);
        playerGroundShatterText.gameObject.SetActive(true);
        roundNum.gameObject.SetActive(true);
        rayGunImage.gameObject.SetActive(true);
        rocketLauncherImage.gameObject.SetActive(true);
        crossHair.gameObject.SetActive(true);
        weaponsUI.gameObject.SetActive(true);
        gemsUI.gameObject.SetActive(false);
        //Turns on restart screen UI
        roundsSurvived.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }
    IEnumerator Timer(int time, int section)
    {
        //Manages different commands in one coroutine split up into sections 
        //If you want to execute a few lines of code for a few seconds,
        //add a new section and pass that parameter.
        //This is more organized than having multiple IEnumerator functions
        if (section == 1)
        {
            yield return new WaitForSeconds(time);
            doubleJumpStatus.gameObject.SetActive(false);
        }
    }
}


     * */


}
