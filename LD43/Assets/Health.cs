using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public const int maxHeatlh = 10;
    public int currentHealth = maxHeatlh;

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Player dead");
        }
    }
    
}
