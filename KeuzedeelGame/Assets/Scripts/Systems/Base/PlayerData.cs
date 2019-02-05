public static class PlayerData
{
    private static float m_DefaultPlayerHeight = 1.7f;
    private static float m_DefaultPlayerDuckHeight = 1.0f;
    private static float m_PlayerHeight = m_DefaultPlayerHeight;
    private static float m_PlayerDuckHeight = m_DefaultPlayerDuckHeight;

    public static AudioManager m_AudioManager;

    public static float GetPlayerHeightDiffrence()
    {
        return  m_PlayerHeight - m_DefaultPlayerHeight;
    }

    public static float GetPlayerHeight()
    {
        return m_PlayerHeight;
    }

    public static void SetPlayerHeight(float height)
    {
        m_PlayerHeight = height;
    }

    public static float GetPlayerDuckHeightMax()
    {
        return m_PlayerDuckHeight;
    }

    public static void SetPlayerDuckHeight(float height)
    {
        m_PlayerDuckHeight = height + 0.05f;
    }
}
