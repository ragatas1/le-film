using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aliveLengthScript : MonoBehaviour
{
    [HideInInspector] public float tid;
    void Update()
    {
        tid = tid+1*Time.deltaTime;
    }
}
