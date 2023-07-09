using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using ZSerializer;

public class ProfileManager: PersistentMonoBehaviour
{
    [SerializeField]public ProfilesData ProfilesData; 
    
    private void Start()
    {
        ZSerialize.LoadGlobal(ProfilesData);
        Application.targetFrameRate = 60;
        DontDestroyOnLoad(this);
    }

    public void CreateNewProfile(string name)
    {
        print("part");
        var newProfile = new PlayerProfile(name);
        ProfilesData.savedProfiles.Add(newProfile);
        ProfilesData.activePlayerProfileIndex = ProfilesData.savedProfiles.Count - 1;
        ZSerialize.SaveGlobal(ProfilesData);
        // ZSerialize.SaveGlobal(this);
    }

    public string[] GetAvailableProfilesUid()
    {
        string[] Uids = new string[ProfilesData.savedProfiles.Count];
        int index = 0;
        foreach (PlayerProfile profile in ProfilesData.savedProfiles)
        {
            Uids[index] = profile.uid;
            index += 1;
        }

        return Uids;
    }
    
    public string[] GetAvailableProfilesNames()
    {
        string[] names = new string[ProfilesData.savedProfiles.Count];
        int index = 0;
        foreach (PlayerProfile profile in ProfilesData.savedProfiles)
        {
            names[index] = profile.profileName;
            index += 1;
        }

        return names;
    }

    public PlayerProfile GetProfileWithUid(string Uid)
    {
        foreach (var vProfile in ProfilesData.savedProfiles)
        {
            if (vProfile.uid == Uid)
            {
                return vProfile;
            }
        }

        return null;
    }
    
    public int GetProfileIndexWithUid(string Uid)
    {
        for (int i=0; i < ProfilesData.savedProfiles.Count; i++)
        {
            if (ProfilesData.savedProfiles[i].uid == Uid)
            {
                return i;
            }
        }

        return -1;
    }

    public void SetProfileWithUidAsActive(string Uid)
    {
        var index = GetProfileIndexWithUid(Uid);
        if(index == -1) return;
        ProfilesData.activePlayerProfileIndex = index;
        ZSerialize.SaveGlobal(ProfilesData);

    }

    public void Save()
    {
        ZSerialize.SaveGlobal(ProfilesData);
    }

    public void Load()
    {
        ZSerialize.LoadGlobal(ProfilesData);
    }
    
}
