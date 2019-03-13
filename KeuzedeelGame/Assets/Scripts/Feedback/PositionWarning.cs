using UnityEngine;
using TMPro;

public class PositionWarning : MonoBehaviour
{
    #region Editor Variables

    [SerializeField]
    private GameObject m_PlayerHead;
    [SerializeField]
    private GameObject m_MiddleOfPlayArea;
    [SerializeField]
    private TextMeshProUGUI m_WarningText;
    [SerializeField]
    private float m_SideDistance;
    [SerializeField]
    private float m_ForwardAndBackDistance;

    #endregion

    #region LifeCycle

    private void Update()
    {
        Vector3 playerHorizontalPosition = new Vector3(m_PlayerHead.transform.position.x, 0.0f,
            m_PlayerHead.transform.position.z);
        Vector3 middleOfPlayAreaHorizontalPosition = new Vector3(m_MiddleOfPlayArea.transform.position.x, 0.0f,
            m_MiddleOfPlayArea.transform.position.z);

        if (playerHorizontalPosition.x > middleOfPlayAreaHorizontalPosition.x + m_SideDistance)
        {
            ShowWarning("MOVE LEFT");
        }
        else if (playerHorizontalPosition.x < middleOfPlayAreaHorizontalPosition.x - m_SideDistance)
        {
            ShowWarning("MOVE RIGHT");
        }
        else if (playerHorizontalPosition.z > middleOfPlayAreaHorizontalPosition.z + m_ForwardAndBackDistance)
        {
            ShowWarning("MOVE BACK");
        }
        else if (playerHorizontalPosition.z < middleOfPlayAreaHorizontalPosition.z - m_ForwardAndBackDistance)
        {
            ShowWarning("MOVE FORWARD");
        }
        else
        {
            HideWarning();
        }
    }

    #endregion

    #region Private Methods

    private void ShowWarning(string warning)
    {
        m_WarningText.gameObject.SetActive(true);
        m_WarningText.text = warning;
    }

    private void HideWarning()
    {
        m_WarningText.gameObject.SetActive(false);
    }

    #endregion
}
