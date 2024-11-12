using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class JeremyScript : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Rigidbody2D rb;
    float playerDistance;
    [SerializeField] float targetDistance;
    [SerializeField] float ishRange;
    [SerializeField] float moveSpeed;
    int horizontalModifier;
    int verticalDirection;
    public float horizontalDirection;
    public float x;
    public float y;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(changeDirection());
    }

    // Update is called once per frame
    void Update()
    {
        
        playerDistance = Vector2.Distance(player.position, transform.position);
        if (playerDistance > targetDistance+ishRange) 
        {
            aproach();
            //around();
            verticalDirection = 1;
            horizontalModifier = 1;
        }
        else if (playerDistance < targetDistance - ishRange)
        {
            aproach();
            verticalDirection = -1;
            horizontalModifier = 1;
        }
        else
        {
            horizontalModifier = 2;
            around();
        }
        turn();

    }
    void turn()
    {
        var dir = player.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
    void around()
    {
        rb.MovePosition(transform.position + transform.right * moveSpeed * horizontalDirection * horizontalModifier * Time.deltaTime);
    }
    void aproach()
    {
        rb.MovePosition(transform.position+transform.up*moveSpeed* verticalDirection * Time.deltaTime);
    }

    IEnumerator changeDirection()
    {
        horizontalDirection = Random.Range(-1f, 1f);
        yield return new WaitForSeconds(2);
        StartCoroutine(changeDirection());
    }
}
