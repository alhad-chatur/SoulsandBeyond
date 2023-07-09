using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallbaby : MonoBehaviour
{
    //public GameObject smallbaby1;
    
    public Transform target;
    public float smoothSpeed = 0.125f;
    public float smoothSpeed1;
    public Vector3 offset;
   // public Vector3 offset1;
    public Vector3 offset2;
    public Transform target1;
    private bool followplayer = false;
    public Vector3 dir;
    public Vector3 offset3;
    public float freq, amp;
    float time;
    characterAnimator playerstate;
    characterAnimator1 playerstate1;
    Rewinder up_down;
    private NewControls _newControls;
  

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        // move = FindObjectOfType<Movement>();
        playerstate = FindObjectOfType<characterAnimator>();
        playerstate1 = FindObjectOfType<characterAnimator1>();
        up_down = FindObjectOfType<Rewinder>();
        _newControls = new NewControls();
        _newControls.Player.Enable();

    }

    // Update is called once per frame

    // the offset from the target object
    //_newControls.Player.Rewind.ReadValue<float>() == 1
    void FixedUpdate()
    {
        //if (Input.GetKey(KeyCode.W) && _newControls.Player.Rewind.ReadValue<float>() !=1)
        if (_newControls.Player.SwapRewind.ReadValue<float>() >0 && _newControls.Player.Rewind.ReadValue<float>() !=1)
        {
            followplayer = false;
            up_down.tempactiverewindmanagerindex = 0;
        }
        //if (Input.GetKey(KeyCode.S) && _newControls.Player.Rewind.ReadValue<float>() != 1)
        if (_newControls.Player.SwapRewind.ReadValue<float>() < 0 && _newControls.Player.Rewind.ReadValue<float>() != 1)
        {
            up_down.tempactiverewindmanagerindex = 1;
            //Vector3 desiredPosition1 = target1.position + offset1;
            //Vector3 smoothPosition1 = Vector3.Lerp(transform.position, desiredPosition1, smoothSpeed);
            //transform.position = smoothPosition1;
            followplayer = true;

        }

        if (followplayer == false)
        {
            followPlayerFirst();
            if (up_down.activeRewindManagerIndex == 0)
            {
                if (playerstate.speedTarget == 0)
                {
                    time += Time.fixedDeltaTime;
                    offset3 = dir.normalized * (amp * Mathf.Sin(time * freq) * Time.fixedDeltaTime);
                    Vector3 desiredPosition = target.position + offset3;
                    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                    transform.position = smoothedPosition;

                }
              /*  else if((target.transform.position - transform.position).magnitude < 0.3)
                 {
                      Vector3 desiredPosition = target.position + offset;
                         Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed1);
                         transform.position = smoothedPosition;
                    print("ravi shirsat");
                     }*/
            }
            else if (up_down.activeRewindManagerIndex == 1)
            {
                if (playerstate1.speedTarget == 0)
                {
                    time += Time.fixedDeltaTime;
                    offset3 = dir.normalized * (amp * Mathf.Sin(time * freq) * Time.fixedDeltaTime);
                    Vector3 desiredPosition = target.position + offset3;
                    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                    transform.position = smoothedPosition;

                }
            }
        }
        else
        {
            followPlayerSecond();
            if (up_down.activeRewindManagerIndex == 0)
            {
                if (playerstate.speedTarget == 0)
                {
                    time += Time.fixedDeltaTime;
                    offset3 = dir.normalized * (amp * Mathf.Sin(time * freq) * Time.fixedDeltaTime);
                    Vector3 desiredPosition = target1.position + offset3;
                    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                    transform.position = smoothedPosition;
                }
                else if((playerstate.speedTarget == 0 && (target.transform.position - transform.position).magnitude < 0.3)){
                    Vector3 desiredPosition = target.position + offset;
                    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed1);
                    transform.position = smoothedPosition;
                }
            }
            else if(up_down.activeRewindManagerIndex == 1)
            {
                if (playerstate1.speedTarget == 0)
                {
                    time += Time.fixedDeltaTime;
                    offset3 = dir.normalized * (amp * Mathf.Sin(time * freq) * Time.fixedDeltaTime);
                    Vector3 desiredPosition = target1.position + offset3;
                    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                    transform.position = smoothedPosition;
                }


            }
        }
    }
    private void followPlayerFirst()
    {
       // print(target.localRotation.y);
        if(target.rotation.y == 1)
        {
            Vector3 desiredPosition = target.position + offset2;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
        else if(target.rotation.y == 1 &&(target.transform.position - transform.position).magnitude < 0.3)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed1);
            transform.position = smoothedPosition;
            
        }
       else if(target.rotation.y == 0 && (target.transform.position - transform.position).magnitude < 0.3)
        {
            Vector3 desiredPosition = target.position + 2*offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed1);
            transform.position = smoothedPosition;
        }
        else 
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
     
        
    }

    private void followPlayerSecond()
    {
        if (target1.rotation.y == 1)
        {
            Vector3 desiredPosition1 = target1.position + offset2;
            Vector3 smoothPosition1 = Vector3.Lerp(transform.position, desiredPosition1, smoothSpeed);
            transform.position = smoothPosition1;
        }

        else
        {
            Vector3 desiredPosition = target1.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

}
