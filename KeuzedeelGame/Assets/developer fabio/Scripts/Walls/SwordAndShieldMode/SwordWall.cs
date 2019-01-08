using System.Collections.Generic;
using UnityEngine;

public class SwordWall : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_Hitpoints;

    private int m_PointsToBeHit;

    private void Awake()
    {
        m_PointsToBeHit = m_Hitpoints.Count;
    }

    public void PointHit()
    {
        m_PointsToBeHit--;
        if (m_PointsToBeHit == 0)
        {
            gameObject.SetActive(false);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Sword"))
    //    {
    //        for (int i = 0; i < m_Walls.Count; i++)
    //        {
    //            m_Walls[i].SetActive(false);
    //        }
    //        gameObject.SetActive(false);
    //    }
    //}
}
