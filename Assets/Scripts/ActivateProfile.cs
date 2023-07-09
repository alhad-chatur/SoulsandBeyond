using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActivateProfile : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public TextMeshProUGUI ProfilenameTMPro;
    [SerializeField] public TextMeshProUGUI ProfilelevelTMPro;
    public Button indplayerprof;
    public string profileName;
    public string profileLevel;
    public GameObject loadgame;
    public GameObject ind_playerprof;
    public string UID;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        indplayerprof.onClick.AddListener(taskonclick);
        ProfilenameTMPro.text = profileName;
        ProfilelevelTMPro.text = profileLevel;

    }
    void taskonclick()
    {

        loadgame.SetActive(false);
        ind_playerprof.SetActive(true);
        var profileManager = FindObjectOfType<ProfileManager>();
        profileManager.SetProfileWithUidAsActive(UID);
        ind_playerprof.GetComponentInChildren<LevelListLoader>().loadLevels();
    }
}
