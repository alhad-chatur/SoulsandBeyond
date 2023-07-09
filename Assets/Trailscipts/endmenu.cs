using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class endmenu : MonoBehaviour
{
    [SerializeField]
    GameObject endmenu1;
    public GameObject andriodcanvas;
    public GameObject blur;
    public TextMeshProUGUI count;
    public TextMeshProUGUI timecount;
    public TextMeshProUGUI playername;
    public GameObject[] selectables1;
    //  public List<GameObject> selectables;
    Movement food;
    [SerializeField]public GameObject FILL;
    [SerializeField]public GameObject EMPTY;
    [SerializeField] public GameObject OBJ;
    [SerializeField] public GameObject post;
    winnigmenu wingame;
    ProfileManager profile_manager;


    private void Start()
    {
        food = FindObjectOfType<Movement>();
        wingame = FindObjectOfType<winnigmenu>();
   
        profile_manager = FindObjectOfType<ProfileManager>();
       

    }

    public void Update()
    {
       

        if (dmanager.death_11 == true && dmanager.hasdied == false)
        {
            FILL.SetActive(false);
            EMPTY.SetActive(false);
            OBJ.SetActive(false);
            post.SetActive(false);
            andriodcanvas.SetActive(false);
            Time.timeScale = 0f;
          Cursor.lockState = CursorLockMode.None;
            endmenu1.SetActive(true);
            blur.SetActive(true);
            playername.text = profile_manager.ProfilesData.savedProfiles[profile_manager.ProfilesData.activePlayerProfileIndex].profileName;
          //  Cursor.lockState = CursorLockMode.Locked;
            timecount.text = System.Math.Round(wingame.Time1,2).ToString() + "Sec";
            count.text = "X" + food.totalapples.ToString();
            dmanager.hasdied = true;
        }
        
    }

    public void restart()
    {
        if (endmenu1.activeInHierarchy == true)
        {
            //   if (Input.GetKey("escape"))
            // {
            //  print(1);
            //    Cursor.lockState = CursorLockMode.None;
            //   if (Input.GetKey("escape"))
            // {

            // Cursor.lockState = CursorLockMode.Locked;

            
            Time.timeScale = 1f;
            endmenu1.SetActive(false);
            blur.SetActive(false);
            FILL.SetActive(true);
            EMPTY.SetActive(true);
            OBJ.SetActive(true);
            andriodcanvas.SetActive(true);
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
            // }
            // }
           Scene active = SceneManager.GetActiveScene();
           SceneManager.LoadScene(active.name);
            Cursor.lockState = CursorLockMode.Locked;


        }


    }

    public void Home()
    {
        SceneManager.LoadScene("Mainmenu");
    }
    public void gamesebahar()
    {

        //if (Input.GetKey("backspace"))
        {
            Application.Quit();
        }
    }
    public void pause1()
    {
        endmenu1.SetActive(true);
        Time.timeScale = 0f;
    }
    public void resume1()
    {
        endmenu1.SetActive(false);
        Time.timeScale = 1f;
    }
    public void gamesebahar1(int sceneID)

    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);

    }


    // Start is called before the first frame update

}
