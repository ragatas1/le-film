using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnswerScript : MonoBehaviour
{
    [SerializeField] bool riktig;
    [SerializeField] string scene;
    GameObject quiz;
    QuizManager quizManager;
    /*[HideInInspector]*/ public bool chosenOne;
    [SerializeField] TextMeshProUGUI txt;
    // Start is called before the first frame update
    void Start()
    {
        quiz = GameObject.FindGameObjectWithTag("quiz");
        quizManager = quiz.GetComponent<QuizManager>();
    }
    private void Update()
    {
        if (chosenOne)
        {
            txt.color = new Color(1,0, 0, 1);
        }
        else
        {
            txt.color = new Color(1, 1, 1, 1);
        }
    }
    public void answer()
    {
        if (riktig)
        {
            quizManager.QuestionRight();
        }
        else
        {
            SceneManager.LoadScene(scene);
        }
    }
}
