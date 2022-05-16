using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
//called health, but is actually just the whole playerside script, not worth effort to change
public class Health : MonoBehaviour {
   [SerializeField]
   public float healthBar = 10;
   public bool spiked = false;
   public TextMeshProUGUI healthDisplay;
   public TextMeshProUGUI deathMessage;
   public TextMeshProUGUI textBox;
   public TextMeshProUGUI tutorial;
   public GameObject profileTot;
   public GameObject profileBun;
   public GameObject deathScreen;
   public bool dead = false;
   public TextAsset dialogue;
   public string[][] dialogueList;
   public string[] initialD;
   private string convNum;
   List<string[]> convList;

   void Start() {
       profileTot.SetActive(false);
       profileBun.SetActive(false);
       deathScreen.SetActive(false);
       initialD = dialogue.text.Split('\n');
       dialogueList = new string[initialD.Length][];
       for (int i = 0; i < initialD.Length; i++) {
           var adding = initialD[i].Split(';');
           dialogueList[i] = adding;
       }
   }
   //For .txt reference: conversation; profilenum; text; wait time; tutorial text(optional)
   void OnTriggerEnter(Collider other) {
       
       if (other.tag == "trap") {
           spiked = true;
       } else if (other.tag != null) {
           convNum = other.tag;
           convList = new List<string[]>();
           bool found = false;
           for (int k = 0; k < dialogueList.Length; k++) {
               if (dialogueList[k][0] == convNum) {
                   convList.Add(dialogueList[k]);
                   found = true;
               } else if (found == true) {
                   break;
               }
           }
           StartCoroutine(conversations(convList));
           Destroy(other.gameObject);
   }

   void OnTriggerExit() {
       spiked = false;
   }

   void Update() {
       healthDisplay.text = "Health: " + healthBar;
       if (healthBar <= 0) {
           deathMessage.text = "You died.";
           deathScreen.SetActive(true);
           dead = true;
       }
   }
   IEnumerator conversations(List<string[]> convList) {
       for (int c = 0; c < convList.Count; c++) {
           //profile.sourceImage = spritesheet(convList[c][1]); this is bad. Needs to be done up
           textBox.text = convList[c][2];
           if (convList[c].Length == 5) {
               tutorial.text = convList[c][4];
           }
           if (convList[c][1] == "1") {
               profileTot.SetActive(true);
           } else if (convList[c][1] == "2") {
               profileBun.SetActive(true);
           }
           yield return new WaitForSeconds(float.Parse(convList[c][3]));
           textBox.text = " ";
           tutorial.text = " ";
           profileBun.SetActive(false);
           profileTot.SetActive(false);
           yield return new WaitForSeconds(.5f);
       }
   }
}
}
