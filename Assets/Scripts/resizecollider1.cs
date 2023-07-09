using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resizecollider1 : MonoBehaviour
{
    
    /*
         center - 0.12, -0.4766 // 00,0.15
         height - 1.2 // 1.7839

         bottom - 0.062,0.044//-0.03,0.93 
    */
    CapsuleCollider cc;
    public Animator anim;
    float animvalue;
    public GameObject bottom;
    private void Start()
    {
        cc = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        animvalue = anim.GetFloat("State");
        animvalue = animvalue - 1;

        cc.center = new Vector3(animvalue * 0.12f, 0.15f - animvalue * (0.6266f), 0);
        cc.height = 1.7839f - animvalue * 0.5839f;

        bottom.transform.localPosition = new Vector3(-0.03f + animvalue * 0.065f, 0.93f - animvalue * 0.886f, 0);

    }
}
