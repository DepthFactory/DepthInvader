using UnityEngine;
using DesignPattern;
using System.Collections;

public class GameManager : Singleton<GameManager> {

	public int m_iLevelNumber = 5;
	private int m_iCurrentLevel;
	private bool m_bIsLevelFinished;
	
	public float m_fDurationBetweenBoatsStandard = 10.0f;
	public float m_fDerivationDurationBetweenBoats = 3.0f;
	
	public float m_fHumanFrequency = 1.0f;
	public float m_fSoldierFrequency = 0.2f;
	public float m_fRobotFrequency = 0.0f;
	
	public Transform m_tCaches;
	public Transform m_tGauge;
	public Transform m_tGaugeCanvas;
	private Transform m_tGaugeTransform;

	public int m_iPairOfExtraTentacle;

//##################################################################################
// Public functions
//##################################################################################
	
//##################################################################################
// General functions
//##################################################################################
	private void Awake()
	{
        m_tGaugeTransform = Instantiate(m_tGauge) as Transform;

		CthulhuManager.Instance.SetGaugeTransform (m_tGaugeTransform);
		CthulhuManager.Instance.SetPairOfExtraTentacle (m_iPairOfExtraTentacle);
	}

// ----------------------------------------------------------------------------
// ----------------------------------------------------------------------------
    private void Start () 
	{
        m_iCurrentLevel = 1;
        m_iPairOfExtraTentacle = 0;
        m_bIsLevelFinished = false;
		Debug.Log ("Level 1 !  " + m_iLevelNumber);

		InstantiateNewLevel (m_fDurationBetweenBoatsStandard, m_fDerivationDurationBetweenBoats,
                             m_fHumanFrequency, m_fSoldierFrequency, m_fRobotFrequency, m_iPairOfExtraTentacle);

        m_tGaugeTransform.transform.parent = m_tGaugeCanvas.transform;
        m_tGaugeTransform.position = m_tGaugeCanvas.position;

		Transform CachesTransform = Instantiate(m_tCaches) as Transform;
	}
	
// ----------------------------------------------------------------------------
// ----------------------------------------------------------------------------
	private void Update () 
	{
		if(IsLevelFinished() && m_iCurrentLevel < m_iLevelNumber)
		{
			Debug.Log("Piou");
			LaunchNewLevel ();	
		}
	}
	
//##################################################################################
// Private functions
//##################################################################################
	private void InstantiateNewLevel(float a_fDurationBetweenBoatsStandard, float a_fDerivationDurationBetweenBoats,
	                                 float a_fHumanFrequency, float a_fSoldierFrequency, float a_fRobotFrequency,
	                                 int a_iPairOfExtraTentacle)
	{
		LevelManager.Instance.SetDurationBetweenBoatsStandard (a_fDurationBetweenBoatsStandard);
		LevelManager.Instance.SetDerivationDurationBetweenBoats (a_fDerivationDurationBetweenBoats);
		LevelManager.Instance.SetHumanFrequency (a_fHumanFrequency);
		LevelManager.Instance.SetSoldierFrequency (a_fSoldierFrequency);
		LevelManager.Instance.SetRobotFrequency (a_fRobotFrequency);
		LevelManager.Instance.SetPairOfExtraTentacle (a_iPairOfExtraTentacle);
	}

// ----------------------------------------------------------------------------
// ----------------------------------------------------------------------------
	private bool IsLevelFinished()
	{
		//Debug.Log (iCurrentLevel);
		return GaugeManager.Instance.IsStagePassed (m_iCurrentLevel);
	}

// ----------------------------------------------------------------------------
// ----------------------------------------------------------------------------
	private void LaunchNewLevel()
	{
        m_iCurrentLevel++;
		Debug.Log (m_iCurrentLevel + " // "+ m_iLevelNumber);
		if (m_iCurrentLevel <= m_iLevelNumber)
		{
			Debug.Log ("Level "+ m_iCurrentLevel + " !");
			Debug.Log(GaugeManager.Instance.GetCurrentStage());
			switch (GaugeManager.Instance.GetCurrentStage())
			{
			case 1:
				Debug.Log("Case 1");
                m_fHumanFrequency = 0.6f;
                m_fSoldierFrequency = 0.4f;
                m_fRobotFrequency = 0.0f;
				break;
			case 2:
				Debug.Log("Case 2");
                m_fHumanFrequency = 0.3f;
                m_fSoldierFrequency = 0.6f;
                m_fRobotFrequency = 0.1f;
				break;
			case 3:
				Debug.Log("Case 3");
                m_fHumanFrequency = 0.2f;
                m_fSoldierFrequency = 0.6f;
                m_fRobotFrequency = 0.2f;
				break;
			default:	
				Debug.Log("Default");
				break;
			}
            LevelDisplayManager.Instance.IncreaseLevel();
            m_iPairOfExtraTentacle++;
			InstantiateNewLevel (m_fDurationBetweenBoatsStandard, m_fDerivationDurationBetweenBoats,
                                 m_fHumanFrequency, m_fSoldierFrequency, m_fRobotFrequency, m_iPairOfExtraTentacle);
		}
	}
}
