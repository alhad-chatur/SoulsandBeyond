using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class pausemenu : MonoBehaviour
{   [SerializeField]
    GameObject pauseenu;
    public GameObject blur;
    public GameObject kamkharab;
    winnigmenu timehai;
    public TextMeshProUGUI playername;
    ProfileManager profile_manager;
    public GameObject[] selectables1;
    //public Animator anim1;
    //  public List<GameObject> selectables;
    private NewControls newcontrol;
    [SerializeField] public GameObject FILL;
    [SerializeField] public GameObject EMPTY;
    [SerializeField] public GameObject OBJ;
    [SerializeField] public GameObject end;
    [SerializeField] public GameObject wining;
        [SerializeField]public TextMeshProUGUI timecount1;
    public GameObject andcan;
    //  [SerializeField] public GameObject wining;
    private NewControls _newControls;

    private void Start()
    {   
       
       profile_manager = FindObjectOfType<ProfileManager>();
        timehai = FindObjectOfType<winnigmenu>();
        _newControls = new NewControls();
        _newControls.Player.Enable();

        //  newcontrol = new NewControls();
        //  newcontrol.Player.Enable();

        //   selectable = pauseenu.transform.childCount;
        //    selectables1 = new GameObject[selectable];

        //   for (int i = 0; i < selectable; i++)
        // {
        //      selectables1[i] = pauseenu.transform.GetChild(i).gameObject;

        //selectables1[i].SetActive(false);
        //  }
        // 
    }

    public void Update()
    {

       // Vector3 small = new Vector3(0.16f, 0.16f, 0.16f);
      //  Vector3 big = new Vector3(0.20f, 0.20f, 0.20f);
      
        
        //print(newcontrol.Player.Pause.IsPressed());
        //if (Input.GetKeyDown("escape") && end.active==false && wining.active == false)
        if (_newControls.Player.Pause.WasPressedThisFrame() && end.activeInHierarchy==false && wining.activeInHierarchy == false)
        {
          
            FILL.SetActive(false);
            EMPTY.SetActive(false);
            OBJ.SetActive(false);
            andcan.SetActive(false);
            
         //   wining.SetActive(false);
            //newcontrol.Player.Disable();
            //newcontrol.UI.Enable();
            //allSelec = pauseenu.transform.GetChild(0).gameObject;
            //   print(allSelectables);
        playername.text = profile_manager.ProfilesData.savedProfiles[profile_manager.ProfilesData.activePlayerProfileIndex].profileName;
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            pauseenu.SetActive(true);
            blur.SetActive(true);
            timecount1.text = System.Math.Round(timehai.Time1, 2).ToString() + "Sec";
            timecount1.text = timehai.Time1.ToString();

        }
    
    }
    public void pause()
    {
        
        
    }
    public void resume() {
        if (pauseenu.activeInHierarchy == true)
        {

                Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
           kamkharab.transform.localScale = new Vector3(0.14f, 0.14f, 0.14f);

            pauseenu.SetActive(false);
            blur.SetActive(false);

            FILL.SetActive(true);
            EMPTY.SetActive(true);
            OBJ.SetActive(true);
            andcan.SetActive(true);
            // }
        }
        

    }
    public void restart()
    {
        Scene active = SceneManager.GetActiveScene();
        SceneManager.LoadScene(active.name);
    }
    public void gamesebahar()
    {

        {
            Application.Quit();
        }
    }
    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void resume1()
    {   
        pauseenu.SetActive(false);
          pauseenu.SetActive(false);
            blur.SetActive(false);

            FILL.SetActive(true);
            EMPTY.SetActive(true);
            OBJ.SetActive(true);
        Time.timeScale = 1f;
    }
    public void gamesebahar1(int sceneID)

    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);

    }


    // Start is called before the first frame update

}
