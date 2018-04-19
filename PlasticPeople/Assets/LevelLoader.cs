using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Diagnostics;

public class LevelLoader : MonoBehaviour {

    public GameObject loadingScreen;
    public Slider slider;

    public void LoadLevel(int sceneIndex)
    {
        Stopwatch watch = Stopwatch.StartNew();
        StartCoroutine(LoadAsynchronously(sceneIndex));
        UnityEngine.Debug.Log("Load Level: " + (watch.ElapsedMilliseconds / 1000) + " seconds");
    }

    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        Stopwatch watch = Stopwatch.StartNew();
        float fadeTime = GameObject.Find("Fade").GetComponent<Fading>().BeginFade(1);

        //AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        SceneManager.LoadSceneAsync(sceneIndex);
        yield return new WaitForSeconds(fadeTime);

        //loadingScreen.SetActive(true);

        /*bool test = true;
        while (test)
        {
            if (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);

                slider.value = progress;

                //Debug.Log("Load: " + progress);

                yield return null;
            }
            else
            {
                test = false;
                UnityEngine.Debug.Log("Load Level While: " + (watch.ElapsedMilliseconds / 1000) + " seconds");
            }
        }*/

    }
	
}
