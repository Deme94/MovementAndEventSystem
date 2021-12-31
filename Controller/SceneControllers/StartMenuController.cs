using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour {

    public SceneLoader sceneLoader;

    private AudioSource startMenuMusic;

	// Use this for initialization
	void Start () {
        startMenuMusic = GetComponent<AudioSource>();
        startMenuMusic.Play();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
	}

    private void StartGame()
    {
        sceneLoader.LoadScene(SceneLoader.Scene.Mapa1, true, this.gameObject);
    }
}
