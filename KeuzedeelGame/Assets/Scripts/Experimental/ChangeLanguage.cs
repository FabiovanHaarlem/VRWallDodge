using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeLanguage : MonoBehaviour
{
    [SerializeField]
    private List<LanguageSet> m_LanguageSets;
    private LanguageSet m_ActiveLanguageSet;

    private int m_ActiveLanguageIndex;

    #region MainButtons
    [SerializeField]
    private TextMeshProUGUI m_LevelSelect, m_MapSelect, m_Calibrate, m_Options, m_Quit;
    #endregion

    #region Options
    [SerializeField]
    private TextMeshProUGUI m_MasterVolume, m_MusicVolume, m_EffectsVolume, m_UISize, m_Gamma, m_Language;
    #endregion

    private void Start()
    {
        m_ActiveLanguageSet = m_LanguageSets[0];
    }

    public void SwitchLanguage()
    {
        m_ActiveLanguageIndex++;
        if (m_ActiveLanguageIndex == m_LanguageSets.Count)
        {
            m_ActiveLanguageIndex = 0;
        }

        m_ActiveLanguageSet = m_LanguageSets[m_ActiveLanguageIndex];
        ChangeAllText();
    }

    public LanguageSet GetLanguageSet()
    {
        return m_ActiveLanguageSet;
    }

    private void ChangeAllText()
    {
        m_LevelSelect.text = m_ActiveLanguageSet.m_LevelSelect;
        m_MapSelect.text = m_ActiveLanguageSet.m_MapSelect;
        m_Calibrate.text = m_ActiveLanguageSet.m_Calibrate;
        m_Options.text = m_ActiveLanguageSet.m_Options;
        m_Quit.text = m_ActiveLanguageSet.m_Quit;

        m_MasterVolume.text = m_ActiveLanguageSet.m_MasterVolume;
        m_MusicVolume.text = m_ActiveLanguageSet.m_MusicVolume;
        m_EffectsVolume.text = m_ActiveLanguageSet.m_EffectsVolume;
        m_UISize.text = m_ActiveLanguageSet.m_UISize;
        m_Gamma.text = m_ActiveLanguageSet.m_Gamma;
        m_Language.text = m_ActiveLanguageSet.m_Language;

    }
}
