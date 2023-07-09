using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class rotation1 : MonoBehaviour
{
    public static Action<bool> AppleCollected;
    private bool _collected = false;
    // Start is called before the first frame update
    public float rotationSpeed = 50f;
    private Vector3 axis;
    private void Start()
    {
        axis = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        axis /= axis.magnitude;
    }

    void Update()
    {
        transform.Rotate(axis * (rotationSpeed * Time.deltaTime));
       //Time.timeScale = 1f;
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& !_collected)
        {
            _collected = true;
            AppleCollected(true);
        }
    }
}
