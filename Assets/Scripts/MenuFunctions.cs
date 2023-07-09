using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuFunctions : MonoBehaviour
{
    // Start is called before the first frame update
    // public static PlayerProfile activePlayerProfile;
    // ProfileManager profileManager;
    void Start()
    {
        // DontDestroyOnLoad(this.gameObject);
        // profileManager = SaveLoad.GetProfileManager();
    }


    public void CreateNewProfile(string name)
    {
        // activePlayerProfile = new PlayerProfile(name);
        // print(profileManager == null);
        // int index = profileManager.UIDs.Count;
        // profileManager.UIDs.Add(activePlayerProfile.UID);
        // profileManager.name.Add(activePlayerProfile.ProfileName);
    }

    public List<string> GetAvaialbeProfiles()
    {
        // return profileManager.UIDs;
        return null;
    }

    
    public void LoadProfile(string UID)
    {
        // activePlayerProfile = SaveLoad.loadPlayerProfile(UID);
    }
    public PlayerProfile GetPRofile(string UID)
    {
        // return SaveLoad.loadPlayerProfile(UID);
        return null;
    }
    public void SaveProfile(PlayerProfile profile)
    {
        // profile.UID = activePlayerProfile.UID;
        // profile.ProfileName = activePlayerProfile.ProfileName;
        // SaveLoad.savePlayerProfile(profile);
    }



}
