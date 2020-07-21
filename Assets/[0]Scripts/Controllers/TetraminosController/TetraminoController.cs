using UnityEngine;

public class TetraminoController
{
    private const int HEIGHT = 20;
    private const int WIDTH = 10;
    private const float X_POS = ((float) WIDTH / 2) - 1;
    private const float Y_POS = HEIGHT;
    private const float Z_POS = 0.0f;

    private static readonly Vector3 _spawnPosition = new Vector3(X_POS, Y_POS, Z_POS);
    
    private static Cube[,] _grid;

    private TetraminoSpawner _spawner;
    private TetraminoLineChecker _lineChecker;
    
    private PoolManager _poolManager;

    private Transform _chestParent;
    private Transform _cubeParent;


    public static int Height => HEIGHT;
    public static int Width => WIDTH;
    public static Vector3 SpawnPosition => _spawnPosition;
    public static Cube[,] Grid => _grid;
    public static bool IsGameOver { get; set; }
    
    public Transform CubeParent => _cubeParent;


    public TetraminoController()
    {
        _spawner = new TetraminoSpawner();
        _lineChecker = new TetraminoLineChecker();
        _poolManager = InjectBox.Get<PoolManager>();
        _chestParent = GameObject.Find("Chest").transform;
        _grid = new Cube[WIDTH, HEIGHT + 5];

        _cubeParent = GameObject.Find(typeof(Cube) + "s").transform;
        _cubeParent.SetParent(_chestParent);

        EventManager.Subscribe<OnTetraminoFellEvent>(OnTetraminoFell);
    }

    public void SpawnTetramino()
    {
        if(!IsGameOver)_spawner.Spawn();
    }
    
    public void SetGrid(Cube cube)
    {
        var cubePos = cube.transform.position;
        _grid[Mathf.RoundToInt(cubePos.x), Mathf.RoundToInt(cubePos.y)] = cube;
    }
    
    private void OnTetraminoFell(OnTetraminoFellEvent obj)
    {
        _poolManager.GetPool<Tetramino>().Deactivate(obj.Tetramino, this);
        SpawnTetramino();
        _lineChecker.CheckForLines();
    }
}