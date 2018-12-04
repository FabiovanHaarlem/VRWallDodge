using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource m_AudioSource;
    private void Start()
    {
        if (PlayerData.m_AudioManager == null)
        {
            DontDestroyOnLoad(this.gameObject);
            PlayerData.m_AudioManager = this;
            m_AudioSource = GetComponent<AudioSource>();
            m_AudioSource.Play();

        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
