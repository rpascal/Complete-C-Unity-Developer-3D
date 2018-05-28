using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    [SerializeField] int playerHealth = 10;
    [SerializeField] int healthDecrease = 1;
    [SerializeField] Text healthText;
    [SerializeField] AudioClip PlayerDamageSFX;


    private void Start() {
        healthText.text = playerHealth.ToString();
    }

    private void OnTriggerEnter(Collider other) {
        GetComponent<AudioSource>().PlayOneShot(PlayerDamageSFX);
        playerHealth -= healthDecrease;
        healthText.text = playerHealth.ToString();
    }

}
