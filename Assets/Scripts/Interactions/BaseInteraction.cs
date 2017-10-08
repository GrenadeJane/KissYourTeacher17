using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

public enum CHARACTERS 
{
    ATHENA,
    HERMES,
    MEDUSA
}

public class BaseInteraction : MonoBehaviour
{
    protected Dictionary<int, UnityAction> m_ActionsDictionary;

    public Hermes m_Hermes;
    public Athena m_Athena;
    Dictionary<CHARACTERS, UnityAction[]> m_CharactersDictionary;
	Dictionary<CHARACTERS, bool[]> m_ConditionsDictionary;
    public Text m_TextShow;
    public static BaseInteraction m_instance;

    public static BaseInteraction m_Instance {
        get { return m_instance; }
        set { m_instance = value;  }
    }

    private void Awake()
    {
        if (!PUBLIC.CreateInstance(ref m_instance, this, gameObject))
            return;

		m_ConditionsDictionary = new Dictionary<CHARACTERS, bool[]>();

		m_ConditionsDictionary[CHARACTERS.ATHENA] = new bool[3];
		m_ConditionsDictionary[CHARACTERS.HERMES] = new bool[5];
        m_ConditionsDictionary[CHARACTERS.MEDUSA] = new bool[3];

		m_CharactersDictionary = new Dictionary<CHARACTERS, UnityAction[]>();
        m_ActionsDictionary = new Dictionary<int, UnityAction>();
    }

    // Use this for initialization
    void Start()
	{
        m_CharactersDictionary[CHARACTERS.ATHENA] = new UnityAction[6];

        m_CharactersDictionary[CHARACTERS.ATHENA][0] = () => { ShowText(" Go show me ur ficking love for ur girl !"); };
    
		m_CharactersDictionary[CHARACTERS.ATHENA][1] = () => {
			ShowText(" I give us this fucking brygthy shield ");
            SetSecondConditionDone(CHARACTERS.ATHENA);
		};
		m_CharactersDictionary[CHARACTERS.ATHENA][2] = () => {
            m_Athena.ShowShield();
			ShowText(" Go speak to Hermes ! "); };
		m_CharactersDictionary[CHARACTERS.ATHENA][3] = () => { 
            ShowText(" You take the shield ");
			ArmManager.instance.HasShield();
			m_ConditionsDictionary[CHARACTERS.MEDUSA][0] = true;
		};

		m_CharactersDictionary[CHARACTERS.HERMES] = new UnityAction[6];
	
        m_CharactersDictionary[CHARACTERS.HERMES][0] = () => { ShowText(" Go find my shoes ? "); };
        m_CharactersDictionary[CHARACTERS.HERMES][1] = () => {
             ShowText(" Go speak to Hell ! "); 
 
            SetSecondConditionDone(CHARACTERS.HERMES);
        };
        m_CharactersDictionary[CHARACTERS.HERMES][2] = () => {m_Hermes.m_bHellIsShown = true;};
		m_CharactersDictionary[CHARACTERS.HERMES][3] = () => {
			ShowText("I Give u the sword "); 
            SetFourthConditionDone(CHARACTERS.HERMES);
		};

		m_CharactersDictionary[CHARACTERS.HERMES][4] = () => {
            m_Hermes.ShowSword();
			//m_ConditionsDictionary[CHARACTERS.ATHENA][2] = true;
			m_ConditionsDictionary[CHARACTERS.MEDUSA][1] = true;
		};

		m_CharactersDictionary[CHARACTERS.HERMES][5] = () => {
            ShowText("Vous avez pris l'épée");
            ArmManager.instance.HasSword();
		//	m_ConditionsDictionary[CHARACTERS.ATHENA][2] = true;
			m_ConditionsDictionary[CHARACTERS.MEDUSA][1] = true;
		};

		m_CharactersDictionary[CHARACTERS.MEDUSA] = new UnityAction[6];

        m_CharactersDictionary[CHARACTERS.MEDUSA][0] = () => { ShowText(" Medusa kills you ! "); };
		m_CharactersDictionary[CHARACTERS.MEDUSA][1] = () =>
        { 
            if ( m_ConditionsDictionary[CHARACTERS.MEDUSA][1]) // :: sword
            {
                ShowText("You try to cut her but she freezes you before you touch her "); 
            }
            if (m_ConditionsDictionary[CHARACTERS.MEDUSA][0]) // :: shield
            {
                ShowText("you freeze her but she comes back");
            }
		};
		m_CharactersDictionary[CHARACTERS.MEDUSA][2] = () => { ShowText(" you killed Medusa ! "); };
	}

    void ShowText(string l_smessage)
    {
        m_TextShow.text = l_smessage;
    }

    int GetCountOfCondition (CHARACTERS l_echaracter )
    {
        int returnValue = 0;
        bool[] temp = m_ConditionsDictionary[l_echaracter];

        for (int i = 0; i < m_ConditionsDictionary[l_echaracter].Length; i ++ )
        {
            if (!m_ConditionsDictionary[l_echaracter][i])
                return returnValue;
            returnValue += 1;
           
        }
        Debug.Log("count condiont " + l_echaracter + returnValue);
        return returnValue;
    }

    public void SetFirstConditionDone( CHARACTERS l_echaracter ) // :: at the end of the mini game
    {
        m_ConditionsDictionary[l_echaracter][0] = true; 

    }

	public void SetSecondConditionDone(CHARACTERS l_echaracter) // :: at the end of the mini game
	{
		m_ConditionsDictionary[l_echaracter][1] = true;

    }

	public void SetThirdConditionDone(CHARACTERS l_echaracter) // :: at the end of the mini game
	{
		m_ConditionsDictionary[l_echaracter][2] = true;

	}

	public void SetFourthConditionDone(CHARACTERS l_echaracter) // :: at the end of the mini game
	{
		m_ConditionsDictionary[l_echaracter][3] = true;

	}

    public void SetFifthConditionDone(CHARACTERS l_echaracter) // :: at the end of the mini game
	{
		m_ConditionsDictionary[l_echaracter][4] = true;

	}
    public void Interact ( CHARACTERS l_echaracter )
    {
        int l_iCountIndex = GetCountOfCondition(l_echaracter);
        m_CharactersDictionary[l_echaracter][l_iCountIndex]();
    }


    public void OnMiniGame(GameObject l_parent )
    {
		Vuforia.VuforiaBehaviour.Instance.enabled = false;
        l_parent.SetActive(true);
	}

    public void StopMiniGame( GameObject l_parent)
    {
		Vuforia.VuforiaBehaviour.Instance.enabled = true;
        l_parent.SetActive(false);
	}
    public void Test()
    {
    }
	// Update is called once per frame
	void Update()
	{
			
	}
}
