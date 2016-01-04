using UnityEngine;
using System.Collections;

public class TentacleManager : MonoBehaviour {
	
	public int m_iPosition;

	public Sprite[] m_asTentacleSprites;

	public CthulhuManager m_CthulhuManager;
	private Transform Gauge;

	static private float c_fDeltaTimePerFrameStandard = 0.3f;
	static private float c_fSpeed = 20.0f;
	private float m_fRand;
	private float m_fCountDown;

	private bool m_bTentacleActivate = false;
	private bool m_bTentacleTouch = false;

	private KeyCode m_KeyPosition;
	static private float c_fWidth;
	
//##################################################################################
// Public functions
//##################################################################################
	public void SetTentaclePosition(int a_iPosition) { m_iPosition = a_iPosition; }

// ----------------------------------------------------------------------------
// ----------------------------------------------------------------------------
	public void SetGaugeTransform(Transform a_Transform) { Gauge = a_Transform;}

// ----------------------------------------------------------------------------
// ----------------------------------------------------------------------------
	public void IncrementSpacePositionX () {
		transform.position += new Vector3 (c_fWidth, 0.0f, 0.0f);
	}

// ----------------------------------------------------------------------------
// ----------------------------------------------------------------------------
	public void DecrementSpacePositionX () {
		transform.position -= new Vector3 (c_fWidth, 0.0f, 0.0f);
	}
	
// ----------------------------------------------------------------------------
// ----------------------------------------------------------------------------
	public bool IsTentacleActivate() { return m_bTentacleActivate; }

//##################################################################################
// General functions
//##################################################################################
	private void Start () 
	{
		GetComponent<SpriteRenderer>().sprite = m_asTentacleSprites[0];

        m_fRand = Random.value * 0.1f;
        m_fCountDown = c_fDeltaTimePerFrameStandard - m_fRand;
		c_fWidth = m_asTentacleSprites[0].rect.width / 100.0f;

		if (m_iPosition < 5)
		{
			transform.position = new Vector3((float)(m_iPosition - 1.5f - 4.0f)*c_fWidth, -5.0f, 0.0f);
		}
		else if (m_iPosition > 4)
		{
			transform.position = new Vector3((float)(m_iPosition + 0.5f - 4.0f)*c_fWidth, -5.0f, 0.0f);
		}

		switch (m_iPosition) {
		case 1:
            m_KeyPosition = KeyCode.Alpha1;
			break;
		case 2:
            m_KeyPosition = KeyCode.Alpha2;
			break;
		case 3:
            m_KeyPosition = KeyCode.Alpha3;
			break;
		case 4:
            m_KeyPosition = KeyCode.Alpha4;
			break;
		case 5:
            m_KeyPosition = KeyCode.Alpha5;
			break;
		case 6:
            m_KeyPosition = KeyCode.Alpha6;
			break;
		case 7:
            m_KeyPosition = KeyCode.Alpha7;
			break;
		case 8:
            m_KeyPosition = KeyCode.Alpha8;
			break;
		}
	}
	
// ----------------------------------------------------------------------------
// ----------------------------------------------------------------------------
	private void Update () 
	{
		if (PauseMenuManager.Instance.isGamePaused () == false) 
		{
			AnimateTentacle ();
			ActivateTentacle ();
		}
	}
	
// ----------------------------------------------------------------------------
// ----------------------------------------------------------------------------
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Enemy") 
		{
            m_bTentacleTouch = true;
			Destroy(other.gameObject);
			if (other.gameObject.GetComponent<EnemyManager>() != null)
			{
				GaugeManager.Instance.IncreaseTerror(other.gameObject.GetComponent<EnemyManager>().GetTerrorCost());
			}
		}
	}

//##################################################################################
// Private functions
//##################################################################################
	private void AnimateTentacle()
	{
        m_fCountDown -= Time.deltaTime;
		if(m_fCountDown < 0.0f)
		{
			if(GetComponent<SpriteRenderer>().sprite == m_asTentacleSprites[0])
			{
				GetComponent<SpriteRenderer>().sprite = m_asTentacleSprites[1];
			}
			else
			{
				GetComponent<SpriteRenderer>().sprite = m_asTentacleSprites[0];
			}
            m_fCountDown = c_fDeltaTimePerFrameStandard - m_fRand;
		}
	}

// ----------------------------------------------------------------------------
// ----------------------------------------------------------------------------
	private void ActivateTentacle()
	{
		if (m_bTentacleActivate == false && Input.GetKey(m_KeyPosition))
		{
            m_bTentacleActivate = true;
            m_bTentacleTouch = false;
		}
		
		if (m_bTentacleActivate) 
		{
			if (m_bTentacleTouch == false)
			{
				if (transform.position.y < 0.0f)
					transform.position += new Vector3(0.0f, c_fSpeed * Time.deltaTime, 0.0f);
				else
                    m_bTentacleTouch = true;
			}
			else
			{
				if (transform.position.y > -5.0f)
					transform.position -= new Vector3(0.0f, c_fSpeed * Time.deltaTime, 0.0f);
				else
                    m_bTentacleActivate = false;
			}
		}
	}
}