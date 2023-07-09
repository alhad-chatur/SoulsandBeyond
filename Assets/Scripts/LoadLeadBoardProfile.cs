using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class LoadLeadBoardProfile : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI profileNameTMPro;
    [SerializeField] public TextMeshProUGUI profileCollectiveTMPro;
    [SerializeField] public TextMeshProUGUI profileTimingTMPro;
    public string profileName;
    public string profileCollectible;
    public string profileTiming;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        profileNameTMPro.text = profileName;
        profileCollectiveTMPro.text = profileCollectible;
        profileTimingTMPro.text = profileTiming;
    }
}
