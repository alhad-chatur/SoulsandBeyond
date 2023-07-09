 using System;
 using System.Collections;
using System.Collections.Generic;
 using Unity.VisualScripting;
 using UnityEngine;
 using UnityEngine.SceneManagement;

 public class afterwinning : MonoBehaviour
{
    // Start is called before the first frame update
    public static int ninside1, ninside2;
    public static bool win;
    public static int haswon;
    [SerializeField] private Animator animator;
    private float time;

    [Header("Level Loading")] [SerializeField]
    private int levelsUnlockedOnCompletion =0;
    public string sceneToLoad = "cave";

    [SerializeField] private int collectiblesCollected = 0;
    AudioSource asur;
    audio_manager manager;

    private bool played;

    private bool _savedData;

    void Start()
    {
        _savedData = false;
        haswon = 0;
        ninside1 = 0;
        ninside2 = 0;
        asur = GetComponent<AudioSource>();
        manager = FindObjectOfType<audio_manager>();

        played = true;
        rotation.AppleCollected += AddCollectible;
        rotation1.AppleCollected += AddCollectible;
    }

    // Update is called once per frame
    void Update()
    {
        
        time += Time.deltaTime;
        if (ninside1 + ninside2 == 2 )
        {

            win = true;
            haswon = 1;
           // asur.PlayOneShot(winsound);
            if (!asur.isPlaying && played)
            {
                asur.Play();
                played = false;
                manager.enabled = false;
            }

            if (!_savedData)
            {
                SaveData();
                _savedData = true;
            }

            win = true;
            haswon = 1;
            
            
            // Debug.Log("won");
            // if (!source.isPlaying)
            // {
            //     FindObjectOfType<audio_manager>().play("winning");
            // }
            
            
        }
        // after win function            

        
    }

    private void SaveData()
    {
        ProfileManager profileManager = FindObjectOfType<ProfileManager>();
        int currentLevel = levelsUnlockedOnCompletion - 1;
        var profilesData = profileManager.ProfilesData;
        profilesData.savedProfiles[profilesData.activePlayerProfileIndex].levelsUnlocked = Mathf.Max(
            levelsUnlockedOnCompletion,
            profilesData.savedProfiles[profilesData.activePlayerProfileIndex].levelsUnlocked);
        // if (profilesData.savedProfiles[profilesData.activePlayerProfileIndex].time
        //     .TryGetValue(levelsUnlockedOnCompletion - 1, out float valTime))
        // {
        //     profilesData.savedProfiles[profilesData.activePlayerProfileIndex].time[levelsUnlockedOnCompletion - 1] =
        //         Mathf.Min(time, valTime);
        // }
        // else
        // {
        //     profilesData.savedProfiles[profilesData.activePlayerProfileIndex].time
        //         .Add(levelsUnlockedOnCompletion - 1, time);
        // }
        profilesData.savedProfiles[profilesData.activePlayerProfileIndex].UpdateCollectibleValue(currentLevel, collectiblesCollected);
        profilesData.savedProfiles[profilesData.activePlayerProfileIndex].UpdateTimeValue(currentLevel, time);
        // if (profilesData.savedProfiles[profilesData.activePlayerProfileIndex].collectibleCollected
        //     .TryGetValue(levelsUnlockedOnCompletion - 1, out int valCollectible))
        // {
        //     profilesData.savedProfiles[profilesData.activePlayerProfileIndex]
        //         .collectibleCollected[levelsUnlockedOnCompletion - 1] = Mathf.Max(collectiblesCollected);
        // }
        // else
        // {
        //     profilesData.savedProfiles[profilesData.activePlayerProfileIndex]
        //         .collectibleCollected.Add(levelsUnlockedOnCompletion - 1, collectiblesCollected);
        // }

         profileManager.Save();
    }

    private void AddCollectible(bool collected)
    {
        collectiblesCollected += 1;
    }


    
    
}
