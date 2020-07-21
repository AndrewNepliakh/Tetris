#undef DEBUG
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetraminoController
{
    private const int HEIGHT = 20;
    private const int WIDTH = 10;
    private const float X_POS = ((float) WIDTH / 2) - 1;
    private const float Y_POS = HEIGHT;
    private const float Z_POS = 0.0f;

    private static Cube[,] _grid;

    private TetraminoSpawner _tetraminoSpawner;
    private PoolManager _poolManager;

    private Transform _chestParent;
    private Transform _cubeParent;

    private static readonly Vector3 _spawnPosition = new Vector3(X_POS, Y_POS, Z_POS);

    public static float Height => HEIGHT;
    public static float Width => WIDTH;
    public static Vector3 SpawnPosition => _spawnPosition;
    public static Cube[,] Grid => _grid;
    public Transform CubeParent => _cubeParent;

    public TetraminoController()
    {
        _tetraminoSpawner = new TetraminoSpawner();
        _poolManager = InjectBox.Get<PoolManager>();
        _chestParent = GameObject.Find("Chest").transform;
        _grid = new Cube[WIDTH, HEIGHT + 5];

        _cubeParent = GameObject.Find(typeof(Cube) + "s").transform;
        _cubeParent.SetParent(_chestParent);

        EventManager.Subscribe<OnTetraminoFellEvent>(OnTetraminoFell);
    }

    public void SpawnTetramino()
    {
        _tetraminoSpawner.Spawn();
    }

    private void OnTetraminoFell(OnTetraminoFellEvent obj)
    {
        _poolManager.GetPool<Tetramino>().Deactivate(obj.Tetramino, this);
        SpawnTetramino();
        CheckForLines();
#if (DEBUG)
        DrawGrid();
#endif
    }

    public void SetGrid(Cube cube)
    {
        var cubePos = cube.transform.position;
        _grid[Mathf.RoundToInt(cubePos.x), Mathf.RoundToInt(cubePos.y)] = cube;
    }

    private void CheckForLines()
    {
        for (int i = HEIGHT - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    private bool HasLine(int i)
    {
        for (int j = 0; j < WIDTH; j++)
        {
            if (_grid[j, i] == null) return false;
        }

        return true;
    }

    private void DeleteLine(int i)
    {
        for (int j = 0; j < WIDTH; j++)
        {
            _poolManager.GetPool<Cube>().Deactivate(_grid[j,i]);
            EventManager.TriggerEvent<OnScoreGainedEvent>();
            _grid[j, i] = null;
        }
    }

    private void RowDown(int i)
    {
        for (int k = i; k < HEIGHT; k++)
        {
            for (int j = 0; j < WIDTH; j++)
            {
                if (_grid[j, k] != null)
                {
                    _grid[j, k - 1] = _grid[j, k];
                    _grid[j, k] = null;
                    _grid[j, k - 1].transform.position -= Vector3.up;
                }
            }
        }
    }
    
    private void DrawGrid()
    {
        for (int i = 0; i < WIDTH; i++)
        {
            for (int j = 0; j < HEIGHT; j++)
            {
                if (_grid[i, j])
                    GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = new Vector3(i, j, -1.0f);
            }
        }
    }
}