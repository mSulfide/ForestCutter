using UnityEngine;
using TMPro;

public class SaveManagerInterface : MonoBehaviour
{
    [SerializeField] private TMP_InputField _input;

    public void StartGame()
    {
        Context.Game.StartGame(_input.text);
    }

    public void DeleteGame()
    {
        Context.Saves.DeleteSave(_input.text);
    }
}
