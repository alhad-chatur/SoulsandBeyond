using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ZSerializer;

[Serializable]
public class PlayerProfile
{
    public string uid;
    public string profileName;
    public List<float> time;
    public List<int> collectibleCollected;
    public int levelsUnlocked;

    public PlayerProfile(string name)
    {
        uid = System.Guid.NewGuid().ToString("N");
        profileName = name;
        time = new List<float>(0);
        collectibleCollected = new List<int>();
        levelsUnlocked = 0;
    }
    

    public void UpdateCollectibleValue(int level, int value)
    {
        if (collectibleCollected.Count == level)
        {
            collectibleCollected.Add(value);
        }
        else
        {
            collectibleCollected[level] = Mathf.Max(value, collectibleCollected[level]);
        }
    }

    public void UpdateTimeValue(int level,float value)
    {
        if (time.Count == level)
        {
            time.Add(value);
        }
        else
        {
            time[level] = Mathf.Min(value, time[level]);
        }
    }
    
    public int CompareTo(object obj)
    {
        var a = this;
        var b = obj as PlayerProfile;
        if (a.collectibleCollected.Sum() > b.collectibleCollected.Sum())
        {
            return 1;
        }
        else
        {
            if (a.collectibleCollected.Sum() < b.collectibleCollected.Sum())
            {
                return -1;
            }
            else
            {
                if (a.time.Sum() > b.time.Sum())
                {
                    return -1;
                }
                else if (a.time.Sum() < b.time.Sum())
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

    }
}
