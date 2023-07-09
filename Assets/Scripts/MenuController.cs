using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;

public class MenuController : MonoBehaviour
{
    public TMP_InputField inputFeild;
    MenuFunctions menuFunctions;
    [Header("Levels to load here")]
    public string NewGameLevel;
    private string levelToLoad;
    public string NameInput;
    
    [SerializeField] public GameObject noSavedGame = null;
    private ProfileManager _profileManager;
    private void Start()
    {
        _profileManager = FindObjectOfType<ProfileManager>();
        Cursor.lockState = CursorLockMode.None;
    }
    public void NewGameDialogOK()
    {
        NameInput = inputFeild.text;
       // print(inputFeild);
        _profileManager.CreateNewProfile(NameInput);
        SceneManager.LoadScene("cutscene 1");

    }

    public void LoadGameDialogOK()
    {
        if (PlayerPrefs.HasKey(NameInput))
        {
            
            levelToLoad = PlayerPrefs.GetString(NameInput);
            //PlayerPrefs.SetString("SavedLevel", levelToLoad);
            //SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            noSavedGame.SetActive(true);
        }
    }
  

public void Exit()
    {
        Application.Quit();
    }

    public AudioMixer audioMixer;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume",volume);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}