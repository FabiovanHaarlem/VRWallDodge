using UnityEngine;

public class SpotLight : MonoBehaviour
{
    [SerializeField]
    private Transform m_Player;

    private void Update()
    {
        transform.LookAt(m_Player);
    }
}
