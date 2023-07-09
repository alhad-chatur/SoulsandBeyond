using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class groundcollider2 : MonoBehaviour
{
    bool tempisgrounded = false;
    float ntime = 0;
    public GameObject player;

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ground") || other.gameObject.CompareTag("movinggnd"))
        {
            tempisgrounded = true;
        }
        else
        {
            tempisgrounded = false;
        }


    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("ground") || other.gameObject.CompareTag("movinggnd"))
        {
            ntime+= Time.deltaTime;
        }
        else
        {
            ntime = 0;
        }
        if (ntime >= 0.33f)
        {
            Movement2.isgrounded = true;
            Movement2.isfalling = false;
            if (other.gameObject.CompareTag("movinggnd"))
                player.transform.SetParent(other.transform);
        }
       

    }

    private void OnTriggerExit(Collider other)
    {
        if (tempisgrounded == true)
        {
            if (other.gameObject.CompareTag("ground") || other.gameObject.CompareTag("movinggnd"))
            {

                Movement2.isgrounded = false;
                tempisgrounded = false;
            }
            if (other.gameObject.CompareTag("movinggnd"))
                player.transform.SetParent(null);
        }
    }

    
}
