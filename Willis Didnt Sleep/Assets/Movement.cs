using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Movement : MonoBehaviour
{

    public enum PacmanState
    {
        Running,
//        Jumping,
//        Flying,
        Rolling,
        Damaged,
//        Chomp,
//        Megachomp,
        Ducking,
        ChargeUp,
        Dead
    }

    public enum Chomp
    {
        chomp,
        notChomp
    }

    public PacmanState pacstate = PacmanState.Running;
    public Chomp chompState = Chomp.notChomp;
    public GameObject pacMan;
    public GameObject camera;
    public Slider floatTimer;
    public Slider healthSlider;
    public Slider suparArmorSlider;
    public Slider chargeAndDashSlider;
    public Text pelletCounter;
    public int health = 3;
    public int pelletCount = 0;
    public int superPelletCounter = 0;
    public int superArmor = 0;
    public float speed = 15.0f;
    public float jumpSpeed = 10.0f;
    public float gravity = 20.0f;
    public float counter = 0.0f;
    private Vector3 direction = Vector3.zero;
    public Vector3 newRotation;
    public float flightTimer = 1.8f;
    public float chargeTimer = 1.0f;
    public float rolltimer = 1.0f;
    public bool superpowered = false;
    public float chompTimer = 0.5f;

    Animator anim;
    // Use this for initialization
    void Start()
    {
        pacMan = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        newRotation = Vector3.zero;
        pelletCounter.text = "0";
        floatTimer.value = 0;
        chargeAndDashSlider.value = 0;
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        CharacterController contr = pacMan.GetComponent<CharacterController>();
        if (superPelletCounter == 4)
        {
            superpowered = true;
        }
        
        switch (pacstate)
        {
            case PacmanState.Running:
                speed = (15.0f + ((float)pelletCount / 100));
                rolltimer = 1.0f;
                chargeTimer = 1.0f;
                if (contr.isGrounded)
                {
                    direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                    direction = transform.TransformDirection(direction);
                    direction *= speed;
                    if (Input.GetButtonDown("Jump"))
                    {
                      direction.y = jumpSpeed;
                      flightTimer = 1.8f;
                      floatTimer.value = 1.8f;
                      anim.SetBool("JumpB", true);
                      anim.SetFloat("JSpeed", 1);

                   }
                    else
                    {
                        anim.SetBool("JumpB", false);
                    }

                    if (Input.GetButtonDown("Fire2"))
                    {
                        crouch();
                    }
                    else
                    {
                        anim.SetBool("CrouchButton", false);
                    }
                }
               else
               {
                   direction = new Vector3(Input.GetAxis("Horizontal"), direction.y, Input.GetAxis("Vertical"));
                   direction = transform.TransformDirection(direction);
                   direction.x *= speed;
                   direction.z *= speed;
               }

                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                {
                    anim.SetFloat("Speed", 1);
                }
                else
                {
                    anim.SetFloat("Speed", 0);
                }

                if (direction.y == 0)
                {
                    anim.SetFloat("JSpeed", 0);
                }

               if (Input.GetButton("Fire1"))
               {
                   if (flightTimer > 0)
                   {
                       fly();
                       floatTimer.value -= Time.deltaTime;
                   }
               }
               else if (Input.GetButton("Submit"))
               {
                   if (chompTimer > 0)
                   {
                       chompState = Chomp.chomp;
                       anim.SetBool("OnPellet", true);
                       chompTimer -= Time.deltaTime;
                   }
                   else
                   {
                       chompState = Chomp.notChomp;
                       anim.SetBool("OnPellet", false);
                   }
               }
                if (chompState == Chomp.notChomp){
                    chompTimer = 0.5f;
                }
               direction.y -= gravity * Time.deltaTime;

               contr.Move(direction * Time.deltaTime);
               newRotation = pacMan.transform.eulerAngles;
               newRotation.y = camera.transform.eulerAngles.y;
               //        lookdirection.eulerAngles = new Vector3(pacMan.transform.rotation.x, camera.transform.rotation.y, pacMan.transform.rotation.z);
               pacMan.transform.eulerAngles = newRotation;
               break;
            
            case PacmanState.Ducking:
                direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                direction = transform.TransformDirection(direction);
                direction *= speed;
                direction *= 0.3f;
                if (Input.GetButtonDown("Fire3"))
                {
                    pacstate = PacmanState.ChargeUp;
                }
               if (Input.GetButtonUp("Fire2")||!contr.isGrounded)
               {
                   pacstate = PacmanState.Running;
               }

               if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
               {
                   anim.SetFloat("Speed", 1);
               }
               else
               {
                   anim.SetFloat("Speed", 0);
               }
               direction.y -= gravity * Time.deltaTime;
                contr.Move(direction * Time.deltaTime);
               newRotation = pacMan.transform.eulerAngles;
               newRotation.y = camera.transform.eulerAngles.y;
               //        lookdirection.eulerAngles = new Vector3(pacMan.transform.rotation.x, camera.transform.rotation.y, pacMan.transform.rotation.z);
               pacMan.transform.eulerAngles = newRotation;
                break;
            case PacmanState.Damaged:

               break;
            case PacmanState.Rolling:
                   direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                   direction = transform.TransformDirection(direction);
                   direction *= speed;
                   direction *= 5;

                anim.SetBool("DashButton", true);

                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                {
                    anim.SetFloat("Speed", 1);
                }
                else
                {
                    anim.SetFloat("Speed", 0);
                }

                   direction.y -= gravity * Time.deltaTime;
                   
                   contr.Move(direction * Time.deltaTime);
                   newRotation = pacMan.transform.eulerAngles;
                   newRotation.y = camera.transform.eulerAngles.y;
                   //        lookdirection.eulerAngles = new Vector3(pacMan.transform.rotation.x, camera.transform.rotation.y, pacMan.transform.rotation.z);
                   pacMan.transform.eulerAngles = newRotation;
                   rolltimer -= Time.deltaTime;
                   chargeAndDashSlider.value -= Time.deltaTime;
               if (rolltimer <= 0)
               {
                   pacstate = PacmanState.Running;
                   anim.SetBool("DashButton", false);
               }
               break;

            case PacmanState.ChargeUp:

               if (Input.GetButtonUp("Fire3"))
               {
                   pacstate = PacmanState.Ducking;
                   break;
               }
               
               chargeTimer -= Time.deltaTime;
               chargeAndDashSlider.value = 1 - Time.deltaTime;
               if (chargeTimer <= 0)
               {
                   pacstate = PacmanState.Rolling;
               }

               contr.Move(direction * Time.deltaTime);
               newRotation = pacMan.transform.eulerAngles;
               newRotation.y = camera.transform.eulerAngles.y;
               //        lookdirection.eulerAngles = new Vector3(pacMan.transform.rotation.x, camera.transform.rotation.y, pacMan.transform.rotation.z);
               pacMan.transform.eulerAngles = newRotation;
               break;
            case PacmanState.Dead:

                break;
            

        }
        
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("cherry"))
        {
            superArmor = 3;
            suparArmorSlider.value = superArmor;
            anim.SetBool("OnPellet", true);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("strawberry"))
        {
            jumpSpeed *= 2;
            anim.SetBool("OnPellet", true);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("ghost") && !superpowered)
        {
            if (superArmor > 0)
            {
                superArmor--;
                suparArmorSlider.value = superArmor;
            }
            else
            {
                health--;
                healthSlider.value = health;
            }
        }
        else if (other.gameObject.CompareTag("ghost") && superpowered && chompState == Chomp.chomp)
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("pellet"))
        {
            pelletCount++;
            pelletCounter.text = pelletCount.ToString();
            anim.SetBool("OnPellet", true);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("superPelletPart"))
        {
            superPelletCounter++;
            anim.SetBool("OnPellet", true);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("enemy"))
        {
            if (chompState == Chomp.chomp||superpowered)
            {
                Destroy(other.gameObject);
            }
            else
            {
                health--;
                healthSlider.value = health;
            }
        }

        anim.SetBool("OnPellet", false);
    }

    public void fly()
    {
        flightTimer -= Time.deltaTime;
        direction.y = 0;
    }

    public void crouch()
    {
        pacstate = PacmanState.Ducking;
        anim.SetBool("CrouchButton", true);
    }
    
}