using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneLoader", menuName = "SceneLoader", order = 51)]
public class SceneLoader : ScriptableObject {

    // Todas las escenas del juego
    public enum Scene {StartMenu, Mapa1};

    public void LoadScene(Scene scene, bool loadingScreen, GameObject source)
    {
        GameObject loading = Instantiate(Resources.Load("Prefabs/Loading")) as GameObject;
        loading.GetComponent<LoadingController>().LoadScene(scene, loadingScreen);
        if (loadingScreen)
        {
            source.SetActive(false);
        }
    }   
}
