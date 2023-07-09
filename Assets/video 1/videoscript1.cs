using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class videoscript1 : MonoBehaviour
{
   VideoPlayer vp;
    public GameObject text;
      // Start is called before the first frame update
      void Start()
      {
          vp = this.GetComponent<VideoPlayer>();
      }
      void Update()
      { 
          if(vp.time>=65.0f )
           SceneManager.LoadScene("Level 1");

        if (vp.time >= 10.0f && vp.time<=40.0f)
        {
            text.SetActive(true);
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
            {
                SceneManager.LoadScene("Level 1");
            }
        }
        else
            text.SetActive(false);
      }
    public void skipbutton()
    {
        SceneManager.LoadScene("Level 1");
    }
}
