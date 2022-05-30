using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public int enemiesDead;
    [SerializeField]
    public GameObject door1;
    public GameObject door2;
    public GameObject door3;
    public GameObject defeatvic;

    bool o1 = false;
    bool o2 = false;
    bool o3 = false;

    void Awake()
    {
        defeatvic.SetActive(false);
    }

    void Update()
    {
        if (enemiesDead >= 1 && !o1)
        {
            Destroy(door1);
            defeatvic.SetActive(true);
            o1 = true;
        }
        if (enemiesDead >= 3 && !o2)
        {
            Destroy(door2);
            o2 = true;
        }
        if (enemiesDead >= 4 && !o3)
        {
            Destroy(door3);
            o3 = true;
        }
    }
}
