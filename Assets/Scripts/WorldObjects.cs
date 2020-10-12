using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObjects : MonoBehaviour
{
    private int health = 300; //rock health
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
