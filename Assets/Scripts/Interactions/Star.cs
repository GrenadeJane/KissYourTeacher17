using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Star : MonoBehaviour {

    public int m_iStarIndex;

    public void OnMouseEnter()
    {
		Debug.Log("HIT" + m_iStarIndex);
        Constellation.m_instance.AddStar(m_iStarIndex, transform.localPosition);
    }
  

    public void AddStar()
    {
		Debug.Log("Enter Star n°" + m_iStarIndex);
	}
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

