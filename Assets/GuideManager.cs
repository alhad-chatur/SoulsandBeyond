using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideManager : MonoBehaviour
{
    [SerializeField] private GameObject windowsBuildGuide;
    [SerializeField] private GameObject androidBuildGuide;

    private void Awake()
    {
        windowsBuildGuide.SetActive(false);
        androidBuildGuide.SetActive(false);
        if (Application.isMobilePlatform)
        {
            androidBuildGuide.SetActive(true);
        }
        else
        {
            windowsBuildGuide.SetActive(true);
        }
    }
}
