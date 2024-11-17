using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightAnswerScript : MonoBehaviour
{
    GameObject quiz;
    QuizManager quizManager;
    // Start is called before the first frame update
    void Start()
    {
        quiz = GameObject.FindGameObjectWithTag("quiz");
        quizManager = quiz.GetComponent<QuizManager>();
    }
    private void OnMouseDown()
    {
        Debug.Log("right answer");
        quizManager.QuestionRight();
    }
}
