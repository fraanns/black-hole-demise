using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndTrigger : MonoBehaviour
{
    [SerializeField]
    UnityEngine.Playables.PlayableDirector director;

    [SerializeField]
    TMPro.TMP_Text timeText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            director.Play();
            collision.GetComponent<PlayerMovement>().enabled = false;

            StartCoroutine("Wait");
        }
    }

    IEnumerator Wait()
    {
        float time = Time.timeSinceLevelLoad;

        yield return new WaitForSeconds(2);

        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
 
        timeText.text = $"Goal reached in {timeSpan.Minutes} min. {timeSpan.Seconds} sec.";
    }
}
