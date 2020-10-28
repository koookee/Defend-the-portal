using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorldObjects : MonoBehaviour
{
    public string objectName;
    public string type; //Metal, wood, stone
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForHealth();
    }
    public void TakeDamage()
    {
        health--;
    }
    private void CheckForHealth()
    {
        if (health <= 0) Destroy(gameObject);
    }
}
