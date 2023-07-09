using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resizecollider : MonoBehaviour
{
    // Start is called before the first frame update
    /* public GameObject toe1,toe2;
     public GameObject head;
     CapsuleCollider cc;

     void Start()
     {
        cc = GetComponent<CapsuleCollider>();    
     }

     // Update is called once per frame
     void Update()
     {
   }
   private void FixedUpdate()
   {
       if (Vector3.Distance(toe1.transform.position, head.transform.position) > Vector3.Distance(toe2.transform.position, head.transform.position))
       {
           cc.center = new Vector3(transform.position.x, (toe1.transform.position.y + head.transform.position.y) / 2, transform.position.z) - transform.position;
           cc.height = Mathf.Abs(head.transform.position.y - toe1.transform.position.y);
       }
       else
       {
           cc.center = new Vector3(transform.position.x, (toe2.transform.position.y + head.transform.position.y) / 2, transform.position.z) - transform.position;
           cc.height = Mathf.Abs(head.transform.position.y - toe2.transform.position.y);
       }

   }
    */
    /*
         center - 0.08 ,0.82 // 00,0.17
         height - 1.31 // 1.85

         bottom - 0,0.114//-0.036,-0.821 
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

        cc.center = new Vector3(animvalue*0.08f, 0.17f + animvalue*(0.65f), 0);
        cc.height = 1.85f - animvalue * 0.54f;

        bottom.transform.localPosition = new Vector3(-0.036f + animvalue * 0.036f, -0.821f + animvalue * 0.935f, 0);

    }
}
