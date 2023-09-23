using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{

    public float maxHealth = 100;
    public float currentHealth;
    public float maxStamina = 100;
    public float currentStamina;
    public AudioSource playerAudioSource;
    public AudioClip playerHurtFX;

    public TMP_Text playerHealthUI;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        playerAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthUI.SetText(currentHealth + "%");

        
    }

    public void TakeDamageToPlayer(float damage)
    {
        currentHealth -= damage;
        playerAudioSource.pitch = UnityEngine.Random.Range(0.8f, 1f);
        playerAudioSource.PlayOneShot(playerHurtFX, 0.5f);
    }
}
