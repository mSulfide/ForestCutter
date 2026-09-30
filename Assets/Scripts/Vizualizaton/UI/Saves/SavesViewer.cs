using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SavesViewer : MonoBehaviour
{
    [SerializeField] private SaveShower _savePrefab;
    [SerializeField] private TMP_InputField _input;

    private readonly List<SaveShower> _showers = new();
    public void ViewAllSaves()
    {
        foreach (GameObject shower in _showers.Select(x => x.gameObject))
            Destroy(shower);
        _showers.Clear();

        foreach (SaveData data in Context.Saves.GetAllSaves())
        {
            if (data.Name.Length == 0)
                continue;

            SaveShower shower = Instantiate(_savePrefab, transform);

            shower.ShowSave(data);
            shower.GetComponent<Button>().onClick.AddListener(() => _input.text = data.Name);

            _showers.Add(shower);
        }
    }

    private void Start()
    {
        ViewAllSaves();
    }
}