using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    NewControls inaction;
    float inputx, inputy;
    public float xforce = 2.0f;
    public float jumpforce = 20.0f;
    public static bool isgrounded,isjumping,isfalling;
    public PhysicMaterial ground;
    ConstantForce cf;
    public float frictionforce = 2.0f;
    public List<GameObject> fruits;
    //public Animator anim;
    public int totalapples = 0;
    public Transform button;
    private GameObject x;
    private bool iscollision = false;
    int appleanim, justid;
    public float force;
    private characterAnimator _characterAnimator;
    WaitForSeconds appleanimdelay = new WaitForSeconds(0.2f);
    [SerializeField] private float startTime = 0f;
    private float time = 0f;
    private Rewinder rewinder;
    public AudioClip collectiblesound,stabsound;
    AudioSource asur;
 
    void Start()
    {
        
        _characterAnimator = GetComponentInChildren<characterAnimator>();
        rb = GetComponent<Rigidbody>();
        inaction = new NewControls();
        inaction.Player.Enable();
        isgrounded = false;
        cf = GetComponent<ConstantForce>();
        rewinder = FindObjectOfType<Rewinder>();
        asur = GetComponent<AudioSource>();
    }

   
    void Update()
    {
        if (time < startTime)
        {
            time += Time.deltaTime;
            return;
        }
        if (isgrounded && !dmanager.isdying1 )
        {
            _characterAnimator.activeState = 1;
        }
        else if(!dmanager.isdying1 )
        {
            _characterAnimator.activeState = 2;
        }
        if (!dmanager.isdying1 &&!dmanager.isdying2 && !rewinder.isRewinding)
        {
            
            inputx = inaction.Player.Movement.ReadValue<float>();
            if (inaction.Player.Jump.WasPerformedThisFrame() && isgrounded == true)
            {
                inputy = 1;
                isgrounded = false;
                isjumping = true;
                _characterAnimator.Jump();
                _characterAnimator.activeState = 2;
                Vector3 jump = new Vector3(0, inputy * jumpforce, 0);
                rb.AddForce(jump);

            }
            else
                inputy = 0;

            if (inputx != 0)
                transform.right = new Vector3(inputx, 0, 0);

        }
        else
        {
            inputx = 0;
            inputy = 0;
        }

    }
    private void FixedUpdate()
    {
        afterwinning.ninside1 = 0;
        if (time < startTime)
        {
            return;
        }
        Vector3 speed = new Vector3(inputx * xforce, 0, 0);
        _characterAnimator.speedTarget = Mathf.Abs(inputx);

        transform.position += speed *Time.fixedDeltaTime;
        // This part of code creates a list and when player collides with collactables
        // add that object in list, give force to that collactable iin direction of button and then destroy it, i.e pop it from the scipt
        if (iscollision == true)
        {
            if (fruits.Count != 0)
            {
                for (int i = 0; i < fruits.Count; i++)
                {
                    Vector3 directionofmove = (button.position - fruits[i].transform.position).normalized;
                    fruits[i].transform.position += directionofmove * (Time.deltaTime * force);
                    if ((button.position - fruits[i].transform.position).magnitude <= 0.5f)
                    {
                       /// anim.SetBool(appleanim, true);
                        StartCoroutine(appleanimationhai());
                        Destroy(fruits[i]);
                        totalapples++;
                        fruits.Remove(fruits[i]);

                    }

                }

            }
            if (fruits.Count == 0)
            {
                iscollision = false;
            }

        }
    }
    
    private void OnCollisionStay(Collision collision)
    {
        if (rb.velocity.y != 0  && inputx != 0)
        {
            transform.position += new Vector3(0, rb.velocity.y * Time.fixedDeltaTime, 0);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
        
        if (other.CompareTag("winarea"))
        {
            afterwinning.ninside1 = 1;
        }

    }
    IEnumerator appleanimationhai()
    {
        yield return appleanimdelay;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("spike"))
        {
            _characterAnimator.activeState = 0;
            if (dmanager.isdying1 != true)
            {
                dmanager.isdying1 = true;
                asur.PlayOneShot(stabsound);
            }
                
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("apple"))
        {
            iscollision = true;

            x = collision.gameObject;

            fruits.Add(x); //adding collectables that collided with player to list

            x.GetComponent<BoxCollider>().enabled = false;

            asur.PlayOneShot(collectiblesound);

        }
    }

}
