using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour {

    public Sprite[] loadingBackgrounds;
    public string[] tips;

    private LoadingView view;

    // Use this for initialization
    void Awake () {
        view = GetComponent<LoadingView>();
	}

    public void LoadScene(SceneLoader.Scene scene, bool loadingScreen)
    {
        if (!loadingScreen)
        {
            // Para cargas rapidas que no necesitan pantalla de carga
            SceneManager.LoadScene(scene.ToString());
        }
        else
        {
            // Para cargas mas pesadas que requieren pantalla de carga
            view.canvas.SetActive(true);
            StartCoroutine(SwitchLoadingBackground());
            StartCoroutine(Load(scene));
        }
    }

    // Corutina: Carga una nueva escena y mide el progreso en una pantalla de carga
    private IEnumerator Load(SceneLoader.Scene scene)
    {
        Debug.Log("Loading the new scene");
        yield return new WaitForSeconds(20);
        // Operacion asincrona que carga la escena 
        AsyncOperation op = SceneManager.LoadSceneAsync(scene.ToString());
        while (!op.isDone)
        {
            yield return null;
        }
    }
    private IEnumerator SwitchLoadingBackground()
    {
        int i = -1;
        int tmp = -1;
        while (true)
        {
            while (i == tmp) {
                tmp = Random.Range(0, tips.Length);
            }
            i = tmp;
            view.SetBackground(loadingBackgrounds[i]);
            view.SetTip(tips[i]);
            yield return new WaitForSeconds(6);
        }
    }
}
