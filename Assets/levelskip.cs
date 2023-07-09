using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelskip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void skipbutton()
    {
        SceneManager.LoadScene("cutscene 2");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
