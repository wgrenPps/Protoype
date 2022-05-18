using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class big_collide : MonoBehaviour
{
    [SerializeField]
    public GameObject trapSpike;
    public GameObject player;

    private Health helth;
    private bool spikesActive = false;
    private bool collided = false;
    private bool stabbed = false;

    public float cooldown;
    // Start is called before the first frame update
    void Start() {
        trapSpike.SetActive(false);
        cooldown = 0f;
        helth = player.GetComponent<Health>();
    }
    void OnTriggerEnter(Collider other)
    {
        collided = true;
    }

    void OnTriggerExit(Collider other) {
        collided = false;
    }

    void Update() {
        if (cooldown > 0) {
            cooldown -= Time.deltaTime;
        }
        if (cooldown < 0) {
            cooldown = 0f;
        }
        if (cooldown == 0 && collided == true)
        {
            StartCoroutine(spikey());
            cooldown = 5f;
            stabbed = false;
        }
        if (spikesActive && collided && helth.spiked && stabbed == false && !helth.dead) {
            helth.healthBar--;
            stabbed = true;
        }
    }

    IEnumerator spikey() {
        yield return new WaitForSeconds(1f);
        trapSpike.SetActive(true);
        spikesActive = true;
        yield return new WaitForSeconds(1f);
        trapSpike.SetActive(false);
        spikesActive = false;
    }
}
