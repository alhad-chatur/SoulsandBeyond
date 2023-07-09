using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelListLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public string UID = "";
    [SerializeField]private GameObject LevelPrefab;
    private static string[] levelsOrder = new[] { "cave", "Level 1", "Level 2", "Level 3", "Level 4" }; 

    public void loadLevels()
    {
        foreach (Transform child in transform)
        {
            Destroy((child.gameObject));
        }
        var profileManager = FindObjectOfType<ProfileManager>();
        var profiles = profileManager.ProfilesData.savedProfiles;
       // print(profileManager.ProfilesData.activePlayerProfileIndex);
        for (int i = 0; i <= profiles[profileManager.ProfilesData.activePlayerProfileIndex].levelsUnlocked; i++)
        {
            GameObject level = Instantiate(LevelPrefab, transform);
            var levelButton = level.GetComponent<LevelButton>();
            levelButton.levelNo = "Level "+i.ToString();
            levelButton.sceneToLoad = levelsOrder[i];
        }
    }
    
}
