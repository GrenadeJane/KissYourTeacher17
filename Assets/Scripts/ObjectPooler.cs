using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///// <summary>
///// Here a generic class to instantiate a certain amount of prefab and get it when needed, if the max is reached
///// it instantiate new one if the condition is true
///// When the clear function in the parent is called, the current ojbect shown are desactivated
///// And you can recall the GetPool with empty obj
///// </summary>
/// 
public class ObjectPooler : MonoBehaviour {

    public GameObject
        m_tPrefab;
    public int
        m_iMaxAmount;
    public bool
        m_bWillGrow = false;

    public List<ObjectPooled>
        m_PooledScripts;
    public GameObject GetPooledObject () // :: get the next one 
    {
        foreach ( ObjectPooled obj in m_PooledScripts )
        {
            if( !obj.enabled )
            {
                // obj.SetActive(true);
                obj.Show();
                obj.enabled = true;
                return obj.gameObject;
            } 
        }

        if ( m_bWillGrow )
        {
            GameObject obj = Instantiate(m_tPrefab) as GameObject;
            obj.transform.SetParent(transform);
            ObjectPooled script = obj.GetComponent<ObjectPooled>();
            m_PooledScripts.Add(script);
            return obj;
        }

        return null;
    }

    public void Start()
    {
        m_PooledScripts = new List<ObjectPooled>();

        for (int i = 0; i < m_iMaxAmount; i ++ )
        {
            GameObject obj = Instantiate(m_tPrefab) as GameObject;
            obj.transform.SetParent(transform);
            ObjectPooled script = obj.GetComponent<ObjectPooled>();
            m_PooledScripts.Add(script);
            script.enabled = false;
        }
        //:: instantiate here the prefab
    }

    public void Clear ()
    {
        foreach ( ObjectPooled obj in m_PooledScripts )
        {
            obj.Clear();
            obj.enabled = false;
        }

    }


}