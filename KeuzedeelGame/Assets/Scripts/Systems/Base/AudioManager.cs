using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_Music;
    [SerializeField]
    private AudioSource m_Effects;

    private void Start()
    {
        m_Music.volume = 1.0f;
        m_Effects.volume = 1.0f;
    }

    public void UpdateAudioVolume()
    {
        m_Music.volume = SavedOptions.m_MusicVolume * SavedOptions.m_MasterVolume;
        m_Effects.volume = SavedOptions.m_EffectsVolume * SavedOptions.m_MasterVolume;
        Debug.Log(SavedOptions.m_MusicVolume * SavedOptions.m_MasterVolume);
        Debug.Log("Update " + m_Music.volume);

    }
}
