using System.Collections;
using System.Linq;
using UnityEngine;

public class GlobalSaver : MonoBehaviour
{
    private const float delay = 30f;

    [SerializeField] private bool _enableAutoSave = true;

    private Coroutine _coroutine;

    public void Save()
    {
        if (Context.Exist() && Context.Storage != null)
            foreach (ISaver saver in FindObjectsByType<MonoBehaviour>().OfType<ISaver>())
            {
                saver.Save();
            }
    }

    private IEnumerator AutoSave()
    {
        while (_enableAutoSave)
        {
            yield return new WaitForSeconds(delay);
            Save();
        }
    }

    private void OnEnable()
    {
        _coroutine = StartCoroutine(AutoSave());
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}