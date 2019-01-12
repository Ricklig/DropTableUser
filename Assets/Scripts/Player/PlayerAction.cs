using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Interact"))
        {
            Action();
        }
    }

    private void Action()
    {
        Vector3 startPos = transform.position; // umm, start position !
        Vector3 targetPos = transform.position + transform.forward; // variable for calculated end position

        float angle = Vector2.Angle(startPos, targetPos) + 30;

        int startAngle = (int)(-angle * 0.5); // half the angle to the Left of the forward
        int finishAngle = (int)(angle * 0.5); // half the angle to the Right of the forward

        // the gap between each ray (increment)
        int inc = (int)(30 / 5);


        // step through and find each target point
        for (int i = startAngle; i < finishAngle; i += inc) // Angle from forward
        {
            targetPos = (Quaternion.Euler(0, i, 0) * transform.forward).normalized * 1.5f + transform.position;

            RaycastHit hit;
            if(Physics.Linecast(startPos, targetPos, out hit))
                {
                    if(hit.collider.gameObject.tag.Equals("Art"))
                    {
                        Debug.Log("hit");
                    hit.collider.gameObject.tag = "Stolen";
                     }
                }
            


            // to show ray just for testing
            Debug.DrawLine(startPos, targetPos, Color.green, 5.5f);
        }
    }
}
