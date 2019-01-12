using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {
    
    private GameManager gm;

    // Use this for initialization
    void Start () {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Interact"))
        {
            Action();
        }

    }

    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "Escape")
        {
            gm.escape();
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        
        Gizmos.DrawSphere(transform.position, 1);
    }

    private void Action()
    {
        Vector3 startPos = transform.position; // umm, start position !
        Vector3 targetPos =  transform.forward; // variable for calculated end position

        RaycastHit tg;
        if(Physics.SphereCast(startPos, .5f, targetPos, out tg, 1.5f))
        {
            if (tg.collider.gameObject.tag.Equals("Art"))
            {
                gm.addValue(tg.collider.GetComponent<Value>().getValue());
                tg.collider.gameObject.tag = "Stolen";
            }
        }
        
    }
}
