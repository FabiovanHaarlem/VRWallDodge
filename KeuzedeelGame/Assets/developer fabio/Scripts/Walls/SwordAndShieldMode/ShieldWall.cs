using System.Collections.Generic;
using UnityEngine;

public class ShieldWall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield"))
        {
            gameObject.SetActive(false);
        }
    }

}
