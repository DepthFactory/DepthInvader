using UnityEngine;
using UnityEngine.UI;
using DesignPattern;
using System.Collections;

public class GaugeManager : Singleton<GaugeManager> {

	public float m_fMax = 20;
	private float m_fCurrentValue;
	private int m_iCost;
	private bool[] m_abStage;
	
//##################################################################################
// Public functions
//##################################################################################
	public bool IsStagePassed(int a_iVal)
	{
		//Debug.Log (a_iVal +" : "+ AbStage [a_iVal]);
		if (a_iVal < m_abStage.Length)
			return m_abStage[a_iVal];
		else
			return false;
	}
	
// ----------------------------------------------------------------------------
// ----------------------------------------------------------------------------
	public int GetCurrentStage()
	{
		//Debug.Log ("AbStage[1] = " + AbStage [1]);
		//Debug.Log ("AbStage[2] = " + AbStage [2]);
		for (int i = 0; i < m_abStage.Length; i++)
		{
			if (m_abStage[i] == false)
			{
				//Debug.Log("i-1 : "+ (i-1));
				return i -1;
			}
		}
		return 0;
	}

// ----------------------------------------------------------------------------
// ----------------------------------------------------------------------------
	public void IncreaseTerror (float a_iVal) 
	{
        m_fCurrentValue += a_iVal/ m_fMax;
	}
	
//##################################################################################
// General functions
//##################################################################################	
	private void Start () 
	{
		m_fCurrentValue = 0.0f;
        m_abStage = new bool[5];
		for (int i = 0; i < 5; i++)
		{
            m_abStage[i] = false;
		}
	}

// ----------------------------------------------------------------------------
// ----------------------------------------------------------------------------
	private void Update()
	{
		gameObject.GetComponent<Image> ().fillAmount = m_fCurrentValue;
		//Debug.Log (fCurrentValue);
		for (int i = 0; i < m_abStage.Length; i++)
		{
			if (m_fCurrentValue * (m_abStage.Length -1) >= i)
                m_abStage[i] = true;
		}
	}
}
