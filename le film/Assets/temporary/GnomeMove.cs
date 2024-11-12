using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnomeMove : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizantalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movmentDirection = new Vector2(horizantalInput, verticalInput);
        float inputMagnitude = Mathf.Clamp01 (movmentDirection.magnitude);
        movmentDirection.Normalize();

        transform.Translate(movmentDirection * speed * inputMagnitude * Time.deltaTime, Space.World);

        if (movmentDirection != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movmentDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
