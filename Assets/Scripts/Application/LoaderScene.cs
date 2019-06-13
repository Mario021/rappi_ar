using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderScene : MonoBehaviour {

    [Header("Options Loading")]
    public GameObject loadingUI;
    // Indica el tiempo de espera despues de terminar de cargar una escena
    public float delayTimeLoading;

    void Start ()
    {
		
	}

    public void LoadScene(int id)
    {
        StartCoroutine(LoadAsyncScene(id));
    }

    IEnumerator LoadAsyncScene(int id)
    {
        loadingUI.SetActive(true);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(id);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        LeanTween.delayedCall(delayTimeLoading, () =>
        {
            loadingUI.SetActive(false);
        });
    }
}
