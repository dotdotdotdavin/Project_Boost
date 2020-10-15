using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    Vector3 startingPos;
    Vector3 offset;

    [SerializeField] float period = 2f;
    [Range(0,1)] [SerializeField] float movementFactor;
    float cycles;
    const float tau = Mathf.PI * 2f;
    float rawSinWave;


    // Start is called before the first frame update
    void Start()
    {
      startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
      if (period <= Mathf.Epsilon) { return; }
      
      cycles =Time.time / period;
      rawSinWave = Mathf.Sin(cycles * tau);

      movementFactor = rawSinWave / 2f + 0.5f;
      offset = movementVector * movementFactor;
      transform.position = startingPos + offset;
    }




}
