using System.Collections.Generic;

public static class SelectedLevelData
{
    private static List<SOLevelValue> m_LevelsData;
    private static SOLevelValue m_ActiveLevelData;

    public static SOLevelValue GetLevelData()
    {
        return m_ActiveLevelData;
    }

    public static void SetLevelsData(List<SOLevelValue> levelsData)
    {
        m_LevelsData = new List<SOLevelValue>(levelsData);
    }

    public static void SetLevelsData(SOLevelValue devLevel)
    {
        m_ActiveLevelData = devLevel;
    }

    public static void SetActiveLevelData(string levelName)
    {
        for (int i = 0; i < m_LevelsData.Count; i++)
        {
            if (levelName == m_LevelsData[i].m_LevelName)
            {
                m_ActiveLevelData = m_LevelsData[i];
                break;
            }
        }
    }
}
