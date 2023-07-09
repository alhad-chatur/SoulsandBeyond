using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class groundcollider1 : MonoBehaviour
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
            ntime += Time.deltaTime;
        }
        else
        {
            ntime = 0;
        }
        if (ntime >= 0.2f)
        {
            movement1.isgrounded = true;
            movement1.isfalling = false;
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

                movement1.isgrounded = false;
                tempisgrounded = false;
            }
            if (other.gameObject.CompareTag("movinggnd"))
                player.transform.SetParent(null);
        }
    }


}
