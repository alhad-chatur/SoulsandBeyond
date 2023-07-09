using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class onhover : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform Button;
   // Animator anim;       //using animator for giving pop up effect on hover but attaching to on every button is quite heavy.
   // private string onhoveranimaton = "hoveron";
   // private string Onhoveranimation1 = "hoveroff";
    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        //anim.SetBool(onhoveranimaton, false);

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector3 small = new Vector3(0.16f, 0.16f, 0.16f);
         Vector3 big = new Vector3(0.14f, 0.14f, 0.14f);
         //Vector3 big = new Vector3(0.18f, 0.18f, 0.18f);
       // Button.GetComponent<Animator>().Play("hoveron");
        Button.localScale = Vector3.Slerp(small, big, 0.1f);
       // anim.SetBool(onhoveranimaton, true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
     //Button.GetComponent<Animator>().Play("hoveroff");
        Vector3 small = new Vector3(0.16f, 0.16f, 0.16f);
        Vector3 big = new Vector3(0.14f, 0.14f, 0.14f);
        Button.localScale = Vector3.Slerp(big, small, 0.1f);
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
