using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class JeremyScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Animator jeranim;
    [SerializeField] Animator gunanim;
    [SerializeField] Transform jeremy;
    [SerializeField] Transform player;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject bullet;
    [SerializeField] CircleCollider2D circle;
    [SerializeField] QuizManager quiz;
    public string sendeScene;


    [Header("Movement")]
    [SerializeField] float entranceTime;
    [SerializeField] float stopTime;
    [SerializeField] float timeTilQuiz;
    [SerializeField] float moveSpeed;
    int horizontalModifier;
    int verticalDirection;
    float horizontalDirection;
    bool hasStarted;
    float check;
    bool keepTurning;

    [Header("Gun")]
    [SerializeField] float targetDistance;
    [SerializeField] float ishRange;
    [SerializeField] float betweenShots;
    [SerializeField] float distanceToShoot;
    [SerializeField] float hesitation;
    bool stop;
    float playerDistance;
    int shellsLeft;
    bool shootin;
    bool standingStill;
    float standingRotation;
    Vector2 standingPosition;
    public float shotlength;
    public float shotLife;
    int shotsFired;
    int reloadRandom;
    [SerializeField] int reloadRandomSize;
    bool stopShoot;
    [HideInInspector] public bool danger;

    // Start is called before the first frame update
    void Start()
    {
        hasStarted = false;
        StartCoroutine(doStart());
        StartCoroutine(Quiz());
        shellsLeft = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted)
        {
            if (!stopShoot)
            {
                if (!standingStill)
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
                    if (playerDistance < distanceToShoot)
                    {
                        shoot();
                    }
                    turn();
                }
                else
                {
                    if (keepTurning)
                    {
                        turn();
                    }
                    else
                    {
                        rb.rotation = standingRotation;
                    }
                    transform.position = standingPosition;
                }
            }
        }
        move();
        animate();
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
            sprite.sortingOrder = 2;
        }
        else if (check > 135 || check < -135)
        {
            jeranim.SetInteger("direction", 1);
            gunanim.SetInteger("direction", 1);
            sprite.sortingOrder = 2;
        }
        else
        {
            jeranim.SetInteger("direction", 0);
            gunanim.SetInteger("direction", 0);
            if (check > 0)
            {
                sprite.sortingOrder = 0;
            }
            else
            {
                sprite.sortingOrder = 2;
            }
        }
        jeranim.SetBool("reload", standingStill);
    }
    void move()
    {
        rb.MovePosition(transform.position + (transform.right * moveSpeed * horizontalDirection * horizontalModifier)+ (transform.up * moveSpeed * verticalDirection));
    }
    void shoot()
    {
        if (!stop)
        {
            if (!shootin)
            {
                if (shellsLeft > 0)
                {

                    StartCoroutine(shooting());

                }
                else
                {
                    if (!standingStill)
                    {
                        StartCoroutine(reload());
                    }
                }
            }
        }
    }
    IEnumerator doStart()
    {
        yield return new WaitForSeconds(entranceTime-1.5f);
        verticalDirection = 1;
        AudioManager.Play("entrance clip");
        yield return new WaitForSeconds(1.5f);
        hasStarted = true;
        StartCoroutine(changeDirection());
        StartCoroutine(stopShooting());
        circle.isTrigger = false;
    }
    IEnumerator stopShooting()
    {
        yield return new WaitForSeconds(stopTime - entranceTime);
        verticalDirection = 0;
        standingRotation = rb.rotation;
        standingPosition = transform.position;
        standingStill = true;
        stopShoot = true;
        StopCoroutine(changeDirection());
        StopCoroutine(shooting());
        StopCoroutine(reload());
        AudioManager.Stop("reload");
        AudioManager.Play("credit dialogue");
        horizontalDirection = 0;
        rb.rotation = standingRotation;
        transform.position = standingPosition;
        Debug.Log(shotsFired + " shots fired");
    }
    IEnumerator Quiz()
    {
        yield return new WaitForSeconds(timeTilQuiz);
        quiz.LoadScene();
    }
    IEnumerator shooting()
    {
        if (!stopShoot)
        {
            shootin = true;
            shellsLeft = shellsLeft - 1;
            yield return new WaitForSeconds(betweenShots - 0.68f/*this is the length of the shotgun cock sound*/);
            AudioManager.Play("cock");
            StartCoroutine(DangerTime());
            yield return new WaitForSeconds(0.68f - hesitation);
            standingRotation = rb.rotation;
            standingPosition = transform.position;
            standingStill = true;
            yield return new WaitForSeconds(hesitation);
            Instantiate(bullet, transform.position, transform.rotation);
            shotsFired = shotsFired + 1;
            AudioManager.Play("shoot");
            shootin = false;
            standingStill = false;
        }
    }
    IEnumerator DangerTime()
    {
        danger = true;
        yield return new WaitForSeconds(0.7f);
        danger = false;
    }
    IEnumerator reload()
    {
        if (!stopShoot)
        {
            standingRotation = rb.rotation;
            standingPosition = transform.position;
            AudioManager.Play("reload");
            ReloadBanter();
            standingStill = true;
            yield return new WaitForSeconds(2.5f);
            shellsLeft = 8;
            standingStill = false;
        }
    }

    IEnumerator changeDirection()
    {
        horizontalDirection = Random.Range(-1f, 1f);
        yield return new WaitForSeconds(2);
        if (hasStarted)
        {
            StartCoroutine(changeDirection());
        }
    }
    void ReloadBanter()
    {
        reloadRandom = Random.Range(1, reloadRandomSize+1);
        AudioManager.Play("reload " + reloadRandom);
    }
}
