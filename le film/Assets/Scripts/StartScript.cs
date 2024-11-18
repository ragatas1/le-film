using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    [SerializeField] string button = "roll";
    [SerializeField] string scene;
    void Update()
    {
        if (Input.GetButtonDown(button))
        {
            SceneManager.LoadScene(scene);
        }
    }
}
