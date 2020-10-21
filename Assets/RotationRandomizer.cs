using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationRandomizer : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 euler;

    void Start()
    {
      euler = transform.eulerAngles;
      euler.y = Random.Range(0f, 360f);
      transform.eulerAngles = euler;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
