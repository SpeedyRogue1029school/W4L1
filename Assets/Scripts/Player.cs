using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 1.0f;
    [SerializeField] private Transform _target;
    [SerializeField] private Rigidbody2D _physics;

    [SerializeField] private GameController _gameController;
    [SerializeField] private AudioController _audioController;

    private int _points;
    private int _highScore;

    public delegate void PointThing(int x);
    public event PointThing PointsChanged;

    public void Setup()
    {
        _target.position = Vector3.zero;

        _points = 0;
        
        // points
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            _physics.velocity = Vector2.zero;
            _physics.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

            _audioController.HandlePlayerJumped();

            // jump
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Kill"))
        {
            _gameController.HandlePlayerHitPipe();
            _audioController.HandlePlayerHitPipe();

            // hit pipe
        }
        else if(collider.gameObject.CompareTag("Point"))
        {
            _points++;
            PointsChanged.Invoke(_points);
        }
    }

}
