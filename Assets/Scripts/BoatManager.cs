using UnityEngine;
using System.Collections;

public class BoatManager : MonoBehaviour {

	public int m_iCapacityMaxStandard = 5;
	public int m_iCapacityDeviation = 1;
	public Transform m_tHumanPrefab;
	public Transform m_tSoldierPrefab;
	public Transform m_tRobotPrefab;

	static private float c_fSpeed = 2.0f;
	static private float c_fDivingRateStandard = 0.6f;
	static private float c_fDivingRateDeviation = 0.1f;
	private Vector3 c_v3RightLimit = new Vector3(LevelManager.GetPanelWidth (), 4.0f, 0.0f);
	private Vector3 c_v3LeftLimit = new Vector3(-LevelManager.GetPanelWidth (), 4.0f, 0.0f);

	private Vector3 m_v3PositionToFix = new Vector3 (0.0f, 4.0f, 0.0f);

	private float m_fDivingCooldown;
	private int m_iCurrentCapacity;
	private bool m_bCameFromRight;
	private bool m_bIsFix;

	private float m_fTotalFrequency;
	private float m_fHumanFrequency = 1.0f;
	private float m_fSoldierFrequency = 0.0f;
	private float m_fRobotFrequency = 0.0f;
	
//##################################################################################
// Public functions
//##################################################################################
	public void SetPositionToFix(int a_fNewVal) { m_v3PositionToFix.x = a_fNewVal; }

// ----------------------------------------------------------------------------
// ----------------------------------------------------------------------------
	public void SetHumanFrequency(float a_fVal)	{ m_fHumanFrequency = a_fVal; }

// ----------------------------------------------------------------------------
// ----------------------------------------------------------------------------
	public void SetSoldierFrequency(float a_fVal) { m_fSoldierFrequency = a_fVal; }

// ----------------------------------------------------------------------------
// ----------------------------------------------------------------------------
	public void SetRobotFrequency(float a_fVal)	{ m_fRobotFrequency = a_fVal; }

//##################################################################################
// General functions
//##################################################################################
	void Start () {
		m_bCameFromRight = (Random.value < 0.5f) ? true : false;

        m_bIsFix = false;

        m_fDivingCooldown = 0.0f;
		float fTemp = Random.value;
		int iCurrentCapacityDeviation;
		if (fTemp < 0.33f)
			iCurrentCapacityDeviation = -1;
		else if (fTemp < 0.66f)
			iCurrentCapacityDeviation = 0;
		else
			iCurrentCapacityDeviation = 1;

        m_iCurrentCapacity = m_iCapacityMaxStandard + iCurrentCapacityDeviation;

		if (m_bCameFromRight) {
			transform.position = c_v3RightLimit;
		} else {
			transform.position = c_v3LeftLimit;
		}

        m_fTotalFrequency = m_fHumanFrequency + m_fSoldierFrequency + m_fRobotFrequency;
	}
	
// ----------------------------------------------------------------------------
// ----------------------------------------------------------------------------
	void Update () 
	{
		if (PauseMenuManager.Instance.isGamePaused () == false) 
		{
			MoveToFix ();
			Diving ();
		}
	}
	
//##################################################################################
// Private functions
//##################################################################################
	void MoveToFix()
	{
		if (m_bIsFix == false) {
			if (m_bCameFromRight) {
				transform.position -= new Vector3 (c_fSpeed * Time.deltaTime, 0.0f, 0.0f);
				if (transform.position.x < m_v3PositionToFix.x) {
                    m_bIsFix = true;
				}
			} else {
				transform.position += new Vector3 (c_fSpeed * Time.deltaTime, 0.0f, 0.0f);
				if (transform.position.x > m_v3PositionToFix.x) {
                    m_bIsFix = true;
				}
			}	
		} else {
			if (m_iCurrentCapacity == 0)
			{
				if (m_bCameFromRight) {
					transform.position -= new Vector3 (c_fSpeed * Time.deltaTime, 0.0f, 0.0f);
					if (transform.position.x < c_v3LeftLimit.x) {
						Destroy(gameObject);
					}
				} else {
					transform.position += new Vector3 (c_fSpeed * Time.deltaTime, 0.0f, 0.0f);
					if (transform.position.x > c_v3RightLimit.x) {
						Destroy(gameObject);
					}
				}	
			}
		}
	}
	
// ----------------------------------------------------------------------------
// ----------------------------------------------------------------------------
	void Diving()
	{
		if (m_bIsFix && m_iCurrentCapacity > 0) 
		{
            m_fDivingCooldown -= Time.deltaTime;
			if (m_fDivingCooldown <= 0.0f)
			{
				Transform EnemyTransform;
				float fTemp = Random.value * m_fTotalFrequency;
				if (fTemp < m_fHumanFrequency)
					EnemyTransform = Instantiate(m_tHumanPrefab) as Transform;
				else if(fTemp < m_fHumanFrequency + m_fSoldierFrequency)
					EnemyTransform = Instantiate(m_tSoldierPrefab) as Transform;
				else
					EnemyTransform = Instantiate(m_tRobotPrefab) as Transform;
				EnemyTransform.position = transform.position;
                m_iCurrentCapacity--;

				float fCurrentRateDeviation = c_fDivingRateStandard;
				fTemp = Random.value;
				if (fTemp < 0.33f)
					fCurrentRateDeviation = -c_fDivingRateDeviation;
				else if (fTemp < 0.66f)
					fCurrentRateDeviation = 0.0f;
				else
					fCurrentRateDeviation = c_fDivingRateDeviation;
                m_fDivingCooldown = c_fDivingRateStandard + fCurrentRateDeviation;
			}
		}
	}

}
