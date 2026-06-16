using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _targets;
    [SerializeField] private float _spawnRate = 1f;
    [SerializeField] private int _lives = 3;
    private int _totalScore = 0;
    [SerializeField] private IntEventChannelSO _onScoreChange;
    [SerializeField] private IntEventChannelSO _onLiveMinus;
    [SerializeField] private IntEventChannelSO _onLiveChange;
    [SerializeField] private InputSystem_Actions _inputSystem;
    [SerializeField] private BoolEventChannelSO _onGameOver;
    [SerializeField] private VoidEventChannelSO _onGameRestart;
    // Pause event
    [SerializeField] private VoidEventChannelSO _onResumeGame;
    [SerializeField] private BoolEventChannelSO _onPauseGame;
    [SerializeField] private VoidEventChannelSO _onQuitGame;
    [SerializeField] private GameObject _titleScreen;
    // Target Event
    [SerializeField] private IntEventChannelSO _onTargetHit;
    [SerializeField] private VoidEventChannelSO _onBombHit;
    private bool _isGameActive = false;

    private void Awake()
    {
        _inputSystem = new();
    }
    private void OnEnable()
    {
        _inputSystem.Enable();
        _inputSystem.UI.Pause.performed += OnPauseGame;
        _onLiveMinus.OnEventRaised += OnLiveMinus;
        _onGameRestart.OnEventRaised += OnRestartGame;
        _onResumeGame.OnEventRaised += OnResumeGame;
        _onQuitGame.OnEventRaised += OnQuitGame;
        _onBombHit.OnEventRaised += OnBombHit;
        _onTargetHit.OnEventRaised += OnTargetHit;

    }

    private void OnDisable()
    {
        _inputSystem.UI.Pause.performed -= OnPauseGame;
        _onLiveMinus.OnEventRaised -= OnLiveMinus;
        _onGameRestart.OnEventRaised -= OnRestartGame;
        _onResumeGame.OnEventRaised -= OnResumeGame;
        _onQuitGame.OnEventRaised -= OnQuitGame;
        _onBombHit.OnEventRaised -= OnBombHit;
        _onTargetHit.OnEventRaised -= OnTargetHit;
        _inputSystem.Disable();
    }


    private void OnTargetHit(int score)
    {
        if (!_isGameActive) 
        {
            return;
        }
        _totalScore += score;
        _onScoreChange?.RaiseEvent(_totalScore);
    }

    private void OnBombHit()
    {
        if(!_isGameActive)
        {
            return;
        }
        GameOver();
    }

    private void GameOver()
    {
        _onGameOver.RaiseEvent(true);
        _isGameActive = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
    private IEnumerator SpawnTarget()
    {
        while(_isGameActive)
        {
            yield return new WaitForSeconds(_spawnRate);
            Instantiate(_targets[Random.Range(0, _targets.Count)]);
        }
    }

    private void OnRestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    private void OnLiveMinus(int live)
    {
        if(!_isGameActive)
        {
            return;
        }
        _lives += live;
        _onLiveChange.RaiseEvent(_lives);
        if(_lives <= 0)
        {
            GameOver();
        }
    }

    private void OnPauseGame(InputAction.CallbackContext context)
    {
        _onPauseGame.RaiseEvent(true);
        Time.timeScale = 0f;
    }

    private void OnQuitGame()
    {
        // 1. If running inside the Unity Editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
            
        // 2. If running as a standalone built application (Windows, Mac, Mobile, etc.)
        #else
            Application.Quit();
        #endif
    }

    private void OnResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void StartGame(int difficulty)
    {
        _spawnRate /= difficulty;
        _isGameActive = true;
        _totalScore = 0;
        _onScoreChange.RaiseEvent(_totalScore);
        _onLiveChange.RaiseEvent(_lives);
        StartCoroutine(SpawnTarget());
        _titleScreen.SetActive(false);
    }

}
