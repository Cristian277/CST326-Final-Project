using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed = 5f;
    private Rigidbody2D myRigidbody;
    private Vector3 changeSpeed;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", -1);
    }

    // Update is called once per frame

    void Update()
    {
        changeSpeed = Vector3.zero;
        changeSpeed.x = Input.GetAxisRaw("Horizontal");
        changeSpeed.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && currentState != PlayerState.attack && currentState!= PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.walk;
    }

    void UpdateAnimationAndMove()
    {
        if (changeSpeed != Vector3.zero)
        {
            Move();
            animator.SetFloat("MoveX", changeSpeed.x);
            animator.SetFloat("MoveY", changeSpeed.y);
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    void Move()
    {
        changeSpeed.Normalize();
        myRigidbody.MovePosition(transform.position + changeSpeed * speed * Time.deltaTime);
    }

    public void playerSprinting()
    {
        speed = 8f;
    }

    
    public void playerWalking()
    {
        speed = 6f;
    }

    public void Knock(float knockBackTime)
    {
        StartCoroutine(KnockCo(knockBackTime));
    }

    private IEnumerator KnockCo(float knockBackTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockBackTime);
            myRigidbody.velocity = Vector2.zero;
            //Gets enemy script and sets the currentState enum to idle
            currentState = PlayerState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }
}
