using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;

public class ViveWebcam : MonoBehaviour 
{
	[SerializeField]
	private GameObject m_CamScreen;
    [SerializeField]
    private GameObject m_ScreenPosition;
    [SerializeField]
    private GameObject m_Head;
	private WebCamTexture m_WebcamTexture;
	private Renderer m_Renderer;

	private void Awake()
	{
		 m_Renderer = m_CamScreen.GetComponent<Renderer>();
	}

	private void Start()
	{	
		WebCamDevice[] devices = WebCamTexture.devices;
		if (devices.Length == 2)
		{
            for (int i = 0; i < devices.Length; i++)
            {
                if (devices[i].name == "HTC Vive")
                {
                    m_WebcamTexture = new WebCamTexture(devices[i].name, 400, 300, 12);
                    m_Renderer.material.mainTexture = m_WebcamTexture;
                    m_WebcamTexture.Stop();
                    break;
                }
            }
            //         Debug.Log("HTC Vive");
            //m_WebcamTexture = new WebCamTexture(devices[0].name, 400, 300, 12);
            //         m_Renderer.material.mainTexture = m_WebcamTexture;
            //         m_WebcamTexture.Pause();
            if (m_WebcamTexture != null)
            {
                if (SceneManager.GetActiveScene().name != "MainMenu")
                {
                    GameManager.m_Instance.m_EventSystem.E_ShowViveCamScreen += EnableCamera;
                    GameManager.m_Instance.m_EventSystem.E_HideViveCamScreen += DisableCamera;
                }
            }
        }
	}

    private void OnDestroy()
    {
        if (m_WebcamTexture != null)
        {
            m_WebcamTexture.Stop();
        }
    }

    private void Update()
    {
        if (m_WebcamTexture != null)
        {
            transform.position = Vector3.Lerp(transform.position, m_ScreenPosition.transform.position, 3f * Time.deltaTime);
            transform.LookAt(m_Head.transform.position);
        }
    }

    public void EnableCamera()
	{
        if (m_WebcamTexture != null)
        {
            m_CamScreen.SetActive(true);
            m_WebcamTexture.Play();
        }
	}

	public void DisableCamera()
	{
        if (m_WebcamTexture != null)
        {
            m_WebcamTexture.Pause();
            m_CamScreen.SetActive(false);
        }
	}
}