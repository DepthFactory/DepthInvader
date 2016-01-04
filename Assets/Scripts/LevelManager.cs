using UnityEngine;
using DesignPattern;
using System.Collections;

public class LevelManager : Singleton<LevelManager> {

	public int m_iBoatNumber = 3;
	public Transform m_tBoatPrefab;

	private float m_fDurationBetweenBoatsStandard = 10.0f;
	private float m_fDerivationDurationBetweenBoats = 3.0f;

	private float m_fHumanFrequency = 1.0f;
	private float m_fSoldierFrequency = 0.2f;
	private float m_fRobotFrequency = 0.0f;

	private int m_iPairOfExtraTentacle = 0;

	private float m_fCountdown;
	static private int m_iPanelWidth;
	private int m_iCurrentBoatNumber;
	
//##################################################################################
// Public functions
//##################################################################################
	static public int GetPanelWidth() { return m_iPanelWidth; }
	
	// ----------------------------------------------------------------------------
	// ----------------------------------------------------------------------------
	public void SetBoatNumber(int a_iVal) { m_iBoatNumber = a_iVal; m_iCurrentBoatNumber = m_iBoatNumber; }
	
	// ----------------------------------------------------------------------------
	// ----------------------------------------------------------------------------
	public void SetDurationBetweenBoatsStandard(float a_fVal) { m_fDurationBetweenBoatsStandard = a_fVal; }
	
	// ----------------------------------------------------------------------------
	// ----------------------------------------------------------------------------
	public void SetDerivationDurationBetweenBoats(float a_fVal) { m_fDerivationDurationBetweenBoats = a_fVal; }
	
	// ----------------------------------------------------------------------------
	// ----------------------------------------------------------------------------
	public void SetHumanFrequency(float a_fVal) { m_fHumanFrequency = a_fVal; }
	
	// ----------------------------------------------------------------------------
	// ----------------------------------------------------------------------------
	public void SetSoldierFrequency(float a_fVal) { m_fSoldierFrequency = a_fVal; }
	
	// ----------------------------------------------------------------------------
	// ----------------------------------------------------------------------------
	public void SetRobotFrequency(float a_fVal) { m_fRobotFrequency = a_fVal; }
	
	// ----------------------------------------------------------------------------
	// ----------------------------------------------------------------------------
	public void SetPairOfExtraTentacle(int a_iVal) { m_iPairOfExtraTentacle = a_iVal; }

//##################################################################################
// General functions
//##################################################################################
	private void Start () {
        m_fCountdown = m_fDurationBetweenBoatsStandard;
        m_iCurrentBoatNumber = m_iBoatNumber;

		CthulhuManager.Instance.transform.position = new Vector3(0.0f, -5.0f, 0.0f);
		//CthulhuManager.Instance.SetPairOfExtraTentacle (iPairOfExtraTentacle);


		int PairOfExtraTentacle = CthulhuManager.Instance.GetPairOfExtraTentacle ();
        m_iPanelWidth = PairOfExtraTentacle * 2 + 2 + 2;
	}
	
// ----------------------------------------------------------------------------
// ----------------------------------------------------------------------------
	private void Update () 
	{
		if (PauseMenuManager.Instance.isGamePaused () == false) 
		{
            m_fCountdown -= Time.deltaTime;
			if (m_fCountdown <= 0) {
				Transform BoatTransform = Instantiate (m_tBoatPrefab) as Transform;
				int PosToFixX = (int)Mathf.Floor ((Random.value - 0.5f) * m_iPanelWidth - 1.0f);
				BoatTransform.gameObject.GetComponent<BoatManager> ().SetPositionToFix (PosToFixX); 
				BoatTransform.gameObject.GetComponent<BoatManager> ().SetHumanFrequency (m_fHumanFrequency);
				BoatTransform.gameObject.GetComponent<BoatManager> ().SetSoldierFrequency (m_fSoldierFrequency);
				BoatTransform.gameObject.GetComponent<BoatManager> ().SetRobotFrequency (m_fRobotFrequency);

                m_fCountdown = m_fDurationBetweenBoatsStandard + (Random.value - 0.5f) * m_fDerivationDurationBetweenBoats; 
			}
		}
	}
}