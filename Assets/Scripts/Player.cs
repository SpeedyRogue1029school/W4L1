using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 1.0f;
    [SerializeField] private Transform _target;
    [SerializeField] private Rigidbody2D _physics;

    [SerializeField] private UI _ui;
    [SerializeField] private GameController _gameController;
    [SerializeField] private AudioController _audioController;

/*
    public delegate void IntDelegate(int x);
    public delegate void EmptyDelegate();

    public event IntDelegate PointsChanged;
    public event EmptyDelegate PlayerHitPipe;
    public event EmptyDelegate PlayerJumped;
*/

    private int _points;
    private int _highScore;

    public void Setup()
    {
        _target.position = Vector3.zero;

        _points = 0;

        _ui.HandlePointsChanged(_points);
        
        //PointsChanged?.Invoke(_points);
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            _physics.velocity = Vector2.zero;
            _physics.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

            _audioController.HandlePlayerJumped();

            //PlayerJumped?.Invoke();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Kill"))
        {
            _gameController.HandlePlayerHitPipe();
            _audioController.HandlePlayerHitPipe();

            //PlayerHitPipe?.Invoke();
        }
        else if(collider.gameObject.CompareTag("Point"))
        {
            _points++;

            _ui.HandlePointsChanged(_points);
            _audioController.HandlePlayerEarnedPoint(_points);

            //PointsChanged?.Invoke(_points);
        }
    }

}
