using UnityEngine;

public class DevLevel : MonoBehaviour
{
    [SerializeField]
    private SOLevelValue m_LevelsData;

    private void Awake()
    {
        SelectedLevelData.SetLevelsData(m_LevelsData);
    }
}
