using TMPro;
using UnityEngine;

public class GameVersion : MonoBehaviour
{
    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text = $"Build Version: v{Application.version}";
    }
}