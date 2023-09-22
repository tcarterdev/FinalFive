using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{

    public float maxHealth = 100;
    public float currentHealth;
    public TMP_Text playerHealthUI;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthUI.SetText(currentHealth + "%");

        
    }

    public void TakeDamageToPlayer(float damage)
    {
        currentHealth -= damage;
    }
}
