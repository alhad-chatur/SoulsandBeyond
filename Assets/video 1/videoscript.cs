using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class videoscript : MonoBehaviour
{
    VideoPlayer vp;
    // Start is called before the first frame update
    void Start()
    {
        vp = this.GetComponent<VideoPlayer>();
    }
    void Update()
    {
        if(vp.time>=12.0f || Input.GetKeyDown(KeyCode.L))
         SceneManager.LoadScene("cave");
    }

    
}
