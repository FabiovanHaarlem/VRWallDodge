using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Player;
    private Vector3 m_DefaultPosition;

    private bool m_UnParented;

    private void Awake()
    {
        m_DefaultPosition = new Vector3(0f, 0f, 0f);
        m_Player.transform.SetParent(this.transform);
        transform.position = new Vector3(0f, -2.3f, 0f);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, m_DefaultPosition) >= 0.01f)
            transform.position = Vector3.MoveTowards(transform.position, m_DefaultPosition, 0.4f * Time.deltaTime);
        else
        {
            if (!m_UnParented)
            {
                m_Player.transform.SetParent(null);
                m_UnParented = true;
            }
        }
    }
}
