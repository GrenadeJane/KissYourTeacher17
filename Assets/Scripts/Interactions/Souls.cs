using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Souls : MonoBehaviour {
     public float m_fSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down*Time.deltaTime*m_fSpeed);
	}
}
