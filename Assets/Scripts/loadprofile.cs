using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class loadprofile : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject profileTemplate;
    private int TotalProfiles;
    public GameObject loadgame1;
    public GameObject ind;

    public void loadProfiles()
    {
        foreach(Transform child in transform)
        {
            Destroy((child.gameObject));
        }
        var profileManager = FindObjectOfType<ProfileManager>();
        var profiles = profileManager.ProfilesData.savedProfiles;
        foreach (var profile in profiles)
        {
            GameObject obj = Instantiate(profileTemplate,transform);
            var profileActivater = obj.GetComponent<ActivateProfile>();
            obj.GetComponent<ActivateProfile>().loadgame = loadgame1;
            obj.GetComponent<ActivateProfile>().ind_playerprof = ind;
            profileActivater.profileLevel = profile.levelsUnlocked.ToString();
            profileActivater.profileName = profile.profileName;
            profileActivater.UID = profile.uid;
            var rectTransform = obj.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 65);
        }
    }
    
}
