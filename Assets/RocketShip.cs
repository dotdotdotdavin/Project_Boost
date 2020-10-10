using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShip : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField] float rocketThrust = 100f;
    [SerializeField] float mainThrust = 8f;
    float rotationSpeed;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        Thrust();
        Rotate();
	}

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space)) // can thrust while rotating
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            if(!audioSource.isPlaying){
              audioSource.Play();
            }
        }
        else{
          audioSource.Stop();
        }
    }

    private void Rotate()
    {
      rigidBody.freezeRotation = true;

        rotationSpeed = rocketThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }

      rigidBody.freezeRotation = false;
    }
}
