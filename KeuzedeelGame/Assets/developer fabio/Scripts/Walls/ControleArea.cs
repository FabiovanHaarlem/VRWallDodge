using UnityEngine;

public class ControleArea : MonoBehaviour
{
    [SerializeField]
    private Controller m_Controller;
    private MovingBlock m_Block;
    private bool m_HitCheckpoint;

    private void Awake()
    {
        m_HitCheckpoint = false;
    }

    public void SetCollisionLayer(LayerMask layer, MovingBlock block)
    {
        m_Block = block;
        this.gameObject.layer = layer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_HitCheckpoint == false && other.CompareTag("CheckpointCheck"))
        {
            GameManager.m_Instance.m_EventSystem.HitBlock();
        }
        else
        {
            switch (m_Controller)
            {
                case Controller.HeadController:
                    if (other.CompareTag("HeadController"))
                    {
                        m_HitCheckpoint = true;
                    }
                    break;
                case Controller.HandControllers:
                    if (other.CompareTag("HandControllers"))
                    {
                        m_HitCheckpoint = true;
                    }
                    break;
                case Controller.Anyone:
                    m_HitCheckpoint = true;
                    break;

            }

            if (other.CompareTag("CheckpointCheck"))
            {
                m_Block.DisableBlock();
            }
        }
    }
}

public enum Controller
{
    HeadController = 0,
    HandControllers,
    Anyone
}
