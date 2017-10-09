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
    public Canvas m_CanvasText;
    protected Dictionary<int, UnityAction> m_ActionsDictionary;
    public GameObject m_arm;
	public Hermes m_Hermes;
    public Medusa m_Medusa;
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

        m_CharactersDictionary[CHARACTERS.ATHENA][0] = () => { ShowText(" Persée, tes paroles ne valent rien pour moi ! Prouve moi ton amour pour Andromède par des actes et je te ferai don de ma protection. \nIndice: Demande de l’aide aux étoiles"); };
    
		m_CharactersDictionary[CHARACTERS.ATHENA][1] = () => {
			ShowText(" Il est rare de voir un telle dévotion pour sa bien-aimée, il ne fait aucuns doutes de tes sentiments. Je n’ai qu’une parole, voici donc mon bouclier, qu’il t’aide à surmonter tes défis.");
            SetSecondConditionDone(CHARACTERS.ATHENA);
		};
		m_CharactersDictionary[CHARACTERS.ATHENA][2] = () => {
            m_Athena.ShowShield();
			 };
		m_CharactersDictionary[CHARACTERS.ATHENA][3] = () => { 
            ShowText(" Vous avez pris le bouclier ");
			ArmManager.instance.HasShield();
            SetSecondConditionDone(CHARACTERS.MEDUSA);
		};

		m_CharactersDictionary[CHARACTERS.HERMES] = new UnityAction[6];
	
        m_CharactersDictionary[CHARACTERS.HERMES][0] = () => { ShowText(" Héros si tu veux de l’aide dans ta quête tu devras m’aider en retour. Mais avant ça il faut que tu trouves quelque chose qui m’appartient. \nIndice : Une fois chaussé tu seras plus rapide qu’un dieu.  "); };
        m_CharactersDictionary[CHARACTERS.HERMES][1] = () => {
             ShowText(" Maintenant te voilà presque à ma hauteur, Héro. Ces sandales m’ont permit de livrer les messages des dieux. D’ailleurs voyons si tu en es digne: deviens Guide des âmes de l’enfer comme moi et je réfléchirai peut-être à t’apporter mon aide.  "); 
 
            SetSecondConditionDone(CHARACTERS.HERMES);
        };
        m_CharactersDictionary[CHARACTERS.HERMES][2] = () => {m_Hermes.m_bHellIsShown = true;};
		m_CharactersDictionary[CHARACTERS.HERMES][3] = () => {
			ShowText("Ne pense pas être mon égal Persée, mais je dois avouer que tu m’as surpris … voici ma serpe qui t’aidera à vaincre tes ennemis."); 
            SetFourthConditionDone(CHARACTERS.HERMES);
		};

		m_CharactersDictionary[CHARACTERS.HERMES][4] = () => {
            m_Hermes.ShowSword();
		};

		m_CharactersDictionary[CHARACTERS.HERMES][5] = () => {
            ShowText("Vous avez pris l'épée");
            ArmManager.instance.HasSword();
            SetFirstConditionDone(CHARACTERS.MEDUSA);
		};

		m_CharactersDictionary[CHARACTERS.MEDUSA] = new UnityAction[6];

        m_CharactersDictionary[CHARACTERS.MEDUSA][0] = () => {
            if (m_ConditionsDictionary[CHARACTERS.MEDUSA][1] )
            {
				ShowText("Te cacher ne me tuera pas !"); m_Medusa.Cast(); m_arm.GetComponent<ArmManager>().Death(); 
            }
            else 
                ShowText("Comment oses-tu  te présenter devant moi cupide héros !"); m_arm.GetComponent<ArmManager>().Death(); m_Medusa.Cast(); 
        };
		m_CharactersDictionary[CHARACTERS.MEDUSA][1] = () =>
		{
                ShowText("Crois-tu vraiment que cette serpe suffira ? ");  m_arm.GetComponent<ArmManager>().Death(); m_Medusa.Cast();
			
		};
		//      m_CharactersDictionary[CHARACTERS.MEDUSA][1] = () =>
		//      { 
		//          if ( m_ConditionsDictionary[CHARACTERS.MEDUSA][1]) // :: sword
		//          {
		//              ShowText("You try to cut her but she freezes you before you touch her "); 
		//          }
		//          if (m_ConditionsDictionary[CHARACTERS.MEDUSA][0]) // :: shield
		//          {
		//              ShowText("you freeze her but she comes back");
		//          }
		//};
        m_CharactersDictionary[CHARACTERS.MEDUSA][2] = () => { ShowText("Vous avez VAINCU Méduse ! Felicitations ! Andromède vous remercie."); m_Medusa.Die(); m_arm.GetComponent<ArmManager>().Attack(); };
        ShowText("Bienvenue dans un nouveau chapitre !Aide Persée à devenir un héros en l'aidant à vaincre Médusa afin de libérer Andromède des ses griffes.");
			StartCoroutine(LateStart(1.5f));

    }

	IEnumerator LateStart(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
        m_CanvasText.enabled = true;
		//Your Function You Want to Call
	}
    private void LateStart()
    {
        
    }
    void ShowText(string l_smessage)
    {
        m_CanvasText.enabled = true;
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
		m_arm.SetActive(false);
	}

    public void StopMiniGame( GameObject l_parent)
    {
        m_arm.SetActive(true);
		Vuforia.VuforiaBehaviour.Instance.enabled = true;
        l_parent.SetActive(false);
	}

    public void ResetGame()
    {
		m_ConditionsDictionary[CHARACTERS.ATHENA] = new bool[3];
		m_ConditionsDictionary[CHARACTERS.HERMES] = new bool[5];
		m_ConditionsDictionary[CHARACTERS.MEDUSA] = new bool[3];

		m_CharactersDictionary = new Dictionary<CHARACTERS, UnityAction[]>();
		m_ActionsDictionary = new Dictionary<int, UnityAction>();
        Start();
        m_CanvasText.enabled = false;
    }

    public void Test()
    {
    }
	// Update is called once per frame
	void Update()
	{
			
	}
}
