using UnityEngine;

public class ShowPlayerShadowOnWalls : MonoBehaviour
{
    private GameObject m_Headset;
    private GameObject m_HeadsetForOnWall;
    private GameObject[] m_Controllers;
    private GameObject[] m_ControllersForOnWall;
    [SerializeField]
    private LayerMask m_RaycastLayer;

    private Vector3 m_RayDirection;

    private void Awake()
    {
        m_Controllers = new GameObject[2];
        m_ControllersForOnWall = new GameObject[2];
        GetObjectRefs();
    }

    //Gets all gameObjects for important objects for this class
    private void GetObjectRefs()
    {
        m_Headset = GameObject.Find("Camera (eye)");
        m_HeadsetForOnWall = GameObject.Find("HeadSetMarker");
        m_Controllers[0] = GameObject.Find("Controller (left)");
        m_Controllers[1] = GameObject.Find("Controller (right)");
        m_ControllersForOnWall[0] = GameObject.Find("ControllerSetMarkerLeft");
        m_ControllersForOnWall[1] = GameObject.Find("ControllerSetMarkerRight");
    }

    private void Update()
    {
        Vector3 rotation = m_Headset.transform.eulerAngles;
        if (rotation.y >= -45 && rotation.y <= 45 || rotation.y >= 315 && rotation.y <= 360)
        {
            m_RayDirection = Vector3.forward;
        }
        else if (rotation.y >= 45 && rotation.y <= 135)
        {
            m_RayDirection = Vector3.right;
        }
        else if (rotation.y >= 135 && rotation.y <= 225)
        {
            m_RayDirection = Vector3.back;
        }
        else if (rotation.y >= 225 && rotation.y <= 315)
        {
            m_RayDirection = Vector3.left;
        }
    }

    private void FixedUpdate()
    {
        CheckHeadsetRaycast();
        CheckControllersRaycast();
        ScaleMarkers();
    }

    //Checks of raycast from headset hits a wall and projects a headset shadow on it
    private void CheckHeadsetRaycast()
    {
        m_HeadsetForOnWall.SetActive(false);
        RaycastHit hit;
        if (Physics.Raycast(m_Headset.transform.position, m_RayDirection, out hit, m_RaycastLayer.value))
        {
            if (hit.collider.CompareTag("RaycastCollider"))
            {
                m_HeadsetForOnWall.SetActive(true);
                m_HeadsetForOnWall.transform.position = hit.point;
            }
            else
            {
                m_HeadsetForOnWall.SetActive(false);
            }
            //Debug.DrawRay(m_Headset.transform.position, m_RayDirection * 100, Color.red);
        }
    }

    //Checks of raycast from the controllers hits a wall and projects a controller shadow on it
    private void CheckControllersRaycast()
    {
        RaycastHit hit;
        for (int i = 0; i < 2; i++)
        {
            m_ControllersForOnWall[i].SetActive(false);

            if (Physics.Raycast(m_Controllers[i].transform.position, m_RayDirection, out hit, m_RaycastLayer.value))
            {
                if (hit.collider.CompareTag("RaycastCollider"))
                {
                    m_ControllersForOnWall[i].SetActive(true);
                    m_ControllersForOnWall[i].transform.position = hit.point;
                }
                else
                {
                    m_ControllersForOnWall[i].SetActive(false);
                }
            }
        }
    }

    //Scales the controllers and headset markers when player gets closer
    private void ScaleMarkers()
    {
        float dis = Vector3.Distance(m_Headset.transform.position, m_HeadsetForOnWall.transform.position);
        Vector3 scale = new Vector3();
        if (dis < 10)
        {
            scale = new Vector3(dis / 50f, dis / 50f, dis / 50f);
        }
        else
        {
            scale = new Vector3(0.2f, 0.2f, 0.2f);
        }

        m_HeadsetForOnWall.transform.localScale = scale;
        for (int i = 0; i < m_ControllersForOnWall.Length; i++)
        {
            m_ControllersForOnWall[i].transform.localScale = scale;
        }
    }   
}
