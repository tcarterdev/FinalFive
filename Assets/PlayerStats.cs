using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;
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
}
