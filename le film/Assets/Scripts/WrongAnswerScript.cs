using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WrongAnswerScript : MonoBehaviour
{
    [SerializeField] string scene;
    private void OnMouseDown()
    {
        Debug.Log("wrong answer");
        SceneManager.LoadScene(scene);
    }
}
