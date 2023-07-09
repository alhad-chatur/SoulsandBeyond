using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ABILITYUSE : MonoBehaviour
{
    // Start is called before the first frame update
    private Image image;
   
    private float MaxHealth = 100f;
    public float CurrentHealth = 100f;
    public int collectable_factors;
    public bool IskeyEnabled_w;
    [SerializeField]        private float newFactor = 1f;
   
    Movement collectable;
    NewControls _newControls;
   
    private void Awake()
    {
        IskeyEnabled_w = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        _newControls = new NewControls();
        _newControls.Player.Enable();
        image = GetComponent<Image>();
        collectable = FindObjectOfType<Movement>();
     
    }

    // Update is called once per frame
    void Update()
    {
        collectable_factors = collectable.totalapples + 2;
     
        if(_newControls.Player.Rewind.ReadValue<float>()==1)
        {
            CurrentHealth -= Time.deltaTime* newFactor;
          
        }
        else
        {
            CurrentHealth += Time.deltaTime * collectable_factors;
        }
        
        image.fillAmount = CurrentHealth / MaxHealth;
        //Debug.Log(CurrentHealth);
        if (CurrentHealth > 100)
        {
            CurrentHealth = 100f;
        }
        else if(CurrentHealth <0)

        {
            CurrentHealth = 0f;
            Rewinder.canRewind = false;
        }
        else if(CurrentHealth >0){
            Rewinder.canRewind = true;
        }
       
        

    }
}
