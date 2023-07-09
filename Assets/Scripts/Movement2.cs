using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using Unity.VisualScripting;
using UnityEngine;

public class Movement2 : MonoBehaviour
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
    public List<GameObject> fruits;
    //public Animator anim;
    public int totalapples = 0;
    public Transform button;
    private GameObject x;
    int appleanim, justid;
    public float force;
    private CharacterAnimator2 _characterAnimator;
    WaitForSeconds appleanimdelay = new WaitForSeconds(0.2f);
    [SerializeField] private float startTime = 0f;
    private float time = 0f;
    private Rewinder rewinder;


    void Start()
    {
        //appleanim = Animator.StringToHash("apple");
        _characterAnimator = GetComponentInChildren<CharacterAnimator2>();
        rb = GetComponent<Rigidbody>();
        inaction = new NewControls();
        Cursor.lockState = CursorLockMode.Locked;
        inaction.Player.Enable();
        isgrounded = false;
        cf = GetComponent<ConstantForce>();
    }

    // Update is called once per frame
    void Update()
    {
        if (time < startTime)
        {
            time += Time.deltaTime;
            return;
        }
        if (isgrounded)
        {
            _characterAnimator.activeState = 1;
        }
        else 
        {
            _characterAnimator.activeState = 2;
        }

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


            /* if (isgrounded == true)
             {
                 ground.dynamicFriction = 0f;
                 ground.staticFriction = 0f;
             }
             else
             {
                 ground.dynamicFriction = 0.2f;
                 ground.staticFriction = 0.2f;
             }*/
    }
        private void FixedUpdate()
        {
           if (time < startTime)
            {
                return;
            }
            Vector3 speed = new Vector3(inputx * xforce, 0, 0);
            _characterAnimator.speedTarget = Mathf.Abs(inputx);

            transform.position += speed * Time.fixedDeltaTime;
            // rb.velocity = new Vector3(inputx * xforce, rb.velocity.y, rb.velocity.z);
           /* if (iscollision == true)
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
            //rb.centerOfMass += speed * Time.fixedDeltaTime;

            //if ((inaction.Player.Movement.ReadValue<float>() == 0 && rb.velocity.x != 0) || (inaction.Player.Movement.ReadValue<float>() * rb.velocity.x < 0))
            //  cf.force = new Vector3(-rb.velocity.x * frictionforce, cf.force.y, cf.force.z);
            // rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
        */
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
                afterwinning.ninside1 = 1;
            }

        }
/*        IEnumerator appleanimationhai()
        {
            yield return appleanimdelay;
            // anim.SetBool(appleanim, false);
        }
*/


}
/*    
    void input()
    {
        if (transform.position.x < -28.0f)
        {
            if (Input.GetAxisRaw("Horizontal") == 1)
                movementx = 1;
            else
                movementx = 0;
        }
        else
            movementx = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isgrounded)
        {
            isgrounded = false;
            isjumping = true;
            //rb.AddForce(new Vector2(0f, jumpforce), ForceMode2D.Impulse);
            rb.velocity = Vector2.up * jumpforce;
            energysystem.intenergy -= 5.0f;
        }

    }

    void playermovement()
    {

        // movement.x = Input.GetAxisRaw("Horizontal"); for top down game
        //rb.velocity= movement*moveforce;
        // movementx = Input.GetAxisRaw("Horizontal");

        if (movementx != 0)
        {
            action = 1;
            energysystem.intenergy -= 0.1f;//0.1f;
        }
        Vector3 movement = new Vector3(movementx, 0f, 0f);
        Vector2 speed = new Vector2(inaction.Player.Movement.ReadValue<float>();
     //   Vector2 speed = new Vector2(inaction.Player.Movement.ReadValue<float>();

        transform.position += movement * Time.deltaTime * moveforce;
    }

    void playerjump()
    {

        if (isjumping == true)
        {
            action = 2;
            if (isgrounded == true)
                isjumping = false;
        }

    }

    void playerfalling()
    {
        if (isjumping == false && isgrounded == false)
        {
            isfalling = true;
            action = 2;
        }
        else
            isfalling = false;
    }

    public void playerflip()
    {
        {
            // Debug.Log("ravi");
            m_facingright = !m_facingright;
            transform.Rotate(0f, -180f, 0f);
            //  CreateDust();
        }
    }
    IEnumerator deathanimation1()
    {
        yield return deathdelay;
        Time.timeScale = 0f;
        pausemenu.SetActive(true);
    }
    public void deathanimation()
    {
        if (energysystem.intenergy < 0)
        {
            action = 3;
            StartCoroutine(deathanimation1());
        }

action 0-> idle 
action 1 && isfalling =false -> jump
action 1 && isfalling =true -> falling
action 2-> death


    }

}


    */
