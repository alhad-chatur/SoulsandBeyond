using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class totalscoreUI : MonoBehaviour
{
    public TextMeshProUGUI fruits;
    //  public TextMeshProUGUI foodhai;
    Movement food;
    // Start is called before the first frame update
    void Start()
    {
        food = FindObjectOfType<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        fruits.text = "X" + food.totalapples.ToString();
    }
}
