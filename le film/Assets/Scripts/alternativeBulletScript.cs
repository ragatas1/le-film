using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class alternativeBulletScript : MonoBehaviour
{
    GameObject jeremy;
    JeremyScript jeremyScript;
    GameObject tid;
    aliveLengthScript tidScript;
    public PlayerMovement playerMovement;
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = Player.GetComponent<PlayerMovement>();
        jeremy = GameObject.FindGameObjectWithTag("jeremy");
        jeremyScript = jeremy.GetComponent<JeremyScript>();
        tid = GameObject.FindGameObjectWithTag("tid");
        if (tid != null)
        {
            tidScript = tid.GetComponent<aliveLengthScript>();
        }
        StartCoroutine(die());
        transform.localScale = new Vector3 (0.093f*jeremyScript.shotlength,0.093f * jeremyScript.shotlength, 0.093f * jeremyScript.shotlength);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && playerMovement.isRolling == false)
        {
            if (tidScript == null)
            {
                Debug.Log("shot");
            }
            else
            {
                Debug.Log("alive for " + tidScript.tid + " seconds");
            }
            SceneManager.LoadScene(jeremyScript.sendeScene);
        }
    }
    IEnumerator die()
    {
        yield return new WaitForSeconds(jeremyScript.shotLife);
        Destroy(gameObject);
    }
}
