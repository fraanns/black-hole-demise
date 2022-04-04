using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectKiller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ISuckable suckable = collision.GetComponent<ISuckable>();

        if (suckable != null)
        {
            suckable.DestroyAnim ();
        }
    }
}
