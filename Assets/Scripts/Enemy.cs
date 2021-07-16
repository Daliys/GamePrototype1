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
        attackGoal = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = transform.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(attackGoal.position);
    
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
