using UnityEngine;

public class DirectionArrow : MonoBehaviour
{
    [SerializeField]
    private WaveSpawnController m_WaveSpawnController;
    private MovingBlock m_FollowBlock;

    private void Start()
    {
        ActivateRef();
    }

    private void ActivateRef()
    {
        m_WaveSpawnController.ArrowActive(this);
    }

    public void FollowNextBlock(MovingBlock block)
    {
        m_FollowBlock = block;
    }

    private void Update()
    {
        if (m_FollowBlock != null)
        {
            Vector3 dir = new Vector3(m_FollowBlock.transform.position.x, m_FollowBlock.transform.position.y, m_FollowBlock.transform.position.z);
            transform.LookAt(dir);
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
        }
    }
}
