using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizAnswerChooser : MonoBehaviour
{
    [SerializeField] GameObject[] answers;
    [SerializeField] int index;
    GameObject current;
    AnswerScript answer;
    AnswerScript lastOne;
    float wait;
    [SerializeField] float coolDown;
    private void Start()
    {
        current = answers[index - 1];
        answer = current.GetComponent<AnswerScript>();
        answer.chosenOne = true;
    }
    // Update is called once per frame
    void Update()
    {        
        controlls();
        if (Input.GetButtonDown("roll"))
        {
            answer = current.GetComponent<AnswerScript>();
            answer.answer();
        }
    }

    void controlls()
    {

        if (Input.GetAxis("Vertical") < 0)
        {
            if (wait > coolDown)
            {
                if (answer != null)
                {
                    lastOne = answer;
                    lastOne.chosenOne = false;
                }
                current = answers[index - 1];
                index = index + 1;
                answer = current.GetComponent<AnswerScript>();
                answer.chosenOne = true;
                if (index > answers.Length)
                {
                    index = 1;
                }
                wait = 0;
            }
            else
            {
                wait = wait + 1 * Time.deltaTime;
            }
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            if (wait > coolDown)
            {
                if (answer != null)
                {
                    lastOne = answer;
                    lastOne.chosenOne = false;
                }
                current = answers[index - 1];
                answer = current.GetComponent<AnswerScript>();
                answer.chosenOne = true;
                index = index - 1;
                if (index <= 0)
                {
                    index = answers.Length;
                }
                wait = 0;
            }
            else
            {
                wait = wait +1*Time.deltaTime;
            }
        }
        else
        {
            wait = coolDown;
        }
    }
}
