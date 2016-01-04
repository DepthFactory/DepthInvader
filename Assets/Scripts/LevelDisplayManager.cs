using UnityEngine;
using UnityEngine.UI;
using DesignPattern;
using System.Collections;

public class LevelDisplayManager : Singleton<LevelDisplayManager> {
    
    private enum LevelEnum { Level_1, Level_2, Level_3, Level_4 };
    private LevelEnum m_LevelEnum;

    private string m_sText;

//##################################################################################
// Public functions
//##################################################################################
    public void IncreaseLevel()
    {
        switch (m_LevelEnum)
        {
            case LevelEnum.Level_1:
                m_LevelEnum = LevelEnum.Level_2;
                break;
            case LevelEnum.Level_2:
                m_LevelEnum = LevelEnum.Level_3;
                break;
            case LevelEnum.Level_3:
                m_LevelEnum = LevelEnum.Level_4;
                break;
            default:
                Debug.Log("Game is finish or error.");
                break;
        }
    }

//##################################################################################
// General functions
//##################################################################################
    private void Start ()
    {
        m_LevelEnum = LevelEnum.Level_1;
        m_sText = "";

        SetDisplayText();
    }

// ----------------------------------------------------------------------------
// ----------------------------------------------------------------------------
    private void Update ()
    {
        SetDisplayText();
        GetComponent<Text>().text = m_sText;
	}

//##################################################################################
// Private functions
//##################################################################################
    private void SetDisplayText()
    {
        switch (m_LevelEnum)
        {
            case LevelEnum.Level_1:
                m_sText = "Level 1";
                break;
            case LevelEnum.Level_2:
                m_sText = "Level 2";
                break;
            case LevelEnum.Level_3:
                m_sText = "Level 3";
                break;
            case LevelEnum.Level_4:
                m_sText = "Level 4";
                break;
        }
    }
}
