using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timerevscreen : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject upscreen, downscreen;
    Rewinder up_down;
    ABILITYUSE au;
    NewControls inaction;
    void Start()
    {
        up_down = FindObjectOfType<Rewinder>();
        au = FindObjectOfType<ABILITYUSE>();
        inaction = new NewControls();
        inaction.Player.Enable();

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.Z) && au.CurrentHealth >=5.0f) 
        if(inaction.Player.Rewind.ReadValue<float>() !=0 && au.CurrentHealth >=5.0f)
        {
             if(up_down.activeRewindManagerIndex ==0)
             {
                upscreen.SetActive(true);
                downscreen.SetActive(false);
            }
             else if(up_down.activeRewindManagerIndex == 1)
            {
                upscreen.SetActive(false);
                downscreen.SetActive(true);

            }

        }
        else
        {
            upscreen.SetActive(false);
            downscreen.SetActive(false);
        }

    }
}
