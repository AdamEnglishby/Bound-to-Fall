using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicSystem : MonoBehaviour
{
    // Singleton - prevents more than one Music System instance
    private static MusicSystem _instance;

    public static MusicSystem Instance
    {
        get
        {
            TouchInstance();

            return _instance;
        }
    }

    //Called by MusicSystemSpawner to instantiate the Music System
    public static void TouchInstance()
    {
        if (!_instance)
        {
            MusicSystem prefab = Resources.Load<MusicSystem>("MusicSystem");
            if (prefab)
            {
                _instance = Instantiate(prefab);
            }
        }
    }

    [SerializeField]
    AK.Wwise.Event musicSystemEvent;

    private Scene currentScene;
    private Scene activeScene;

    private GameObject player;

    private bool bInCombat;

    void Awake()
    {
        //Prevents multiple MusicSystem instances
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        currentScene = SceneManager.GetActiveScene();

        MusicLevelStateSelector();

        player = GameObject.FindGameObjectWithTag("Player");

        //Play music system Event
        musicSystemEvent.Post(gameObject);
    }

    void Update()
    {
        currentScene = SceneManager.GetActiveScene();

        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (activeScene != currentScene)
        {
            MusicLevelStateSelector();
        }
        
    }

    void MusicLevelStateSelector()
    {
        /* CUSTOM MusicLevelStateSelector() SHENANIGANS
         * Set defaults for custom states every time Gameplay chances.
         */

        if (currentScene.name == "MainMenu")
        {
            AkSoundEngine.SetState("Gameplay", "MainMenu");
        }
        if (currentScene.name == "Maze")
        {
            AkSoundEngine.SetState("Gameplay", "Maze");
        }

        activeScene = currentScene;
    }

}
