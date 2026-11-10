using System.Collections;
using UnityEngine;
using RandMath;
using System;
using System.Collections.Generic;

public class TreeSpawner : Spawner
{
    [SerializeField, Min(1f)] private float _range = 1f;

    private readonly List<GameObject> _trees = new();
    private readonly TreeGenerator _generator = new();
    private Coroutine _spawning;
    private int _spawnCount = 0;

    private readonly Upgradable<int> _maxSpawnCount = new(3);
    private readonly Upgradable<float> _delay = new(1f);
    private readonly Upgradable<RandomValue<TreeSettings>> _treePool = new(null);

    public List<GameObject> Trees => _trees;
    public Upgradable<int> MaxSpawnCount => _maxSpawnCount;

    public Upgradable<float> Delay => _delay;

    public Upgradable<RandomValue<TreeSettings>> TreePool => _treePool;

    public event Action<GameObject> OnSpawn;
    public event Action<GameObject> OnDespawn;

    public GameObject Spawn(TreeSettings settings)
    {
        GameObject tree = _generator.Spawn(settings != null ? settings : _treePool.GetValue());

        tree.transform.parent = transform;

        if (tree.TryGetComponent(out SpawnedEntity entity))
            entity.Initialize(() =>
            {
                _trees.Remove(tree);
                _spawnCount--;

                OnDespawn?.Invoke(tree);
            });
        _trees.Add(tree);
        _spawnCount++;

        OnSpawn?.Invoke(tree);

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
        while (true)
        {
            yield return new WaitForSeconds(_delay.GetValue());
            yield return new WaitUntil(() => _spawnCount < _maxSpawnCount.GetValue());

            GameObject tree = Spawn(null, _range);
            if (tree.TryGetComponent(out Animator animator))
                animator.Play("GrownUp");
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
        _spawning = StartCoroutine(Spawning());
    }

    private void OnDisable()
    {
        if (_spawning != null)
        {
            StopCoroutine(_spawning);
            _spawning = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 1f, 0f, 0.2f);
        Gizmos.DrawSphere(transform.position, _range);
    }
}
