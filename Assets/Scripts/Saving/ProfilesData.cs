using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZSerializer;

[CreateAssetMenu(menuName = "ProfileData")]
[SerializeGlobalData(GlobalDataType.Globally)]
public class ProfilesData : GlobalObject
{
    public List<PlayerProfile> savedProfiles;
    public int activePlayerProfileIndex = 0;
    
}
