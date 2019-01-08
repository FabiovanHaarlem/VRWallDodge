using UnityEngine;

public class MenuControllers : MonoBehaviour
{
    private SteamVR_TrackedObject m_TrackedObject;
    private ControllerInput m_ControllerInput;

    private SteamVR_Controller.Device m_Controller
    {
        get { return SteamVR_Controller.Input((int)m_TrackedObject.index); }
    }

    private void Awake()
    {
        m_TrackedObject = GetComponent<SteamVR_TrackedObject>();
        m_ControllerInput = GetComponent<ControllerInput>();
    }

    private void Start()
    {
        m_ControllerInput.E_OnTriggerDownEvent += ToggleViveCamera;
    }

    private void ToggleViveCamera()
    {
        MenuManager.m_Instance.ToggleViveCam();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LevelTrigger"))
        {
            MenuManager.m_Instance.GoToLevel(other.name);
        }
    }
}
