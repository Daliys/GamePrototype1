using System.Collections;
using System.ComponentModel;
using System.Configuration;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
    public float speed;
    public float turnSmothTime = 0.1f;
    public float turnSmothVelocity;
    private new Camera camera;
    public Animator animator;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float speedMovementAnimstion;
    public CharacterController characterController;

  //  public Vector3 mousePosition;

    private Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGround;

    public Transform playerSpawner;
    private float dashSpeedMult = 1;

    private bool isInvulnerable = false;

    public delegate void HelthChanger(int i);
    public static event HelthChanger onHelthChange;
    
 
    [SerializeField]
    private int helthPoint;

    private void Start()
    {
        //animator = GetComponent<Animator>();
        camera = Camera.main;
   
    }


    private void Update()
    {
        //mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        //print(Input.mousePosition);

      

        /*
        Vector3 worldPosition;
        Plane plane = new Plane(Vector3.up, 0);

        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            worldPosition = ray.GetPoint(distance);
           // print(worldPosition);
            //worldPosition.y = transform.position.y + transform.localScale.y;
            Vector2 lookDir = new Vector2(worldPosition.x, worldPosition.z) - new Vector2(transform.position.x, transform.position.z);

            float angle = Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg;// + 180f;
            transform.eulerAngles = new Vector3(0, angle, 0);
           // transform.LookAt(worldPosition);
        }*/

        Vector2 lookDir = new Vector2(Utils.mouseWorldPosition.x, Utils.mouseWorldPosition.z) - new Vector2(transform.position.x, transform.position.z);
        float angleToMousePosition = Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg;// + 180f;
        transform.eulerAngles = new Vector3(0, angleToMousePosition, 0);


        // jumping
        /*
        isGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGround && velocity.y < 0)
        {
            velocity.y = -2f;
           // animator.SetBool("Jump", false);
        }

        if (Input.GetButtonDown("Jump") && isGround)
        {
        //    animator.SetBool("Jump", true);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        */


        //if (Game.isPause) return;


        float movementHorizontal = Input.GetAxis("Horizontal");
        float movementVertical = Input.GetAxis("Vertical");


     //   if (Mathf.Abs(movementHorizontal) + Mathf.Abs(movementVertical) > 0) animator.SetBool("isRanning", true);
      //  else animator.SetBool("isRanning", false);


        velocity.y += gravity * Time.deltaTime;

        Vector3 direction = new Vector3(movementHorizontal, 0, movementVertical).normalized;
        //Vector3 direction = new Vector3(movementHorizontal, 0, movementVertical);

        Vector3 moveDirection = Vector3.zero;
        animator.SetFloat("Movement", direction.magnitude);
        if (direction.magnitude >= 0.05f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmothVelocity, turnSmothTime);
            //transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //print(movementHorizontal + "   " + moveDirection.x);

        }

        Vector3 move = moveDirection * (speed * dashSpeedMult);


        if (Input.GetButtonDown("Dash") && dashSpeedMult == 1f){
            StartCoroutine(DashTimer());
        }
        move.y = velocity.y;

        //transform.position += move*Time.deltaTime;
     if(!isDead)
        characterController.Move(move * Time.deltaTime);
       

    }

 
    IEnumerator DashTimer()
    {
        
            dashSpeedMult = 8;
        yield return new WaitForSecondsRealtime(0.1f);
        dashSpeedMult = 1f;
    }

    private bool isDead;
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !isInvulnerable)
        {
            helthPoint -= other.gameObject.GetComponent<Enemy>().GetDamage();
            helthPoint = helthPoint < 0 ? (0) : (helthPoint);
            onHelthChange?.Invoke(helthPoint);
            if (helthPoint != 0) StartCoroutine(InvulnerableTimer(2));
            else StartCoroutine(DeathPlayer());
        }

        if (other.CompareTag("HelthBuff"))
        {
            int addedHelthCount = other.gameObject.GetComponent<HelthBuff>().GetHelthBuffAddedCount();
            if ((helthPoint + addedHelthCount) <= 3)
            {
                helthPoint+= addedHelthCount;
                onHelthChange?.Invoke(helthPoint);
                other.GetComponent<HelthBuff>().DestroyHelthBuff();
            }
           
        }
    }

    private IEnumerator InvulnerableTimer(float time)
    {
        isInvulnerable = true;
        yield return new WaitForSecondsRealtime(time);
        isInvulnerable = false;
    }
    private IEnumerator DeathPlayer()
    {
        isDead = true;
        transform.position = playerSpawner.position;
        yield return new WaitForSecondsRealtime(0.1f);
     
        transform.position = playerSpawner.position;
        helthPoint = 3;
        onHelthChange?.Invoke(helthPoint);
        
        isDead = false;
    }

}
