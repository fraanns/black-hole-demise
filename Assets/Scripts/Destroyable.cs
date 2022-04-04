using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour, ISuckable
{
    public void DestroyAnim()
    {
        StartCoroutine("DoDestroyAnim");
    }

    IEnumerator DoDestroyAnim()
    {
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null)
            rb.gravityScale = 0;

        Vector3 originalScale = transform.localScale;

        for (float i = 0; i < 1; i += 5 * Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, i);
            yield return 0;
        }

        //yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }
}
