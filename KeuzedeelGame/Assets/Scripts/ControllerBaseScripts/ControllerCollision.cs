using UnityEngine;

public class ControllerCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameManager.m_Instance.m_EventSystem.HitBlock();
            GameManager.m_Instance.SetupDeathScene(this.gameObject.name, other.gameObject.transform.parent.gameObject);
        }
    }
}
