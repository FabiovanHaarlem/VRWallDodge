using UnityEngine;

[CreateAssetMenu(fileName = "LanguageSet", menuName = "Create New Languege Set")]
public class LanguageSet : ScriptableObject
{
    public string m_SetName;

    #region MainButtons
    public string m_LevelSelect;
    public string m_MapSelect;
    public string m_Calibrate;
    public string m_Options;
    public string m_Quit;
    #endregion

    #region LevelSelect
    public string m_EasyMode;
    public string m_MediumMode;
    public string m_HardMode;
    public string m_SpecialMode;
    #endregion

    #region Options
    public string m_MasterVolume;
    public string m_MusicVolume;
    public string m_EffectsVolume;
    public string m_UISize;
    public string m_Gamma;
    public string m_Language;
    #endregion
}
