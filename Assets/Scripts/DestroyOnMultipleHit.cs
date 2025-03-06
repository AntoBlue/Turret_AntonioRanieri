using UnityEngine;

public class DestroyOnMultipleHit : MonoBehaviour
{
    [SerializeField] int maxHitCount = 10;
    [SerializeField] private bool randomHitcount = true;

    //change col/transparency
    Material _material;
    private float _destroyStepsPercent = 1;

    //notify destruction

    GameManager _gameManager;
    
    public GameManager gameManager { set => _gameManager = value; }
    public GameManager GameManager { get; internal set; }

    private AudioSource _audioSource;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _material = GetComponent<MeshRenderer>().material;

        _audioSource = GetComponent<AudioSource>();

        if (randomHitcount)
        {
            maxHitCount = Random.Range(1, maxHitCount);

        }

        _destroyStepsPercent = 1f/maxHitCount;
    }


    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.GetComponent<MoveBullet>()) return;

        maxHitCount = 1;

        _material.color -= new Color(0, 0, 0, _destroyStepsPercent);

        Debug.Log($"{_destroyStepsPercent} -> alpha = {_material.color.a}");

        if (maxHitCount <= 0)
        {
            _gameManager.DidDestroyWall();

            if (_audioSource.clip)
            {
                _audioSource.Play();
                Invoke(nameof(DestroyMe), _audioSource.clip.length); //play audio before destroying
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void DestroyMe()
    {
        _gameManager = null;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
