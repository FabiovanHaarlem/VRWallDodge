using UnityEngine;

public class BuildingLevel : MonoBehaviour
{
    [SerializeField]
    private MainMenuWorldBuilder m_Builder;

    private float m_Speed;

    private void Awake()
    {
        m_Speed = 1f;
    }

    private void Update()
    {
        transform.position += new Vector3(0f, -m_Speed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        m_Builder.ActivatePiece();
        gameObject.SetActive(false);
    }
}
