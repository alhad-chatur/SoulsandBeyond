using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public static class SaveLoad
{
    public static ProfileManager GetProfileManager()
    {
        string path = Application.persistentDataPath + "PlayerProfiles.pasta";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream;
        ProfileManager profileManager;
        if (File.Exists(path))
        {
            stream = new FileStream(path, FileMode.Open);
            profileManager = formatter.Deserialize(stream) as ProfileManager;
            stream.Close();
        }
        else
        {
            profileManager = new ProfileManager();
            SaveProfileManager(profileManager);
        }
        
        return profileManager; 
    }

    public static void SaveProfileManager(ProfileManager profileManager)
    {
        string path = Application.persistentDataPath + "PlayerProfiles.lick";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, profileManager);
        stream.Close();
    }

    public static PlayerProfile loadPlayerProfile(string UID)
    {
        string path = Application.persistentDataPath + UID + "_profile.kissback";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream;
        if (File.Exists(path))
        {
            stream = new FileStream(path, FileMode.Open);
            PlayerProfile profile = formatter.Deserialize(stream) as PlayerProfile;
            stream.Close();
            return profile;
        }
        else
        {
            Debug.Log("Please call Load fith a valid UID");
            return null;
        }
    }

    public static void savePlayerProfile(PlayerProfile playerProfile)
    {
        string path = Application.persistentDataPath + playerProfile.uid + "_profile.kissback";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, playerProfile);
        stream.Close();

    }

    public static int getIndex(string UID, ProfileManager profileManager)
    {
        // List<string> uids = profileManager.UIDs;
        // for(var i=0; i <uids.Count; i++)
        // {
        //     if (uids[i] == UID)
        //         return i;
        // }
        // return -1;
        return -1;
    }



}
