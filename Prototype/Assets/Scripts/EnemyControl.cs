using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour {
    public NavMeshAgent monster;
    public Transform bungie;
    
    public LayerMask whatIsGround, whatIsBungie;

    public int health;

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
    public GameObject drop; //your club


    void Start() {
        health = 1;
    }

    private void Awake() {
        bungie = GameObject.Find("thirdPersonPlayer").transform;
        monster = GetComponent<NavMeshAgent>();
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
        if (health == 0) Destroy(this.gameObject);
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
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    
    private void ResetAttack() {
        alreadyAttacked = false;
    }

    //when enemy gets destroyed it becomes a club
    private void OnDestroy() {
        //Instantiate(drop, Vector3(monster.x, monster.y + 1, monster.z), drop.transform.rotation);
    }
}