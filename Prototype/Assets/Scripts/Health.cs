using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    public float healthBar = 10;
    public bool spiked = false;

    void OnTriggerEnter() {
        spiked = true;
    }

    void OnTriggerExit() {
        spiked = false;
    }
}
