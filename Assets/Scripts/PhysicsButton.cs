using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class PhysicsButton : MonoBehaviour
{
    public bool isRewinidng;
    public Rigidbody buttonTopRigid;
    public Transform buttonTop;
    public Transform buttonLowerLimit;
    public Transform buttonUpperLimit;
    public float threshHold;
    public float force = 10;
    private float upperLowerDiff;
    public bool isPressed;
    private bool prevPressedState;
    public AudioSource pressedSound;
    public AudioSource releasedSound;
    public Collider[] CollidersToIgnore;
    public UnityEvent onPressed;
    public UnityEvent onReleased;
    public bool isactive;
    
    // Start is called before the first frame update
    void Start()
    {
        isactive = false;
        Collider localCollider = GetComponent<Collider>();
        if (localCollider != null)
        {
            Physics.IgnoreCollision(localCollider, buttonTop.GetComponentInChildren<Collider>());

            foreach (Collider singleCollider in CollidersToIgnore)
            {
                Physics.IgnoreCollision(localCollider, singleCollider);
            }
        }
        
        if (transform.eulerAngles != Vector3.zero){
            Vector3 savedAngle = transform.eulerAngles;
            transform.eulerAngles = Vector3.zero;
            upperLowerDiff = buttonUpperLimit.position.y - buttonLowerLimit.position.y;
            transform.eulerAngles = savedAngle;
        }
        else
            upperLowerDiff = buttonUpperLimit.position.y - buttonLowerLimit.position.y;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (buttonTopRigid.velocity.magnitude <= 1.0f)
            buttonTopRigid.velocity = Vector3.zero;

        // if(isRewinidng) return;
        buttonTop.transform.localPosition = new Vector3(0, buttonTop.transform.localPosition.y, 0);
        buttonTop.transform.localEulerAngles = new Vector3(0, 0, 0);
        if (buttonTop.localPosition.y >= 0)

        {
            buttonTop.transform.position = new Vector3(buttonUpperLimit.position.x, buttonUpperLimit.position.y, buttonUpperLimit.position.z);
            buttonTopRigid.velocity = Vector3.zero;
        }
        else
        {
            if (isactive == false) 
                buttonTopRigid.AddForce(buttonTop.transform.up * (force * Time.fixedDeltaTime));

        }
        if (buttonTop.localPosition.y <= buttonLowerLimit.localPosition.y)
        {
            buttonTop.transform.position = new Vector3(buttonLowerLimit.position.x, buttonLowerLimit.position.y, buttonLowerLimit.position.z);
            buttonTopRigid.velocity = Vector3.zero;
        }
        if (Vector3.Distance(buttonTop.position, buttonLowerLimit.position) < upperLowerDiff * threshHold)
        {
            isPressed = true;
        }
        else
            isPressed = false;

        if (isPressed && prevPressedState != isPressed)
            Pressed();
        if (!isPressed && prevPressedState != isPressed)
            Released();
        
    }
 
    void Pressed(){
        if(isRewinidng) return;
        prevPressedState = isPressed;
        pressedSound.pitch = 1;
        pressedSound.Play();
        isactive = true;
        onPressed.Invoke();
    }

    void Released(){
        if(isRewinidng) return;
        prevPressedState = isPressed;
        onReleased.Invoke();
    }


}
