using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float moveSpeed = 1f;
    bool moving = true;
    public GameObject path;
	// Use this for initialization
	void Start () {
		
	}
	void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("spikes"))
        {
            Vector3 temp = GameObject.Find("SpawnPoint").transform.position;
            this.transform.position = temp;
        }
    }
	// Update is called once per frame
	void Update ()
    {
        if(moving)
        {
            if (Input.GetButton("up"))
            {
                this.GetComponent<Rigidbody2D>().MovePosition(transform.position + Vector3.up * moveSpeed);
            }
            else if (Input.GetButton("down"))
            {
                this.GetComponent<Rigidbody2D>().MovePosition(transform.position + Vector3.down * moveSpeed);
            }
            else if (Input.GetButton("left"))
            {
                this.GetComponent<Rigidbody2D>().MovePosition(transform.position + Vector3.left * moveSpeed);
            }
            else if (Input.GetButton("right"))
            {
                this.GetComponent<Rigidbody2D>().MovePosition(transform.position + Vector3.right * moveSpeed);
            }
            if (Input.GetButtonDown("sight"))
            {
                if (path.activeInHierarchy) path.SetActive(false);
                else path.SetActive(true);
            }
        }

        checkCamera();
	}

    void checkCamera()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0.0)
        {
            StartCoroutine(slide(1.5f, "left", 10f));
        }
        else if (1.0 < pos.x)
        {
            StartCoroutine(slide(1.5f, "right", 10f));
        }
        else if (pos.y < 0.0)
        {
            StartCoroutine(slide(1.5f, "down", 10f));
        }
        else if (1.0 < pos.y)
        {
            StartCoroutine(slide(1.5f, "up", 10f));
        }
    }

    IEnumerator slide(float time, string dir, float distance)
    {
        float elapsed_time = 0f;
        Vector3 startingPos = Camera.main.transform.position;
        Vector3 offset = new Vector3();
        moving = false;
        if (dir == "left")
        {
            offset = Camera.main.transform.position - new Vector3(distance, 0f, 0f);
        }
        if (dir == "right")
        {
            offset = Camera.main.transform.position + new Vector3(distance, 0f, 0f);
        }
        if (dir == "up")
        {
            offset = Camera.main.transform.position + new Vector3(0f, distance, 0f);
        }
        if (dir == "down")
        {
            offset = Camera.main.transform.position - new Vector3(0f, distance, 0f);
        }

        while (elapsed_time < time)
        {
            Camera.main.transform.position = Vector3.Lerp(startingPos, offset, elapsed_time / time * 2.2f);
            elapsed_time += Time.deltaTime;
            yield return null;
        }
        moving = true;
    }
}

