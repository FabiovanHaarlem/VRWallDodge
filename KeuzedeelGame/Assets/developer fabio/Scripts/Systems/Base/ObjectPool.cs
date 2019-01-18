using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private List<MovingBlock> m_MovingBlocks;

    //Sets Variables
    public void SetVariables()
    {
        m_MovingBlocks = new List<MovingBlock>();
    }

    public MovingBlock GetObstacle()
    {
        for (int i = 0; i < m_MovingBlocks.Count; i++)
        {
            if (!m_MovingBlocks[i].gameObject.activeInHierarchy)
            {
                return m_MovingBlocks[i];
            }
        }

        return m_MovingBlocks[0];
    }
}
