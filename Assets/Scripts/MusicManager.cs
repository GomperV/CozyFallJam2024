using System.Collections;

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
    private bool _banksLoaded;

    void Start()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {

            Instance = this;
            DontDestroyOnLoad(gameObject);

            StartCoroutine(LoadEventsRoutine());
        }
    }

    IEnumerator LoadEventsRoutine()
    {
        yield return new WaitUntil(() => FMODUnity.RuntimeManager.HasBankLoaded("Master"));

        _banksLoaded = true;

        musicInstance = FMODUnity.RuntimeManager.CreateInstance(musicEvent);
        winInstance = FMODUnity.RuntimeManager.CreateInstance(winEvent);
        loseInstance = FMODUnity.RuntimeManager.CreateInstance(loseEvent);

        musicInstance.start();
    }

    void Update()
    {
        if(!_banksLoaded) return;

        string scene = SceneManager.GetActiveScene().name;

        _gameOverPlayed = _gameOverPlayed && gameOver;
        _victoryPlayed = _victoryPlayed && victory;

        if(gameOver)
        {
            if(!_gameOverPlayed)
            {
                _gameOverPlayed = true;
                loseInstance.start();
                musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            }
        }
        else if (victory)
        {
            if(!_victoryPlayed)
            {
                _victoryPlayed = true;
                winInstance.start();
                musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            }
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
