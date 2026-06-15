using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float _upwardForceMin = 10f;
    [SerializeField] private float _upwardForceMax = 20f;
    [SerializeField] private float _torqueRandom = 10f;
    [SerializeField] private int _score = 10;
    [SerializeField] private ParticleSystem _explosionVfx;
    private float _xRange = 4f;
    private float _yRange = -4f;
    private Rigidbody _rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(RandomForce(),ForceMode.Impulse);
        _rb.AddTorque(RandomTorque(),
        RandomTorque(),
        RandomTorque(),
        ForceMode.Impulse
        );
        transform.position = RandomPos();
    }

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(_upwardForceMin, _upwardForceMax);
    }

    private Vector3 RandomPos()
    {
        return new Vector3(Random.Range(-_xRange, _xRange), _yRange);
    }

    private float RandomTorque()
    {
        return Random.Range(-_torqueRandom, _torqueRandom);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        Instantiate(_explosionVfx, transform.position, transform.rotation);
    }
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("DestroyZone"))
        {
            Destroy(gameObject);
        }
    }

    public int GetScore()
    {
        return _score;
    }
}
