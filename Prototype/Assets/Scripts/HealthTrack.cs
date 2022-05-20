using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class HealthTrack : MonoBehaviour
{
    public float healthBar = 10;
   public bool spiked = false;
   public TextMeshProUGUI healthDisplay;
   public TextMeshProUGUI deathMessage;
   public GameObject deathScreen;
   public bool dead;

    void Start() {
        Debug.Log("here");
        deathScreen.SetActive(false);
    }

    void FixedUpdate() {
       healthDisplay.text = "Health: " + healthBar;
       if (healthBar <= 0) {
           deathMessage.text = "You died.";
           deathScreen.SetActive(true);
           dead = true;
       }
   }

   void OnTriggerEnter(Collider other) {
       if (other.tag == "trap") {
           spiked = true;
            //any other tags will have to be put before the !=null, cause otherwise it will auto delete them
       }
   }

    void OnTriggerExit() {
       spiked = false;
   }


}
