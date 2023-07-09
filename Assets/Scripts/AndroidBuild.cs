using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidBuild : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (!Application.isMobilePlatform)
        {
            this.gameObject.SetActive(false);
        }

    }

}
