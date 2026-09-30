using System.Collections;
using UnityEngine;
using RandMath;
using System;
using System.IO;

public class TreeSpawner : Spawner
{
    [SerializeField, Min(1f)] private float _range = 1f;

    private readonly TreeGenerator _generator = new();
    private Coroutine _spawning;
    private int _spawnCount = 0;

    private readonly Upgradable<int> _maxSpawnCount = new(3);
    private readonly Upgradable<float> _delay = new(2f);
    private readonly Upgradable<RandomValue<TreeSettings>> _treePool = new(null);

    public Upgradable<int> MaxSpawnCount => _maxSpawnCount;

    public Upgradable<float> Delay => _delay;

    public Upgradable<RandomValue<TreeSettings>> TreePool => _treePool;

    public event Action OnSpawningStart;
    public event Action OnSpawningStop;

    public GameObject Spawn(TreeSettings settings)
    {
        GameObject tree = _generator.Spawn(settings != null ? settings : _treePool.GetValue());

        tree.transform.parent = transform;

        tree.GetComponent<Health>().OnPointsOver += () => _spawnCount--;
        _spawnCount++;

        return tree;
    }

    public GameObject Spawn(TreeSettings settings, Vector3 position)
    {
        GameObject tree = Spawn(settings);

        tree.transform.position = Ground.GetPosition(position) ?? position;

        return tree;
    }

    public GameObject Spawn(TreeSettings settings, float range)
    {
        GameObject tree = Spawn(settings);

        Vector3 position = GetClearPosition(
                () => Rand.GetPosition(range) + transform.position,
                tree.TryGetComponent(out OccupyingArea area) ? area.Range : 0f
            );
        tree.transform.position = Ground.GetPosition(position) ?? position;

        return tree;
    }

    private void SetBiome()
    {
        TreeSpawnerSettings settings = Context.Game.Storage.GetResource<TreeSpawnerSettings>();

        _treePool.BaseValue = settings.TreePool;
        _maxSpawnCount.BaseValue = settings.MaxSpawnCount;
        _delay.BaseValue = settings.Delay;
    }

    private IEnumerator Spawning()
    {
        yield return new WaitForSeconds(_delay.GetValue());
        while (true)
        {
            yield return new WaitUntil(() => _spawnCount < _maxSpawnCount.GetValue());
            yield return new WaitForSeconds(_delay.GetValue());

            Spawn(null, _range);
        }
    }

    private void Start()
    {
        if (!Context.Exist() || !Context.Game.IsPlaying())
            throw new MissingGameException();
        SetBiome();
    }

    private void OnEnable()
    {
        OnSpawningStart?.Invoke();

        _spawning = StartCoroutine(Spawning());
    }

    private void OnDisable()
    {
        if (_spawning != null)
        {
            StopCoroutine(_spawning);
            _spawning = null;

            OnSpawningStop?.Invoke();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 1f, 0f, 0.2f);
        Gizmos.DrawSphere(transform.position, _range);
    }
}
