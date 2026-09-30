using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private readonly GameStorage _storage = new();
    private GameState _state;

    public GameState State => _state;

    public GameStorage Storage => _storage;

    public void StartGame(string name, EGameLevel? startLevel = null)
    {
        if (IsPlaying())
            throw new InvalidOperationException("Game is already started!");

        _state = new()
        {
            Name = name
        };
        if (startLevel.HasValue)
            _state.Level = startLevel.Value;

        _storage.SetState(_state);

        LoadScene(EScene.MainScene);
    }

    public void ExitGame()
    {
        _state = null;

        LoadScene(EScene.MainMenu);
    }

    public bool IsPlaying() => _state != null;

    public void LoadLevel(EGameLevel level)
    {
        if (_state.Level != level)
        {
            _state.Level = level;

            LoadScene(EScene.MainScene);
        }
    }

    public void LoadScene(EScene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}