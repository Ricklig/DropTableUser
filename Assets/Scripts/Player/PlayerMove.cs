using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    //for internal referencing
    private Rigidbody playerRB;
    private bool sprint;

    // Use this for initialization
    void Start () {
        //Get rigidbody
        playerRB = GetComponent<Rigidbody>();
        sprint = false;

    }

    // Update is called once per frame
    void Update () {
        Move();

        if (Input.GetButtonDown("Sprint"))
        {
            sprint = true;
        }
        if (Input.GetButtonUp("Sprint"))
        {
            sprint = false;
        }
	}

    //move script
    private void Move()
    {
        float moveHorizontal = 0;
        float moveVertical = 0;
        if (sprint)
        {
            moveHorizontal = Input.GetAxis("Horizontal") * Time.deltaTime * 5.0f;
            moveVertical = Input.GetAxis("Vertical") * Time.deltaTime * 5.0f;
        }
        else
        {
            moveHorizontal = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f;
            moveVertical = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
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
