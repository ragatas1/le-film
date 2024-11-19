using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletScript : MonoBehaviour
{
    public string scene;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;
    Vector2 spawnPosition;
    float distance;
    [SerializeField] float deathDistance;
    GameObject tid;
    aliveLengthScript tidScript;
    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(transform.up*speed);
        spawnPosition = transform.position;
        StartCoroutine(die());
        tid = GameObject.FindGameObjectWithTag("tid");
        if (tid != null)
        {
            tidScript = tid.GetComponent<aliveLengthScript>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(spawnPosition, transform.position);
        if (distance > deathDistance)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (tidScript == null)
            {
                Debug.Log("shot");
            }
            else
            {
                Debug.Log("alive for " + tidScript.tid + " seconds");
            }
            SceneManager.LoadScene(scene);
        }
    }
    IEnumerator die()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
