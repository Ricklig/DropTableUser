using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    //for internal referencing
    private Rigidbody playerRB;
    private bool sprint;
    private GameManager gm;
    private Animator anim;


    public int speedFactor = 1;
    public MusicController mc;
    public AudioSource level1;
    public AudioSource level2;
    public AudioSource level3;
    

    // Use this for initialization
    void Start () {
        //Get rigidbody
        playerRB = GetComponent<Rigidbody>();
        sprint = false;
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update () {
        Move();
        if (Input.GetButtonDown("Sprint"))
        {
            sprint = true;
            anim.SetBool("Running", true);
        }
        if (Input.GetButtonUp("Sprint"))
        {
            sprint = false;
            anim.SetBool("Running", false);

        }
    }

    //move script
    private void Move()
    {
        float moveHorizontal = 0;
        float moveVertical = 0;
        if (sprint)
        {
            moveHorizontal = Input.GetAxis("Horizontal") * Time.deltaTime * 5.0f * speedFactor * (1-(2*gm.getQuantity()/100)) ;
            moveVertical = Input.GetAxis("Vertical") * Time.deltaTime * 5.000001f * speedFactor * (1 - (2 * gm.getQuantity() / 100));
        }
        else
        {
            moveHorizontal = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f * speedFactor * (1 - (2 * gm.getQuantity() / 100));
            moveVertical = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f * speedFactor * (1 - (2 * gm.getQuantity() / 100));
        }

        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
        {

            if (mc.getCurrentLevel() == 2)
            {
                level1.Play();
            }
            else if (mc.getCurrentLevel() == 3)
            {
                level1.Stop();
                level2.Play();
            }
            else if (mc.getCurrentLevel() == 4)
            {
                level2.Stop();
                level3.Play();
            }
        }
        else if (!Input.GetKey("a") && !Input.GetKey("d") && !Input.GetKey("w") && !Input.GetKey("s"))
        {

            level1.Stop();
            level2.Stop();
            level3.Stop();
        }



        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        playerRB.transform.Translate(movement, Space.World);

        // Please stop turning in the pause menu
        if (Time.timeScale != 0f)
        {
            //Get the Screen positions of the object
            Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

            //Get the Screen position of the mouse
            Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

            //Get the angle between the points
            float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen) - 90;
            //Debug.Log(transform.localRotation.y);
            if (moveHorizontal != 0 || moveVertical != 0)
            {
                if (transform.localRotation.y > -.3 && transform.localRotation.y < .3)
                {
                    //Debug.Log("up");
                    if (Mathf.Abs(moveHorizontal) > Mathf.Abs(moveVertical))
                    {
                        if (moveHorizontal < 0)
                        {
                            anim.SetInteger("New Int", 4);
                        }
                        else if (moveHorizontal > 0)
                        {
                            anim.SetInteger("New Int", 2);
                        }
                    }
                    else if (Mathf.Abs(moveHorizontal) < Mathf.Abs(moveVertical))
                    {
                        if (moveVertical > 0)
                        {
                            anim.SetInteger("New Int", 1);
                        }

                        else
                        {
                            anim.SetInteger("New Int", 3);
                        }

                    }
                }
                else if (Quaternion.Euler(new Vector3(0f, angle, 0f)).y > -.9 && Quaternion.Euler(new Vector3(0f, angle, 0f)).y <= -.3)
                {
                    //Debug.Log("left");
                    if (Mathf.Abs(moveHorizontal) > Mathf.Abs(moveVertical))
                    {
                        if (moveHorizontal < 0)
                        {
                            anim.SetInteger("New Int", 1);
                        }
                        else if (moveHorizontal > 0)
                        {
                            anim.SetInteger("New Int", 3);
                        }
                    }
                    else if (Mathf.Abs(moveHorizontal) <= Mathf.Abs(moveVertical))
                    {
                        //Debug.Log("HELP");
                        if (moveVertical > 0)
                        {
                            anim.SetInteger("New Int", 2);
                        }

                        else
                        {
                            anim.SetInteger("New Int", 4);
                        }
                    }
                    
                }
                else if (Quaternion.Euler(new Vector3(0f, angle, 0f)).y < .9 && Quaternion.Euler(new Vector3(0f, angle, 0f)).y >= .3)
                {
                    //Debug.Log("right");
                    if (Mathf.Abs(moveHorizontal) > Mathf.Abs(moveVertical))
                    {
                        if (moveHorizontal < 0)
                        {
                            anim.SetInteger("New Int", 3);
                        }
                        else if (moveHorizontal > 0)
                        {
                            anim.SetInteger("New Int", 1);
                        }
                    }
                    else if (Mathf.Abs(moveHorizontal) < Mathf.Abs(moveVertical))
                    {
                        if (moveVertical > 0)
                        {
                            anim.SetInteger("New Int", 4);
                        }

                        else
                        {
                            anim.SetInteger("New Int", 2);
                        }
                    }
                }
                else
                {
                    //Debug.Log("down");
                    if (Mathf.Abs(moveHorizontal) > Mathf.Abs(moveVertical))
                    {
                        if (moveHorizontal < 0)
                        {
                            anim.SetInteger("New Int", 4);
                        }
                        else if (moveHorizontal > 0)
                        {
                            anim.SetInteger("New Int", 2);
                        }
                    }
                    else if (Mathf.Abs(moveHorizontal) < Mathf.Abs(moveVertical))
                    {
                        if (moveVertical > 0)
                        {
                            anim.SetInteger("New Int", 3);
                        }

                        else
                        {
                            anim.SetInteger("New Int", 1);
                        }
                    }
                }
            }
            else 
            {
                anim.SetInteger("New Int", 0);
            }
        
            
           

            //Ta Daaa
            playerRB.transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
        }
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * -Mathf.Rad2Deg;
    }

    public bool GetSprint()
    {
        return sprint;
    }
}
