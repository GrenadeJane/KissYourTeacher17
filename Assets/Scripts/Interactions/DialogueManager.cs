using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour {

    public static DialogueManager m_instance;

    public static DialogueManager m_Instance {
        get { return m_instance; }
        set { m_instance = value;  }
    }

    private void Awake()
    {
        if (!PUBLIC.CreateInstance(ref m_instance, this, gameObject))
            return;


    }

    public void NewTarget ( string l_stargetname ) 
    {
        // :: depend of the target scanned, and the condition required is checked do some action 
        // :: tree of decisions 
        // :: 
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
