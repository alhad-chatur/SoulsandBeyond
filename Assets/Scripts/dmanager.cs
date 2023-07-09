using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class dmanager : MonoBehaviour
{
    // Start is called before the first frame update
    public static int d1;
    public static int d2;
    public float dyingspeed;
    postprocessing pp;
    public static bool isdying1 = false;
    public static bool isdying2 = false;
    public static bool death_11,hasdied;
    Volume vol;
    void Start()
    {
        pp = GetComponent<postprocessing>();
        vol = GetComponent<Volume>();
        isdying1 = false;
        isdying2 = false;
        hasdied = false;
        death_11 = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
        if(isdying1 == true || isdying2 == true)
        {
            vol.weight += dyingspeed * Time.fixedDeltaTime;
        }
        if (vol.weight >= 1 && hasdied == false)
        {
            death_11 = true;
            hasdied = false;
        }
    }
   

}
