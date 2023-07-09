using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onpressplat : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 inipos,tempinipos,tempfinalpos;
    public Vector3 movdirection = new Vector3(0,-1.0f,0.0f);
    public float movvalue =1;
    public float maxreturnspeed = 1.0f;
    float returnspeed = 0;
    public GameObject baseobj;
    PhysicsButton pb;
    float totaltime = 0;
    public bool ismovingfwd = false;
    public float forwardmovespeed =5.0f;
    RewindAbstract ra;
    Rigidbody toprb;
    GameObject buttontop;
    void Start()
    {
        ra= GetComponent<RewindAbstract>();
        inipos = transform.position;
        tempinipos = transform.position;
        returnspeed = maxreturnspeed;
        pb = baseobj.GetComponent<PhysicsButton>();
        //toprb = GetComponentInChildren<Rigidbody>();
        toprb = baseobj.transform.Find("upperlimit").transform.Find("buttontop").GetComponent<Rigidbody>();
        buttontop = toprb.transform.gameObject;
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (ra.IsTracking)
        {
            if (ismovingfwd == false)
            {
                if (Vector3.Dot((inipos - transform.position), -movdirection) > 0 && pb.isactive == true)
                {
                    transform.position -= movdirection * (returnspeed * Time.fixedDeltaTime);
                }
                else
                {
                    pb.isactive = false;
                   // toprb.AddForce(buttontop.transform.up * (1000));
                }
            }
            else
            {
                totaltime += Time.fixedDeltaTime;
                transform.position = Vector3.Lerp(tempinipos, tempfinalpos, forwardmovespeed * totaltime);
                if (Vector3.Dot(transform.position - tempfinalpos, movdirection) >= 0)
                    ismovingfwd = false;
            }
        }
        else
            totaltime -= Time.fixedDeltaTime;
    }
    public void moveforward()
    {
       // inipos = transform.position;
       // transform.position += movdirection * movvalue;
        ismovingfwd = true;
        tempinipos = transform.position;
        tempfinalpos = transform.position + movdirection * (movvalue);
        totaltime = 0;
    }
    public void movebackward()
    {
        returnspeed = maxreturnspeed;
    }

    /*IEnumerator startanim()
    {



        //yield return new WaitForSeconds();
    }
    */
}