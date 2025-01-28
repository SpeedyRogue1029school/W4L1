using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private GameObject _gameOverScreen;
    //[SerializeField] private Player _player;

/*
    private void Start()
    {
        _player.PointsChanged += HandlePointsChanged;
    }
*/

    public void HandlePointsChanged(int points)
    {
        _text.text = points.ToString();
    }

    public void GameStart ()
    {
        _gameOverScreen.SetActive(false);
    }

    public void GameOver ()
    {
        _gameOverScreen.SetActive(true);
    }
}
