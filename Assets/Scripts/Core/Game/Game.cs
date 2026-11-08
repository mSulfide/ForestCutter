using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private readonly GameStorage _storage = new();
    private GameState _state;

    public GameState State => _state;

    public GameStorage Storage => _storage;

    public event Action OnStart;
    public event Action OnStop;
    public event Action OnStartLoadScene;
    public event Action OnEndLoadScene;

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

        OnStart?.Invoke();
    }

    public void ExitGame()
    {
        _state = null;

        OnStop?.Invoke();

        LoadScene(EScene.MainMenu);
    }

    public void WinGame()
    {
        if (!_state.Victory)
        {
            _state.Victory = true;
            Debug.Log("Victory!");
        }
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
        StartCoroutine(LoadScene(scene.ToString()));
    }

    private IEnumerator LoadScene(string name)
    {
        Fader fader = GetComponentInChildren<Fader>();
        if (fader == null)
        {
            OnStartLoadScene?.Invoke();

            AsyncOperation loadScene = SceneManager.LoadSceneAsync(name);
            yield return new WaitUntil(() => loadScene.isDone);

            OnEndLoadScene?.Invoke();
        }
        else
        {
            OnStartLoadScene?.Invoke();

            bool waitFading = false;
            fader.FadeIn(() => waitFading = true);

            yield return new WaitUntil(() => waitFading);

            AsyncOperation loadScene = SceneManager.LoadSceneAsync(name);
            yield return new WaitUntil(() => loadScene.isDone);

            waitFading = false;
            fader.FadeOut(() => waitFading = true);

            yield return new WaitUntil(() => waitFading);

            OnEndLoadScene?.Invoke();
        }
        yield break;
    }
}