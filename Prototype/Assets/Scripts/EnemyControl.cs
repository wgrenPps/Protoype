using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour {
    public NavMeshAgent monster;
    public Transform bungie;
    
    public LayerMask whatIsGround, whatIsBungie;

    public int enemyHealth;

    //wandering
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool bungieInSightRange, bungieInAttackRange;

    //item drops stuff
    //public GameObject drop; //coin GameObject

    //Player Health 
    ThirdPersonMovement TPM;
    Vector3 pause;
    bool stopped;
    bool stopP2;


    void Start() {
        enemyHealth = 5;
        TPM = bungie.GetComponent<ThirdPersonMovement>();
    }

    private void Awake() {
        bungie = GameObject.Find("thirdPersonPlayer").transform;
        monster = GetComponent<NavMeshAgent>();
        stopP2 = false;
    }
   

    private void Update() {
        //Check for sight and attack range:
        bungieInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsBungie);
        bungieInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsBungie);

        if (!bungieInSightRange && !bungieInAttackRange) Wander();
        if (bungieInSightRange && !bungieInAttackRange) Chase();
        if (bungieInAttackRange && bungieInSightRange) Attack();
    }

    private void FixedUpdate() {
        if (enemyHealth == 0) Destroy(this.gameObject);
        if (stopped == false && stopP2 == true) {
            pause = bungie.position;
            Debug.Log("GetPause");
            stopped = true;
            } else if (stopped == true && stopP2 == true) {
                bungie.position = pause;
                Debug.Log("Paused" + pause);
            }
    }
    
    private void Wander() {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet) {
            monster.SetDestination(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        
        //reached the walkPoint:
        if (distanceToWalkPoint.magnitude < 1f) {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint() {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) {
            walkPointSet = true;
        }
    }

    private void Chase() {
        monster.SetDestination(bungie.position);
    }

    private void Attack() {
        monster.SetDestination(transform.position);

        if (!alreadyAttacked) {
            TPM.bungieHP --;
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

        //if health is less than or equal to 0, stop movement
        if (TPM.bungieHP <= 0) {
            Debug.Log("You Died");
            //SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
            stopped = false;
            stopP2 = true;

        }
    }
    private void ResetAttack() {
        alreadyAttacked = false;
    }

    //when enemy gets destroyed it becomes a coin
    /*private void OnDestroy() {
        Instantiate(drop, transform.position, drop.transform.rotation);
    }*/
}