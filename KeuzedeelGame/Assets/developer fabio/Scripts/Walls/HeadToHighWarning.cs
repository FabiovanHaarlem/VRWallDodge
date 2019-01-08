using UnityEngine;

public class HeadToHighWarning : MonoBehaviour
{
    private GameObject m_HeadSetTrigger;
    [SerializeField]
    private Material m_HeadLowEnough;
    [SerializeField]
    private Material m_HeadToHigh;

    private Renderer m_Renderer;

    private void Awake()
    {
        m_HeadSetTrigger = GameObject.Find("HeadTrigger");
        m_Renderer = GetComponent<Renderer>();
    }

    //Checks if playerhead is higher than the duck height needed
    private void Update()
    {
        if (m_HeadSetTrigger.transform.position.y + 0.25 <= transform.position.y - 0.05)
        {
            m_Renderer.material = m_HeadLowEnough;
        }
        else
        {
            m_Renderer.material = m_HeadToHigh;
        }
    }

}
