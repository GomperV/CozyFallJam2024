using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public FMODUnity.EventReference musicEvent, winEvent, loseEvent;

    private FMOD.Studio.EventInstance musicInstance, winInstance, loseInstance;

    public static MusicManager Instance;
    public bool gameOver;
    public bool victory;

    private bool _gameOverPlayed;
    private bool _victoryPlayed;

    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        musicInstance = FMODUnity.RuntimeManager.CreateInstance(musicEvent);
        winInstance = FMODUnity.RuntimeManager.CreateInstance(winEvent);
        loseInstance = FMODUnity.RuntimeManager.CreateInstance(loseEvent);

        musicInstance.start();
    }

    void Update()
    {
        string scene = SceneManager.GetActiveScene().name;

        _gameOverPlayed = _gameOverPlayed && gameOver;
        _victoryPlayed = _victoryPlayed && victory;

        if(gameOver)
        {
            _gameOverPlayed = true;
            loseInstance.start();
        }
        else if (victory)
        {
            _victoryPlayed = true;
            winInstance.start();
        }
        else
        {
            musicInstance.getPlaybackState(out FMOD.Studio.PLAYBACK_STATE state);
            if(state != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                musicInstance.start();
            }
        }
            
    }

    
}
