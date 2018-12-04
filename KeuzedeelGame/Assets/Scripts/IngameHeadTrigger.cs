using UnityEngine;

public class IngameHeadTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameManager.m_Instance.m_EventSystem.HitBlock();
        }
    }
}
