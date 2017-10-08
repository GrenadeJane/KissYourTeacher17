using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame : MonoBehaviour {

    public CHARACTERS m_eInteractWith;
    public Constellation m_script;
    public GameObject m_ARM;

	public void SendDone()
    {
      //  BaseInteraction.m_Instance.SetFirstConditionDone(m_eInteractWith);
        GetComponentInParent<Canvas>().enabled = false;
        m_script.enabled = true;
        m_ARM.SetActive(false);
    }
}
