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
    private int _totalScore = 0;
    [SerializeField] private IntEventChannelSO _onScoreChange;
    [SerializeField] private InputSystem_Actions _inputSystem;
    [SerializeField] private BoolEventChannelSO _onGameOver;
    [SerializeField] private GameObject _titleScreen;
    private bool _isGameActive = false;

    private void Awake()
    {
        _inputSystem = new();
    }
    private void OnEnable()
    {
        _inputSystem.Enable();

        _inputSystem.UI.Click.performed += OnLeftClick;

    }

    private void OnDisable()
    {
        _inputSystem.UI.Click.performed -= OnLeftClick;    
        _inputSystem.Disable();
    }

    private void OnLeftClick(InputAction.CallbackContext callback)
    {
        if(!_isGameActive)
        {
            return;
        }
        Ray ray =
        Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.rebeccaPurple, 2f);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            Target target = hit.collider.gameObject.GetComponent<Target>();
            if(target == null)
            {
                return;
            }
            else if(target.gameObject.CompareTag("Bomb"))
            {
                _onGameOver.RaiseEvent(true);
                _isGameActive = false;
                target.Explode();
                Destroy(target.gameObject);
            }
            else
            {
                _totalScore += target.GetScore();
                _onScoreChange?.RaiseEvent(_totalScore);
                target.Explode();
                Destroy(target.gameObject);
            }
        }
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

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        _spawnRate /= difficulty;
        _isGameActive = true;
        _totalScore = 0;
        _onScoreChange.RaiseEvent(_totalScore);
        StartCoroutine(SpawnTarget());
        _titleScreen.SetActive(false);
    }

}
