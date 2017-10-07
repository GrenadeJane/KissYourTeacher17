using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Vuforia;


public class ARImage : MonoBehaviour,  ITrackableEventHandler
{

    public GameObject
        m_BackGround; 
    public Canvas
        m_arCanvas;
    public Vector3
        m_vPosition;
    public Vector2
        m_vSizeButton;

	// Use this for initialization

	private void OnDestroy()
	{
		TrackableBehaviour trackableBehaviour = FindObjectOfType<TrackableBehaviour>();
		if (trackableBehaviour)
		{
            trackableBehaviour.UnregisterTrackableEventHandler(this);
		}
		m_arCanvas = null;
	}

    public virtual void Start()
    {
		TrackableBehaviour trackableBehaviour = FindObjectOfType<TrackableBehaviour>();
		if (trackableBehaviour)
		{
			trackableBehaviour.RegisterTrackableEventHandler(this);
		}
    }

    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.UNKNOWN &&
                 newStatus == TrackableBehaviour.Status.NOT_FOUND)
        {
            // Ignore this specific combo
            return;
        }
        else
        {
            OnTrackingLost();
        }
    }

    public virtual void DestroyButton()
    {

    }

    public void OnEnable()
    {
		m_arCanvas.enabled = true;
	}

    public void OnDisable()
    {
		m_arCanvas.enabled = false;
	}



   

    protected virtual void OnTrackingFound() {
        m_arCanvas.enabled = true;
    }

    protected virtual void OnTrackingLost() {
        m_arCanvas.enabled = false;
	}

    public virtual void OnClickButton()
	{ }

	public virtual void OpenFullCanvas()
	{ }

	public virtual void CloseFullCanvas()
	{ }
}
