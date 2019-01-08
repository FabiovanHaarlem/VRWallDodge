using System.Collections.Generic;
using UnityEngine;

public class MainMenuWorldBuilder : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_BuildingLevel;
    [SerializeField]
    private Transform m_BuildingLevelStartPosition;

    public void ActivatePiece()
    {
        for (int i = 0; i < m_BuildingLevel.Count; i++)
        {
            if (!m_BuildingLevel[i].activeInHierarchy)
            {
                m_BuildingLevel[i].SetActive(true);
                m_BuildingLevel[i].transform.position = m_BuildingLevelStartPosition.position;
                break;
            }
        }
    }
}
