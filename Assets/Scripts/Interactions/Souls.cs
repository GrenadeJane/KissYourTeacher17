using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Souls : MonoBehaviour {
     public float m_fSpeed;

    Collider2D collider2D;
    private void Awake()
    {
        collider2D = GetComponent<Collider2D>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down*Time.deltaTime*m_fSpeed);
		if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
		{
		//	Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (collider2D.OverlapPoint(wp))
			{
				//your code
				Debug.Log("Hello");
			}
		}
	}


}
