using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour
{

   // public CharacterController characterController;
    public Transform attackGoal;
    public NavMeshAgent navMeshAgent;

    public float helthPoint;


    void Start()
    {
   //     characterController = transform.GetComponent<CharacterController>();
        attackGoal = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = transform.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(attackGoal.position);
        // print(navMeshAgent.);
       // navMeshAgent.Move(Vector3.one);
       // characterController.Move((transform.rotation * navMeshAgent.nextPosition).normalized * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            helthPoint -= other.GetComponent<Bullet>().damageAtack;
            if (helthPoint <= 0) Destroy(gameObject);
        }
    }
}
