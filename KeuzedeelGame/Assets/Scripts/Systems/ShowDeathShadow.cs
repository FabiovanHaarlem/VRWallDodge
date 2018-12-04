using UnityEngine;

public class ShowDeathShadow : MonoBehaviour
{
    [SerializeField]
    private GameObject m_HeadShadow;
    [SerializeField]
    private GameObject m_LeftHandShadow;
    [SerializeField]
    private GameObject m_RightHandShadow;
    [SerializeField]
    private Material m_HitObjectMaterial;

    private GameObject m_HitWall;

    private bool m_Called;

    public void SetupDeathScene(string hitObject, Vector3 headPos, Vector3 headRot,
        Vector3 leftHandPos, Vector3 leftHandRot, Vector3 rightHandPos, Vector3 rightHandRot, GameObject hitWall)
    {
        if (!m_Called)
        {
            m_Called = true;
            m_HitWall = hitWall;
            m_HeadShadow.SetActive(true);
            m_LeftHandShadow.SetActive(true);
            m_RightHandShadow.SetActive(true);
            hitWall.SetActive(true);
            switch (hitObject)
            {
                case "Controller (left)":
                    m_LeftHandShadow.transform.GetChild(0).GetComponent<Renderer>().material = m_HitObjectMaterial;
                    break;
                case "Controller (right)":
                    m_RightHandShadow.transform.GetChild(0).GetComponent<Renderer>().material = m_HitObjectMaterial;
                    break;
                case "HeadTrigger":
                    m_HeadShadow.transform.GetChild(0).GetComponent<Renderer>().material = m_HitObjectMaterial;
                    break;
            }
            Vector3 shadowOffset = new Vector3(0f, 0f, 2f);
            m_HeadShadow.transform.position = headPos + shadowOffset;
            m_HeadShadow.transform.eulerAngles = headRot;
            m_LeftHandShadow.transform.position = leftHandPos + shadowOffset;
            m_LeftHandShadow.transform.eulerAngles = leftHandRot;
            m_RightHandShadow.transform.position = rightHandPos + shadowOffset;
            m_RightHandShadow.transform.eulerAngles = rightHandRot;
            hitWall.transform.position += shadowOffset;
        }
    }

    private void Update()
    {
        if (m_HitWall != null)
        {
            if (!m_HitWall.activeInHierarchy)
            {
                m_HitWall.SetActive(true);
            }
        }
    }
}
