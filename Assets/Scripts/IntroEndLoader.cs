using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroEndLoader : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine("Wait");
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(7);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
