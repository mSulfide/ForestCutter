using System;
using UnityEngine;

public class Fader : MonoBehaviour
{
    private static readonly int FadedHash = Animator.StringToHash("faded");

    [SerializeField] private Animator _animator;

    private Action _fadedInCallback;
    private Action _fadedOutCallback;

    public bool IsFading { get; private set; }

    public void FadeIn(Action callback)
    {
        if (IsFading)
            return;

        IsFading = true;
        _fadedInCallback = callback;
        _animator.SetBool(FadedHash, true);
    }

    public void FadeOut(Action callback)
    {
        if (IsFading)
            return;

        IsFading = true;
        _fadedOutCallback = callback;
        _animator.SetBool(FadedHash, false);
    }

    private void Handle_FadeInAnimationOver()
    {
        _fadedInCallback?.Invoke();
        _fadedInCallback = null;
        IsFading = false;
    }

    private void Handle_FadeOutAnimationOver()
    {
        _fadedOutCallback?.Invoke();
        _fadedOutCallback = null;
        IsFading = false;
    }
}
