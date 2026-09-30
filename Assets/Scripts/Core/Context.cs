using UnityEngine;

[DefaultExecutionOrder(-100)]
public class Context : MonoBehaviour
{
    public static Context Instance { get; private set; }

    public static Game Game => Instance.GetComponent<Game>();

    public static Storage Storage => Instance != null ? Instance._storage : null;

    public static Settings Settings => Instance._settings ?? (Instance._settings = new());

    public static Saves Saves => Instance != null ? Instance._saves : null;

    private Storage _storage;
    private Settings _settings;
    private Saves _saves;

    public static bool Exist() => Instance != null;

    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        InitializeServices();
    }

    private void Initialize()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("Multiple Context instances detected. Destroying duplicate.");
            Destroy(gameObject);
        }
    }

    private void InitializeServices()
    {
        _storage = new();
        _settings = new();
        _saves = new();
    }

    private T GetOrAddComponent<T>() where T : Component => TryGetComponent(out T component) ? component : gameObject.AddComponent<T>();
}