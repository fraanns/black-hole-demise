using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        Vector3 newPos = Vector3.Lerp(transform.position, player.position, 2 * Time.deltaTime);

        newPos.z = transform.position.z;

        transform.position = newPos;
    }
}
