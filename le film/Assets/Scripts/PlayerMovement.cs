using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    private enum State
    {
        Normal,
        Rolling,
    }
    public float moveSpeed = 10f;

    public Rigidbody2D rb;
    public Animator anim;
    private Vector2 rollDir;


    private State state;
    float rollSpeed;
    public float rollingSpeed;
    Vector2 movement;
    [SerializeField] private Cooldown rollCooldown;

    // Update is called once per frame

    private void Awake()
    {
      state = State.Normal;
    }
    void Update()
    {

        switch (state)
        {
            case State.Normal:
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");
                

                if (movement.y != 0 || movement.x != 0)
                {
                    anim.SetBool("Walking", true);
                }
                else
                {
                    anim.SetBool("Walking", false);
                }
                if (rollCooldown.IsCoolingDown)
                {
                    moveSpeed = 0f;
                }
                else
                { 
                    moveSpeed = 10f; 
                }
                if (Input.GetButtonDown("roll"))
                {
                    if (rollCooldown.IsCoolingDown) return;
                    rollDir = new Vector2(movement.x, movement.y).normalized;
                    rollSpeed = rollingSpeed;
                    state = State.Rolling;
                    rollCooldown.StartCooldown();

                }
                else
                    rollDir = Vector2.zero;
                
                break;
            case State.Rolling:
                state = State.Rolling;
                float rollspeedDropMultiplier = 10;
                rollSpeed -= rollSpeed * rollspeedDropMultiplier * Time.deltaTime;
                float rollspeedMinimum = 5f;
                if (rollSpeed < rollspeedMinimum)
                {
                    state = State.Normal;
                }

                break;

            
        }
        anim.SetFloat("RollX", rollDir.x);
        anim.SetFloat("RollY", rollDir.y);

    }

    void FixedUpdate()
    {
        switch (state)
        {
            case State.Normal:
                rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
                break;
            case State.Rolling:
                
                rb.velocity = rollDir * rollSpeed;
                
                 
                
                    
                
                break;
        }
    }    

}
