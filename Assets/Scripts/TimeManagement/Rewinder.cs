    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Rewinder : MonoBehaviour
{
    public RewindManager[] rewindManagers;
    public int activeRewindManagerIndex = 0;
    public int tempactiverewindmanagerindex = 0;
    public bool isRewinding = false;
    [SerializeField] float rewindIntensity = 0.02f;          //Variable to change rewind speed
    float rewindValue = 0;
    private NewControls _newControls;
    public static bool canRewind = true;

    private void Start()
    {
        
        _newControls = new NewControls();
        _newControls.Player.Enable();
    }

    private void Update()
    {
       /* if (_newControls.Player.SwapRewind.triggered)
        //if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            activeRewindManagerIndex =
                activeRewindManagerIndex == rewindManagers.Length - 1 ? 0 : activeRewindManagerIndex + 1;
        }*/
    }

    void FixedUpdate()
    {
        if(_newControls.Player.Rewind.ReadValue<float>() == 1 && canRewind)                     //Change keycode for your own custom key if you want
        {

            rewindValue += rewindIntensity;                 //While holding the button, we will gradually rewind more and more time into the past

            if (!isRewinding)
            {
                activeRewindManagerIndex = tempactiverewindmanagerindex;
                rewindManagers[activeRewindManagerIndex].StartRewindTimeBySeconds(rewindValue);
            }
            else
            {
                if(rewindManagers[activeRewindManagerIndex].HowManySecondsAvailableForRewind>rewindValue)      //Safety check so it is not grabbing values out of the bounds
                    rewindManagers[activeRewindManagerIndex].SetTimeSecondsInRewind(rewindValue);
            }


            isRewinding = true;
        }
        else
        {
            if(isRewinding)
            {
                rewindManagers[activeRewindManagerIndex].StopRewindTimeBySeconds();
                activeRewindManagerIndex = tempactiverewindmanagerindex;
                //rewindManagers[0].StopRewindTimeBySeconds();
                //rewindManagers[1].StopRewindTimeBySeconds();
                rewindValue = 0;
                isRewinding = false;
            }
        }
    }
}
