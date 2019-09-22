using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    private bool isEnter = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            StartCoroutine(DelayReSetScene());
        }
    }
    private IEnumerator DelayReSetScene()
    {
        AudioManager._Instance.PlayAudioFormComponent(gameObject, ClipName.ZombieScream, true, false);
        yield return new WaitForSeconds(1.5f);
        if (!isEnter)
        {
            isEnter = true;
            SceneManager.LoadScene("Game");
        }
    }
}
