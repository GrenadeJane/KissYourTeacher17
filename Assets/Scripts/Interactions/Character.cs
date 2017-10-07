

using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    public CHARACTERS m_eCharacter;

    public void SendInteraction ()
    {
        BaseInteraction.m_Instance.Interact(m_eCharacter);
    }
}
