using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rollSoundScript : MonoBehaviour
{
    [SerializeField] int numberOfClips;
    int sound;
    [SerializeField] JeremyScript jerry;
    private void Update()
    {
        if (Input.GetButtonDown("roll"))
        {
            RollSound();
        }
    }
    void RollSound()
    {
        if (jerry.danger)
        {
            sound = Random.Range(1, numberOfClips + 1);
            AudioManager.Play("roll " + sound);
        }
    }
}
