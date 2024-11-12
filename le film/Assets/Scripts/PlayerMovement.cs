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
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator anim;
    private Vector3 rollDir;
    private State state;
    float rollSpeed;
    public float rollingSpeed;
    Vector2 movement;
    

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

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rollDir = new Vector3(movement.x, movement.y).normalized;
                    rollSpeed = rollingSpeed;
                    state = State.Rolling;
                }
                break;
            case State.Rolling:
                state = State.Rolling;
                float rollspeedDropMultiplier = 5;
                rollSpeed -= rollSpeed * rollspeedDropMultiplier * Time.deltaTime;
                float rollspeedMinimum = 5f;
                if (rollSpeed < rollspeedMinimum)
                {
                    state = State.Normal;
                }

                break;
        }


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
                Debug.Log("im rollin");
                break;
        }
    }    

}
