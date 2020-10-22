using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationRandomizer : MonoBehaviour
{
    // Start is called before the first frame update
    float degree;

    void Start()
    {

      degree = Random.Range(0f, 360f);

      transform.Rotate(0f,degree,0f,Space.Self);
    }

    // Update is called once per frame
    void Update()
    {


        // Smoothly tilts a transform towards a target rotation.


    }
}
