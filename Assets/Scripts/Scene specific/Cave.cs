using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;

public class Cave : MonoBehaviour
{
    public string sceneToLoad = "cave";
    [SerializeField] private Animator animator;
    private static readonly int End = Animator.StringToHash("end");
    private float time;
    private int currentLevel = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        ProfileManager profileManager = FindObjectOfType<ProfileManager>();
        var profilesData = profileManager.ProfilesData;
        profilesData.savedProfiles[profilesData.activePlayerProfileIndex].levelsUnlocked = 1;
        // if (profilesData.savedProfiles[profilesData.activePlayerProfileIndex].time
        //     .TryGetValue(0, out float valTime))
        // {
        //     profilesData.savedProfiles[profilesData.activePlayerProfileIndex].time[0] =
        //         Mathf.Min(time, valTime);
        // }
        // else
        // {
        //     profilesData.savedProfiles[profilesData.activePlayerProfileIndex].time
        //         .Add(0, time);
        // }
        // profilesData.savedProfiles[profilesData.activePlayerProfileIndex].collectibleCollected[0] = 0;
        profilesData.savedProfiles[profilesData.activePlayerProfileIndex].UpdateCollectibleValue(currentLevel, 0);
        profilesData.savedProfiles[profilesData.activePlayerProfileIndex].UpdateTimeValue(currentLevel, time);
        profileManager.Save();
        StartCoroutine(LoadLevel(sceneToLoad));
    }
    public IEnumerator LoadLevel(string scene)
    {
        animator.SetTrigger(End);
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene(scene);
    }

    private void Update()
    {
        time += Time.deltaTime;
    }
}
