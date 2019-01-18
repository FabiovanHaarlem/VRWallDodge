using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    private GameObject m_HeadTracker;
    private GameObject m_LastHeadPosition;
    [SerializeField]
    private GameObject m_PauseMenu;
    [SerializeField]
    private GameObject m_RightHand;

    //Sets Variables
    public void SetVariables()
    {
        m_HeadTracker = GameObject.Find("HeadTrigger");
        m_LastHeadPosition = GameObject.Find("LastPositionHead");
    }

    //Gets Refs
    public void GetRefs()
    {
        DisableObjects(new GameObject[2] { m_LastHeadPosition, m_PauseMenu });
        GameManager.m_Instance.m_EventSystem.E_PauseGameEvent += PauseMenu;
        m_PauseMenu.SetActive(false);
    }

    //Gets all gameObjects for important objects for this class
    private void GetObjectRefs()
    {
        m_HeadTracker = GameObject.Find("HeadTrigger");
        m_LastHeadPosition = GameObject.Find("LastPositionHead");
    }

    //Disable given objects
    private void DisableObjects(GameObject[] gameObjectRef)
    {
        for (int i = 0; i < gameObjectRef.Length; i++)
        {
            if (gameObjectRef[i] != null)
            {
                gameObjectRef[i].SetActive(false);
            }
        }
    }

    //Activate given objects
    private void ActivateObjects(GameObject[] gameObjectRef)
    {
        for (int i = 0; i < gameObjectRef.Length; i++)
        {
            if (gameObjectRef[i] != null)
            {
                gameObjectRef[i].SetActive(true);
            }
        }
    }

    //Gets called when game is paused
    private void PauseMenu()
    {
        ActivateObjects(new GameObject[2] { m_LastHeadPosition, m_PauseMenu });
        m_LastHeadPosition.transform.position = m_HeadTracker.transform.position;
    }

    //Gets called when game is unpaused
    private void UnpauseGame()
    {
        m_LastHeadPosition.SetActive(false);
        m_PauseMenu.SetActive(false);
        GameManager.m_Instance.Unpause();
    }

    //Checks if player head is near position when game paused
    public void CheckIfCanUnpause()
    {  
        if (Vector3.Distance(m_LastHeadPosition.transform.position, m_HeadTracker.transform.position) < 0.3)
        {
            UnpauseGame();
        }
    }
}
