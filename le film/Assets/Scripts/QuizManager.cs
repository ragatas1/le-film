using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

public class QuizManager : MonoBehaviour
{
    [SerializeField] int questionsToPass;
    int questions;
    public static QuizManager instance;
    [SerializeField] int minScene;
    [SerializeField] int maxScene;
    int scene;
    int scene1;
    int scene2;
    int scene3;
    Scene m_Scene;
    void Awake()
    {
        if (instance != null)

        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void QuestionRight()
    {
        questions = questions + 1;
        if (questions == questionsToPass)
        {
            SceneManager.LoadScene("win");
        }
        else
        {
            LoadScene();
        }
    }
    private void Update()
    {
        m_Scene = SceneManager.GetActiveScene();
        if (m_Scene.name == "StartScene")
        {
            Debug.Log("start");
            questions = 0;
        }
    }
    public void LoadScene() 
    {
        scene = Random.Range(minScene, maxScene+1);
        if (scene == scene1|| scene == scene2 || scene == scene3)
        {
            scene = Random.Range(minScene, maxScene + 1);
        }
        else
        {
            if (questions == 1) { scene1 = scene; }
            if (questions == 2) { scene2 = scene; }
            if (questions == 3) { scene3 = scene; }
            SceneManager.LoadScene(scene);
        }

    }
}
