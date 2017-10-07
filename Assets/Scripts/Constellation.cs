using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Constellation : MonoBehaviour
{
    enum STATETRACE
    {
        ISTRACING,
        NOTHING,
        STARTING
    }


    public LineRenderer m_currentLine;

    Canvas
        m_Child;

    public List<LineRenderer>
        m_currentLineList;

    public int m_iCountConstellation = 10;
    int m_iCurrentCount = 0;
    int m_iCurrentCountList = 0;

    string[] m_ListStar;
    bool m_isFound = false;

    STATETRACE m_eTraceState = STATETRACE.NOTHING;


	public static Constellation m_instance;

	public static Constellation m_Instance
	{
		get { return m_instance; }
		set { m_instance = value; }
	}

    public void StartMiniGame()
    {
        m_Child.enabled = true;
        Vuforia.VuforiaBehaviour.Instance.enabled = false;
    }

    public void CloseMiniGame()
    {
        Reset();
        m_Child.enabled = false;
		Vuforia.VuforiaBehaviour.Instance.enabled = true;
        // :: Show bras 
    }

    public void CheckIfRightPattern()
    {
        bool first = false;
        bool second = false;
        for (int i = 0; i < 5; i ++ )
        {
            if (m_ListStar[i] != null && (!first || !second))
            {
				if (m_ListStar[i] == "0123" || m_ListStar[i] == "3210") first = true;
				else if (m_ListStar[i] == "542" || m_ListStar[i] == "245") second = true;
            }
        }

        if (first && second)
        {
			BaseInteraction.m_Instance.SetFirstConditionDone(CHARACTERS.ATHENA);
			m_isFound = true;
        }
      
    }

    public void Reset()
    {
        m_iCurrentCountList = 0;
        foreach (LineRenderer line in m_currentLineList){
            for (int i = 0; i < line.positionCount; i++)
                line.positionCount = 0;
        }
        m_iCurrentCount = 0;
        m_ListStar = new string[5];
    }
    private void Awake()
    {
        if (!PUBLIC.CreateInstance(ref m_instance, this, gameObject))
            return;
        m_Child = GetComponentInChildren<Canvas>();
    }
    // Use this for initialization
    void Start()
    {
        m_ListStar = new string[5];
    }

    public void AddStar( int l_iindex , Vector2 l_vposition)
    {
        if (m_eTraceState != STATETRACE.ISTRACING)
        {
            CreateNewLine();
        }
       
            m_ListStar[m_iCurrentCountList] += l_iindex.ToString();
            m_currentLine.positionCount++;
            m_currentLine.SetPosition(m_iCurrentCount, new Vector3( l_vposition.x, l_vposition.y, m_Child.transform.position.z - 4));
            m_iCurrentCount++;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isFound)
            return;
        
        CheckIfRightPattern();
		switch( m_eTraceState )
        {
            case STATETRACE.ISTRACING : 
                {

#if !UNITY_EDITOR

        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
#else
					if (Input.GetMouseButtonUp(0))
					{
#endif
                        m_eTraceState = STATETRACE.NOTHING;
					}

			
                }
                break;
            case STATETRACE.NOTHING:
                {

#if !UNITY_EDITOR
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
                    CreateNewLine();
                    }
#else
					if (Input.GetMouseButtonDown(0))
					{
						m_eTraceState = STATETRACE.STARTING;
					}
#endif

				}
                break;
            case STATETRACE.STARTING : 
                {

                    if(Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") != 0)
                    {
						CreateNewLine();
					}
                }break;
        }
      
	}

    void CreateNewLine ()
    {
		m_eTraceState = STATETRACE.ISTRACING;

        if (m_iCurrentCountList == m_currentLineList.Count)
            Reset();


		m_currentLine = m_currentLineList[m_iCurrentCountList].GetComponent<LineRenderer>();
		m_iCurrentCountList++;
		m_iCurrentCount = 0;
         
    }
}
