using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class Fade : Singleton<Fade>
{
    public Image fade;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartCoroutine(Fading());
    }

    public void GameReset()
    {
        StartCoroutine(Fading());
    }

    private IEnumerator Fading()
    {
        yield return new WaitForSeconds(3f);

        fade.gameObject.SetActive(true);
        float progress = 0f;

        Color startColor = fade.color;
        Color endColor = fade.color;

        startColor.a = 0f;
        endColor.a = 1f;

        while (progress <= 1f)
        {
            progress += Time.deltaTime;
            fade.color = Color.Lerp(startColor, endColor, progress);
            yield return null;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        while (progress >= 0f)
        {
            progress -= Time.deltaTime;
            fade.color = Color.Lerp(startColor, endColor, progress);
            yield return null;
        }
        
    }
}
