using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> targets;
    [SerializeField] private float _spawnRate = 1f;
    private int _totalScore = 0;
    [SerializeField] private IntEventChannelSO _onScoreChange;
    [SerializeField] private InputSystem_Actions _inputSystem;

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
    }

    private void OnLeftClick(InputAction.CallbackContext callback)
    {
        Ray ray =
        Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.rebeccaPurple, 2f);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            Target target = hit.collider.gameObject.GetComponent<Target>();
            if(target != null)
            {
                _totalScore += target.GetScore();
                _onScoreChange?.RaiseEvent(_totalScore);
                Destroy(target.gameObject);
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnTarget());   
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
    private IEnumerator SpawnTarget()
    {
        while(true)
        {
            yield return new WaitForSeconds(_spawnRate);
            Instantiate(targets[Random.Range(0, targets.Count)]);
        }
    }


}
