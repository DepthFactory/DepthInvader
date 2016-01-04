using UnityEngine;
using DesignPattern;
using System.Collections;

public class CthulhuManager : Singleton<CthulhuManager> {

	public int m_iPairOfExtraTentacle = 0;
	public Transform m_tTentacle;
	public Transform m_tTentacleMother;
	
	static private int m_iPairOfExtraTentacleMax = 3;
	private int m_iCurrentPairOfExtraTentacle;
	
	private Transform m_tTentacleMotherTransform_1;
	private Transform m_tTentacleMotherTransform_2;
	private Transform[] m_tTentacleTransform;

	private Transform m_tGauge;

	private int m_iPosition;
	static private int m_iPositionMin = -1;
	static private int m_iPositionMax = 1;
	private bool m_bCanMove;
	
//##################################################################################
// Public functions
//##################################################################################
	public int GetPairOfExtraTentacle() { return m_iPairOfExtraTentacle; }

// ----------------------------------------------------------------------------
// ----------------------------------------------------------------------------
	public void SetPairOfExtraTentacle(int a_iVal) { m_iPairOfExtraTentacle = a_iVal; }
	
// ----------------------------------------------------------------------------
// ----------------------------------------------------------------------------
	public void SetGaugeTransform(Transform a_Transform) { m_tGauge = a_Transform; }

// ----------------------------------------------------------------------------
// ----------------------------------------------------------------------------
    public void SetInitialPosition(Vector3 a_v3Val) { gameObject.transform.position = a_v3Val;}

//##################################################################################
// General functions
//##################################################################################
	private void Start ()
	{
        m_tTentacleMotherTransform_1 = Instantiate(m_tTentacleMother) as Transform;
        m_tTentacleMotherTransform_1.gameObject.GetComponent<TentacleManager>().SetTentaclePosition(4);
        m_tTentacleMotherTransform_1.gameObject.GetComponent<TentacleManager> ().SetGaugeTransform (m_tGauge);
        m_tTentacleMotherTransform_2 = Instantiate(m_tTentacleMother) as Transform;
        m_tTentacleMotherTransform_2.gameObject.GetComponent<TentacleManager>().SetTentaclePosition(5);
        m_tTentacleMotherTransform_2.gameObject.GetComponent<TentacleManager> ().SetGaugeTransform (m_tGauge);

		InitializeTentacles ();

        m_iPosition = 0;
        m_bCanMove = true;
	}
	
// ----------------------------------------------------------------------------
// ----------------------------------------------------------------------------
	private void Update () 
	{
		if (PauseMenuManager.Instance.isGamePaused () == false) 
		{
			//Debug.Log(iCurrentPairOfExtraTentacle +" // "+ iPairOfExtraTentacle);
			if (m_iCurrentPairOfExtraTentacle != m_iPairOfExtraTentacle)
			{
				InitializeTentacles();
			}

			Move ();
		}
	}
	
//##################################################################################
// Private functions
//##################################################################################
	private void Move()
	{
        m_bCanMove = true;
		if (m_tTentacleMotherTransform_1.gameObject.GetComponent<TentacleManager> ().IsTentacleActivate ())
            m_bCanMove = false;
		if (m_tTentacleMotherTransform_2.gameObject.GetComponent<TentacleManager> ().IsTentacleActivate ())
            m_bCanMove = false;
		for (int i = 0; i < m_iPairOfExtraTentacleMax * 2; i++) {
			if (m_tTentacleTransform[i] != null) {
				GameObject TentacleGO = m_tTentacleTransform[i].gameObject;
				if (TentacleGO.GetComponent<TentacleManager> ().IsTentacleActivate ())
                    m_bCanMove = false;
			}
		}

		if (m_bCanMove) 
		{
			if (Input.GetKeyDown (KeyCode.RightArrow) && m_iPosition < m_iPositionMax) {
				transform.position += new Vector3 (1.0f, 0.0f, 0.0f);
                m_tTentacleMotherTransform_1.gameObject.GetComponent<TentacleManager> ().IncrementSpacePositionX ();
                m_tTentacleMotherTransform_2.gameObject.GetComponent<TentacleManager> ().IncrementSpacePositionX ();
				for (int i = 0; i < m_iPairOfExtraTentacleMax * 2; i++) {
					if (m_tTentacleTransform[i] != null) {
						GameObject TentacleGO = m_tTentacleTransform[i].gameObject;
						TentacleGO.GetComponent<TentacleManager> ().IncrementSpacePositionX ();
					}
				}
                m_iPosition++;
			} else if (Input.GetKeyDown (KeyCode.LeftArrow) && m_iPosition > m_iPositionMin) {
				transform.position -= new Vector3 (1.0f, 0.0f, 0.0f);
                m_tTentacleMotherTransform_1.gameObject.GetComponent<TentacleManager> ().DecrementSpacePositionX ();
                m_tTentacleMotherTransform_2.gameObject.GetComponent<TentacleManager> ().DecrementSpacePositionX ();
				for (int i = 0; i < m_iPairOfExtraTentacleMax * 2; i++) {
					if (m_tTentacleTransform[i] != null) {
						GameObject TentacleGO = m_tTentacleTransform[i].gameObject;
						TentacleGO.GetComponent<TentacleManager> ().DecrementSpacePositionX ();
					}
				}
                m_iPosition--;
			}
		}
	}
	
// ----------------------------------------------------------------------------
// ----------------------------------------------------------------------------
	private void InitializeTentacles()
	{
		if (m_iPairOfExtraTentacle > m_iPairOfExtraTentacleMax)
		{
            m_iPairOfExtraTentacle = m_iPairOfExtraTentacleMax;
			Debug.LogWarning("Number max of extra tentacle's pair is " + m_iPairOfExtraTentacleMax + ".");
		}

        m_iCurrentPairOfExtraTentacle = m_iPairOfExtraTentacle;

        m_tTentacleTransform = new Transform[m_iPairOfExtraTentacleMax * 2];
		
		if (m_iPairOfExtraTentacle > 0) 
		{
            m_tTentacleTransform[0] = Instantiate(m_tTentacle) as Transform;
			m_tTentacleTransform[0].gameObject.GetComponent<TentacleManager>().SetTentaclePosition(3);
			m_tTentacleTransform[0].gameObject.GetComponent<TentacleManager> ().SetGaugeTransform (m_tGauge);
			m_tTentacleTransform[1] = Instantiate(m_tTentacle) as Transform;
			m_tTentacleTransform[1].gameObject.GetComponent<TentacleManager>().SetTentaclePosition(6);
			m_tTentacleTransform[1].gameObject.GetComponent<TentacleManager> ().SetGaugeTransform (m_tGauge);
			if (m_iPairOfExtraTentacle > 1)
			{
				m_tTentacleTransform[2] = Instantiate(m_tTentacle) as Transform;
				m_tTentacleTransform[2].gameObject.GetComponent<TentacleManager>().SetTentaclePosition(2);
				m_tTentacleTransform[2].gameObject.GetComponent<TentacleManager> ().SetGaugeTransform (m_tGauge);
				m_tTentacleTransform[3] = Instantiate(m_tTentacle) as Transform;
				m_tTentacleTransform[3].gameObject.GetComponent<TentacleManager>().SetTentaclePosition(7);
				m_tTentacleTransform[3].gameObject.GetComponent<TentacleManager> ().SetGaugeTransform (m_tGauge);
				if ( m_iPairOfExtraTentacle > 2)
				{
					m_tTentacleTransform[4] = Instantiate(m_tTentacle) as Transform;
					m_tTentacleTransform[4].gameObject.GetComponent<TentacleManager>().SetTentaclePosition(1);
					m_tTentacleTransform[4].gameObject.GetComponent<TentacleManager> ().SetGaugeTransform (m_tGauge);
					m_tTentacleTransform[5] = Instantiate(m_tTentacle) as Transform;
					m_tTentacleTransform[5].gameObject.GetComponent<TentacleManager>().SetTentaclePosition(8);
					m_tTentacleTransform[5].gameObject.GetComponent<TentacleManager> ().SetGaugeTransform (m_tGauge);
				}
			}
		}
	}
}
