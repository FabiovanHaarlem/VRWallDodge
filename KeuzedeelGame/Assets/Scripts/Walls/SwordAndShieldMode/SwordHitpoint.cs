using UnityEngine;

public class SwordHitpoint : MonoBehaviour
{
    [SerializeField]
    private SwordWall m_SwordWall;
    [SerializeField]
    private Material m_Active;

    private Renderer m_Renderer;

    private void Awake()
    {
        m_Renderer = GetComponent<Renderer>();
    }

    private bool m_Hit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            if (m_Hit == false)
            {
                m_Renderer.material = m_Active;
                m_Hit = true;
                m_SwordWall.PointHit();
            }
        }
    }
}
