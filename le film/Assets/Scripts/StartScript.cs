using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    [SerializeField] float delay;
    [SerializeField] string button = "roll";
    [SerializeField] string scene;
    bool delayed;
    private void Start()
    {
        delayed = false;
        StartCoroutine(Delay());
    }
    void Update()
    {
        if (delayed)
        {
            if (Input.GetButtonDown(button))
            {
                SceneManager.LoadScene(scene);
            }
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        delayed = true;
    }
}
