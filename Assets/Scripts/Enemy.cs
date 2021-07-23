using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour
{

   // public CharacterController characterController;
    private Transform attackGoal;
    public NavMeshAgent navMeshAgent;

    private float helthPoint;
    [SerializeField]
    private int damage;

    private Animator animator;

    void Start()
    {
        attackGoal = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = transform.GetComponent<NavMeshAgent>();
        animator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(attackGoal.position);
     
        animator.SetFloat("Movement", navMeshAgent.velocity.magnitude);

        if (Input.GetKeyDown(KeyCode.I))
        {
            animator.SetTrigger("Atak1");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            helthPoint -= other.GetComponent<Bullet>().damageAtack;
            if (helthPoint <= 0) Destroy(gameObject);
        }
    }

    public int GetDamage() { return damage; }
}
