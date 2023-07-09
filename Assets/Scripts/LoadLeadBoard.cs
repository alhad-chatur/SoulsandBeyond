using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoadLeadBoard : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject profileForLeaderBoard;
    
    public void LoadLeaderBoard()
    {
        foreach(Transform child in transform)
        {
            Destroy((child.gameObject));
        }
        var array = FindObjectOfType<ProfileManager>().ProfilesData.savedProfiles.ToArray();
        var list = array.ToList();
            list.Sort(
                (profile, playerProfile) => profile.CompareTo(playerProfile));
            list.Reverse();
            foreach (var profile in list)
            {
                var prof = Instantiate(profileForLeaderBoard, this.transform);
                LoadLeadBoardProfile instantiatedProfile = prof.GetComponent<LoadLeadBoardProfile>();
                instantiatedProfile.profileName = profile.profileName;
                instantiatedProfile.profileCollectible = profile.collectibleCollected.Sum().ToString();
                instantiatedProfile.profileTiming = profile.time.Sum().ToString();
                // var rect = prof.GetComponent<RectTransform>();
                // rect.sizeDelta = new Vector2(2000, 65);
            }
    }
}
