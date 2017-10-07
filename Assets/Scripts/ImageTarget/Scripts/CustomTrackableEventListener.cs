using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CustomTrackableEventListener : MonoBehaviour
{
    [Header("Tracking")]
    public string trackableListened;

    [Header("Events")]
    public bool disableRenderer = false;
    public bool disableCollider = false;
    public bool disableCanvas = false;
    [Space(10)]
    public bool onThisGameObject = true;
    public bool onChildren = true;
    [Space(10)]
    public bool useCustomEvent = false;
    [Space(10)]
    public GameObject UITracking;

    void OnEnable()
    {
        CustomTrackableEventHandler.OnTrackingFoundEvent += OnTrackableFound;
        CustomTrackableEventHandler.OnTrackingLostEvent += OnTrackableLost;
    }

    void OnDisable()
    {
        CustomTrackableEventHandler.OnTrackingFoundEvent -= OnTrackableFound;
        CustomTrackableEventHandler.OnTrackingLostEvent -= OnTrackableLost;
    }

    void Start()
    {
        print("gameobject:  " + gameObject.name + " listen to trackable: " + trackableListened);

        // Disable checked elements on start
        if (onThisGameObject)
        {
            SetComponentsEnabled(false);
        }

        if (onChildren)
        {
            SetChildrenComponentsEnabled(false);
        }
    }

    void OnTrackableFound(string trackableFound)
    {
        if(trackableListened == trackableFound)
        {
            if (onThisGameObject)
            {
                SetComponentsEnabled(true);
            }

            if (onChildren)
            {
                SetChildrenComponentsEnabled(true);
            }

            if (useCustomEvent)
            {
                CustomEventOnTrackingFound();
            }
        }
    }

    void OnTrackableLost(string trackableFound)
    {
        if (trackableListened == trackableFound)
        {
            if (onThisGameObject)
            {
                SetComponentsEnabled(false);
            }

            if (onChildren)
            {
                SetChildrenComponentsEnabled(false);
            }

            if (useCustomEvent)
            {
                CustomEventOnTrackingLost();
            }
        }
    }

    void SetComponentsEnabled(bool bState)
    {
        if(disableRenderer && GetComponent<Renderer>() != null)
        {
            GetComponent<Renderer>().enabled = bState;
        }

        if (disableCollider && GetComponent<Collider>() != null)
        {
            GetComponent<Collider>().enabled = bState;
        }

        if (disableCanvas && GetComponent<Canvas>() != null)
        {
            GetComponent<Canvas>().enabled = bState;
        }
    }

    void SetChildrenComponentsEnabled(bool bState)
    {
        if(disableRenderer)
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);

            foreach (Renderer component in rendererComponents)
            {
                component.enabled = bState;
            }
        }
        
        if(disableCollider)
        {
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            foreach (Collider component in colliderComponents)
            {
                component.enabled = bState;
            }
        }

        if(disableCanvas)
        {
            Canvas[] canvasComponents = GetComponentsInChildren<Canvas>(true);

            foreach (Canvas component in canvasComponents)
            {
                component.enabled = bState;
            }
        }
    }
    
    void CustomEventOnTrackingFound()
    {
        // Custom event on tracking found
        UITracking.SetActive(false);
    }

    
    void CustomEventOnTrackingLost()
    {
        // Custom event on tracking lost
        UITracking.SetActive(true);
    }
}
