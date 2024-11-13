using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class JeremyScript : MonoBehaviour
{
    [SerializeField] Animator jeranim;
    [SerializeField] Animator gunanim;
    [SerializeField] Transform jeremy;
    [SerializeField] Transform player;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject bullet;
    float playerDistance;
    [SerializeField] float targetDistance;
    [SerializeField] float ishRange;
    [SerializeField] float moveSpeed;
    int horizontalModifier;
    int verticalDirection;
    float horizontalDirection;
    int shellsLeft;
    bool shootin;
    bool reloadin;
    [SerializeField] float betweenShots;
    [SerializeField] float shootDistance;
    public float check;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(changeDirection());
    }

    // Update is called once per frame
    void Update()
    {
        if (!reloadin)
        {
            playerDistance = Vector2.Distance(player.position, transform.position);
            if (playerDistance > targetDistance + ishRange)
            {
                verticalDirection = 1;
                horizontalModifier = 1;
            }
            else if (playerDistance < targetDistance - ishRange)
            {
                verticalDirection = -1;
                horizontalModifier = 0;
            }
            else
            {
                verticalDirection = 0;
                horizontalModifier = 2;
            }
            if (playerDistance < shootDistance)
            {
                shoot();
            }
            turn();
            move();
            animate();
        }
        jeremy.position = transform.position;
    }
    void turn()
    {
        var dir = player.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        check = angle;
    }
    void animate()
    {
        if (check < 45 && check > -45)
        {
            jeranim.SetInteger("direction", 2);
            gunanim.SetInteger("direction", 2);
        }
        else if (check > 135 || check < -135)
        {
            jeranim.SetInteger("direction", 1);
            gunanim.SetInteger("direction", 1);
        }
        else
        {
            jeranim.SetInteger("direction", 0);
            gunanim.SetInteger("direction", 0);
        }
        jeranim.SetBool("reload", reloadin);
    }
    void move()
    {
        rb.MovePosition(transform.position + (transform.right * moveSpeed * horizontalDirection * horizontalModifier)+ (transform.up * moveSpeed * verticalDirection));
    }
    void shoot()
    {
        if (!shootin)
        {
            if (shellsLeft > 0)
            {

                StartCoroutine(shooting());

            }
            else
            {
                if (!reloadin)
                {
                    StartCoroutine(reload());
                }
            }
        }
    }
    IEnumerator shooting()
    {
        shootin = true;
        shellsLeft = shellsLeft - 1;
        yield return new WaitForSeconds(betweenShots-0.68f/*this is the length of the shotgun cock sound*/);
        AudioManager.Play("cock");
        yield return new WaitForSeconds(0.68f);
        Instantiate(bullet, transform.position, transform.rotation);
        AudioManager.Play("shoot");
        shootin = false;
    }
    IEnumerator reload()
    {
        AudioManager.Play("reload");
        reloadin = true;
        yield return new WaitForSeconds(2.5f);
        shellsLeft = 8;
        reloadin = false;
    }

    IEnumerator changeDirection()
    {
        horizontalDirection = Random.Range(-1f, 1f);
        yield return new WaitForSeconds(2);
        StartCoroutine(changeDirection());
    }
}
