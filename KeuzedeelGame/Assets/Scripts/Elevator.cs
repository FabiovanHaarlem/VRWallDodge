using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{

    private void Update()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y + 1.5f * Time.deltaTime, transform.position.z);
    }
}
