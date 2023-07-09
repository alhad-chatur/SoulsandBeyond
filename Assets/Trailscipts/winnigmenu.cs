using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class winnigmenu : MonoBehaviour
{
    [SerializeField]
    GameObject winningmenu1;
    [SerializeField] GameObject endmenu;
    public GameObject blur;
    public TextMeshProUGUI collectablecount;
    public TextMeshProUGUI timecount;
    public GameObject androidcanvas;
    
    public GameObject[] selectables1;
    //  public List<GameObject> selectables;
    Movement food;
    [SerializeField] public GameObject FILL;
    [SerializeField] public GameObject EMPTY;
    [SerializeField] public GameObject OBJ;
    public float Time1;
    private static readonly int End = Animator.StringToHash("end");

    //SerializeField] public GameObject post;
    //  [SerializeField] public GameObject glow;
    // public GameObject pause1;

    [Header("Level Loading")] 
    public string sceneToLoad = "cave";
    [SerializeField] private Animator animator;

    private void Start()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        food = FindObjectOfType<Movement>();
        //// newcontrol = new NewControls();
        // newcontrol.Player.Enable();
    
     

    }

    public void Update()
    {
        //print(activeButtonIndex1);
       // print(selectables1[0]);
       // print(selectables1[1]);
        Time1 += Time.deltaTime;
        //print(Time1);
    

      
       
        if (afterwinning.win == true && afterwinning.haswon ==1)
        {   
            FILL.SetActive(false);
            EMPTY.SetActive(false);
            OBJ.SetActive(false);
            
            endmenu.SetActive(false);
            androidcanvas.SetActive(false);

            Cursor.lockState = CursorLockMode.None;
            // activeButtonIndex = 0;
            // Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 0f;
        //  Cursor.lockState = CursorLockMode.Locked;
            winningmenu1.SetActive(true);
            blur.SetActive(true);
            timecount.text = System.Math.Round(Time1,2).ToString() + "Sec";
            collectablecount.text = "X" + food.totalapples.ToString();
            afterwinning.haswon = 2;
        }
        
    }
    public void pause()
    {


    }

    public void resume()
    {
        if (winningmenu1.activeInHierarchy == true)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(sceneToLoad);


         
            Time.timeScale = 1f;
            winningmenu1.SetActive(false);
            blur.SetActive(false);
            FILL.SetActive(true);
            EMPTY.SetActive(true);
            OBJ.SetActive(true);
            androidcanvas.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;

        }



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
        winningmenu1.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void resume1()
    {
        winningmenu1.SetActive(false);
        Time.timeScale = 1f;
    }
    public void gamesebahar1(int sceneID)

    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);

    }

    // Start is called before the first frame update

}
