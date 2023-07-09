using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    private PhysicsButton _physicsButton;
    public bool isRewinding;
    public Action<bool> PlayerLeftPlatform;
    public Action<bool> PlayerEnteredPlatform;// Start is called before the first frame update
    void Start()
    {
        _physicsButton = GetComponentInParent<PhysicsButton>();
    }

    // Update is called once per frame
    private void OnCollisionExit(Collision other)
    {
        if(isRewinding ) return;
        
        PlayerLeftPlatform.Invoke(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isRewinding) return;
        PlayerEnteredPlatform.Invoke(true);
    }
}
