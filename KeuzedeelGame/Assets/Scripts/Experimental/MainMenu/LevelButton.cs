using UnityEngine;

public class LevelButton : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            MenuManager.m_Instance.GoToLevel(other.name);
        }
    }
}
