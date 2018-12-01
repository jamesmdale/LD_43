using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public const int maxHeatlh = 10;
    public int currentHealth = maxHeatlh;
    public Text uiText;

    void Start()
    {
        GameObject textCanvas = GameObject.Find("HealthText");
        uiText = textCanvas.GetComponent<Text>(); 
        
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Player dead");
        }

        if (uiText)
            uiText.text = currentHealth.ToString();
    }
    
}
