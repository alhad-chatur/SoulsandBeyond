using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement1 : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    NewControls inaction;
    float inputx, inputy;
    public float xforce = 2.0f;
    public float jumpforce = 20.0f;
    public static bool isgrounded, isjumping, isfalling;
    public PhysicMaterial ground;
    ConstantForce cf;
    public float frictionforce = 2.0f;
    private characterAnimator1 _characterAnimator;
    Movement fruits1;
    public List<GameObject> fruits;
    private GameObject x;
    public Transform button;
    private bool iscollision = false;
    public float force;
    private Rewinder rewinder;
    public AudioClip collsound,stabsound;
    AudioSource asur;
  ///  public static bool youwon=false;
    //public static bool youhaswon=false;

    void Start()
    {
        _characterAnimator = GetComponentInChildren<characterAnimator1>();

        rb = GetComponent<Rigidbody>();
        inaction = new NewControls();
        inaction.Player.Enable();
        isgrounded = false;
        cf = GetComponent<ConstantForce>();
        fruits1 = FindObjectOfType<Movement>();
        rewinder = FindObjectOfType<Rewinder>();

        asur = FindObjectOfType<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
         if (isgrounded &&  !dmanager.isdying2)
        {
            _characterAnimator.activeState = 1;
        }
        else if( !dmanager.isdying2)
        {
            _characterAnimator.activeState = 2;
        }
        if (!dmanager.isdying1 && !dmanager.isdying2 && !rewinder.isRewinding)
        {
            inputx = inaction.Player.Movement.ReadValue<float>();
            //if (Input.GetButtonDown("Jump") && isgrounded == true)
            if (inaction.Player.Jump.WasPerformedThisFrame() && isgrounded ==true)
            {
                inputy = 1;
                isgrounded = false;
                isjumping = true;
                _characterAnimator.Jump();
                _characterAnimator.activeState = 2;
                Vector3 jump = new Vector3(0, -inputy * jumpforce, 0);
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
        afterwinning.ninside2 = 0;
        Vector3 speed = new Vector3(inputx * xforce, 0, 0);
        _characterAnimator.speedTarget = Mathf.Abs(inputx);

        transform.position += speed * Time.fixedDeltaTime;
        if (iscollision == true)
        {
            if (fruits.Count != 0)
            {
                for (int i = 0; i < fruits.Count; i++)
                {
                    Vector3 directionofmove = (button.position - fruits[i].transform.position).normalized;
                    fruits[i].transform.position += directionofmove * Time.deltaTime * force;
                    if ((button.position - fruits[i].transform.position).magnitude <= 0.5f)
                    {
                        Destroy(fruits[i]);
                        fruits1.totalapples++;
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
        if (rb.velocity.y != 0 && inputx != 0)
        {
            transform.position += new Vector3(0, rb.velocity.y * Time.fixedDeltaTime, 0);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("winarea"))
        {
            afterwinning.ninside2 = 1;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("spike"))
        {
            _characterAnimator.activeState = 0;
            if (dmanager.isdying2 != true)
            {
                dmanager.isdying2 = true;
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


            fruits.Add(x);

            x.GetComponent<BoxCollider>().enabled = false;

            asur.PlayOneShot(collsound);

        }
      
    }

}
