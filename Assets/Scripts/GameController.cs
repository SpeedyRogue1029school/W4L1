using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class GameController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private UI _ui;
    [SerializeField] private MoveLeft _pipePrefab;
    [SerializeField] private Transform _pipeAnchor;
    [SerializeField] private float _pipeSpawnRate = 2.0f;

    private List<MoveLeft> _environment;
    private float _pipeCountdown;
    private float _halfWorldWidth;

    public Vector3 GetWorldMax () =>
                Camera.main.ScreenToWorldPoint(
                    new Vector3(
                        Screen.width,
                        Screen.height,
                        Camera.main.transform.position.z
                    )
                );

    private Vector3 GetWorldMin () =>
                Camera.main.ScreenToWorldPoint(
                    new Vector3(
                        0,
                        0,
                        -Camera.main.transform.position.z
                    )
                );

    private void Start()
    {
        //_player.PlayerHitPipe += HandlePlayerHitPipe;

        SetupGame();
    }

    private void Update ()
    {
        Vector3 worldMin = GetWorldMin();

        List<MoveLeft> despawnList = new List<MoveLeft>();
        foreach(MoveLeft item in _environment)
        {
            if(item.transform.position.x + item.transform.lossyScale.x < worldMin.x - _halfWorldWidth)
            {
                despawnList.Add(item);
            }
        }

        foreach(MoveLeft despawnItem in despawnList)
        {
            _environment.Remove(despawnItem);
            Despawn(despawnItem);
        }

        _pipeCountdown -= Time.deltaTime;
        if(_pipeCountdown <= 0.0f)
        {
            SpawnPipe();
            _pipeCountdown = _pipeSpawnRate;
        }
    }

    public void HandlePlayerHitPipe()
    {
        GameOver();
    }

    public void RestartButton ()
    {
        SetupGame();
    }

    private void SetupGame()
    {
        _halfWorldWidth = (GetWorldMax().x - GetWorldMin().x) / 2.0f;

        _player.Setup();
        _player.enabled = true;

        _ui.GameStart();

        DespawnAllEnvironment();
        _environment = new List<MoveLeft>();
        SpawnPipe();

        _pipeCountdown = _pipeSpawnRate;
    }

    private void SpawnPipe ()
    {
        MoveLeft pipe = Instantiate(_pipePrefab, _pipeAnchor) as MoveLeft;
        Assert.IsNotNull(pipe);

        _environment.Add(pipe);

        pipe.SetInitialPosition(_halfWorldWidth);
    }

    private void Despawn (MoveLeft moveLeft)
    {
        Destroy(moveLeft.gameObject);
    }

    private void GameOver()
    {
        _ui.GameOver();

        _player.enabled = false;

        DespawnAllEnvironment();
    }

    private void DespawnAllEnvironment ()
    {
        if(_environment == null || _environment.Count == 0) return;

        foreach(MoveLeft despawnItem in _environment)
        {
            Despawn(despawnItem);
        }
        _environment.Clear();
    }
}
