using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ObjectPooled : MonoBehaviour {

    Image
        m_Sprite;
    public virtual void Start( )
    {
        
    }


    public virtual void Show()
    {
        m_Sprite.enabled = true;
    }
    public virtual void Clear()
    {
        m_Sprite.enabled = false;

	}
}