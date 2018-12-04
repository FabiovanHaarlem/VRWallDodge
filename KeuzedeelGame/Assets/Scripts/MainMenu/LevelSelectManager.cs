using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    private List<LevelCollection> m_LevelCollections;

    private LevelCollection m_Active;
    private LevelCollection m_NextLevels;

    private bool m_MenuCanSwitch;

    public void SetLevelsActive(LevelCollection levels)
    {
        if (m_Active == null)
        {
            m_Active = levels;
            m_Active.MoveMenuUp();
        }
        else
        {
            m_NextLevels = levels;
            m_Active.MoveMenuDown();
        }
    }

    public void MenuCanSwitch()
    {
        if (m_Active == m_NextLevels)
        {
            m_Active = null;
            m_NextLevels = null;
        }
        else if (m_NextLevels != null)
        {
            m_Active = m_NextLevels;
            m_Active.MoveMenuUp();
            m_NextLevels = null;
        }
    }

    private void Update()
    {
        if (m_Active != null)
        {
            m_Active.Move();
        }
        
    }

}
