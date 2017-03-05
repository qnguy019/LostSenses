using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float moveSpeed = 1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetButton("up"))
        {
            this.GetComponent<Rigidbody2D>().MovePosition(transform.position + Vector3.up * moveSpeed);
        }	
        else if(Input.GetButton("down"))
        {
            this.GetComponent<Rigidbody2D>().MovePosition(transform.position + Vector3.down * moveSpeed);
        }	
        else if(Input.GetButton("left"))
        {
            this.GetComponent<Rigidbody2D>().MovePosition(transform.position + Vector3.left * moveSpeed);
        }
        else if(Input.GetButton("right"))
        {
            this.GetComponent<Rigidbody2D>().MovePosition(transform.position + Vector3.right * moveSpeed);
        }

        checkCamera();
	}

    void checkCamera()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(this.transform.position);
        if(pos.x < 0.0)
        {
            Debug.Log("Camera at left position!");
            StartCoroutine("slideLeft");
        }
        else if(1.0 < pos.x)
        {

        }
        else if(pos.y < 0.0)
        {

        }
        else if(1.0 < pos.y)
        {

        }
    }

    IEnumerator slide(int time, string dir, int distance)
    {
        Debug.Log("Sliding");
        float elapsed_time = 0f;
        Vector3 startingPos = Camera.main.transform.position;
        Vector3 offset = new Vector3();
        if(dir == "left")
        {
            offset = Camera.main.transform.position - new Vector3(distance, Camera.main.transform.position.y, 0f);
        }
        if (dir == "right")
        {
            offset = Camera.main.transform.position - new Vector3(-distance, Camera.main.transform.position.y, 0f);
        }
        if (dir == "up")
        {
            offset = Camera.main.transform.position - new Vector3(Camera.main.transform.position.x, distance, 0f);
        }
        if (dir == "down")
        {
            offset = Camera.main.transform.position - new Vector3(Camera.main.transform.position.x, -distance, 0f);
        }

        while(elapsed_time < time)
        {
            Camera.main.transform.position = Vector3.Lerp(startingPos, offset, elapsed_time/time * 2.2f);
            elapsed_time += Time.deltaTime;
            yield return null;
        }
    }
}
