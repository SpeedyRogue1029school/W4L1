using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _jumpAudio;
    [SerializeField] private AudioSource _pointAudio;
    [SerializeField] private AudioSource _hurtAudio;
    //[SerializeField] private Player _player;

/*
    private void Start()
    {
        _player.PlayerHitPipe += HandlePlayerHitPipe;
        _player.PointsChanged += HandlePlayerEarnedPoint;
        _player.PlayerJumped += HandlePlayerJumped;
    }
*/

    public void HandlePlayerJumped ()
    {
        _jumpAudio.Play();
    }

    public void HandlePlayerEarnedPoint (int points)
    {
        _pointAudio.Play();
    }

    public void HandlePlayerHitPipe ()
    {
        _hurtAudio.Play();
    }
}
