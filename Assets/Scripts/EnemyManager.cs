using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public enum EnemyType{Human, Soldier, Robot};

	public EnemyType m_Type;
	static private float c_fDivingHumanSpeed = 1.0f;
	static private float c_fDivingSoldierSpeed = 1.5f;
	static private float c_fDivingRobotSpeed = 2.0f;
	
	static private float c_fHumanTerrorCost = 2.0f;
	static private float c_fSoldierTerrorCost = 3.0f;
	static private float c_fRobotTerrorCost = 5.0f;
	
//##################################################################################
// Public functions
//##################################################################################
	public float GetTerrorCost()
	{
		float fTerrorCost = 0.0f;
		switch (m_Type)
		{
		case EnemyType.Human:
			fTerrorCost = c_fHumanTerrorCost;
			break;
		case EnemyType.Soldier:
			fTerrorCost = c_fSoldierTerrorCost;
			break;
		case EnemyType.Robot:
			fTerrorCost = c_fRobotTerrorCost;
			break;
		}
		return fTerrorCost;
	}

//##################################################################################
// General functions
//##################################################################################
	void Update () {
		if (PauseMenuManager.Instance.isGamePaused () == false) 
		{
			switch (m_Type) {
			case EnemyType.Human:
				transform.position -= new Vector3 (0.0f, c_fDivingHumanSpeed * Time.deltaTime, 0.0f);
				break;
			case EnemyType.Soldier:
				transform.position -= new Vector3 (0.0f, c_fDivingSoldierSpeed * Time.deltaTime, 0.0f);
				break;
			case EnemyType.Robot:
				transform.position -= new Vector3 (0.0f, c_fDivingRobotSpeed * Time.deltaTime, 0.0f);
				break;
			}
		}
	}
}
