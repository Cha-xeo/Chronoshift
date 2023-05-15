using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tileDefault, _obstacleTile;
    [SerializeField] private Transform _camera;
    [SerializeField] private float scaled = 1;

    private Dictionary<Vector2, Tile> _tiles;
    private bool odd=false;
    private float _driftX = 0.759f;
    private float _driftY = 0.571f;
    private float _originY = 0;

    void Awake() {
        // GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        Instance = this;
    }

    // private void OnDestroy() {
    //     GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    // }

    // private void GameManagerOnGameStateChanged(GameState state) {
    //     GenerateGrid();
    // }

    public void GenerateGrid() {
        // _tiles = new Dictionary<Vector2, Tile>();
        // for (int x = 0; x < _width; x++) {
        //     for(int y = 0; y < _height; y++) {
        //         var randomTile = Random.Range(0,6) != 3 ? _tileDefault : _obstacleTile;
        //         var hasSpawned = Instantiate(randomTile, new Vector3(x,y), Quaternion.identity);
        //         hasSpawned.name = $"Tile {x};{y}";

        //         hasSpawned.Init(x,y);

        //         _tiles[new Vector2(x,y)] = hasSpawned;
        //     }
        // }

        // _camera.transform.position = new Vector3((float)_width/2 - 0.5f,(float)_height/2 - 0.5f, -1);

        // GameManager.Instance.ChangeState(GameState.SpawnChar);
        //-----------------------------------------Code Grille 2d Cubes ^ --------------------------------------------------------------
        _tiles = new Dictionary<Vector2, Tile>();
            for (float y = 0; (y) < _height; y += 1, odd = !odd)
            {
                for (float x = y; (x) < _width+y; x+= 1)
                {
                    var randomTile = Instantiate(Random.Range(0, 6) != 3 ? _tileDefault : _obstacleTile, new Vector3(odd ? ((x*_driftX)+_driftY)*scaled - _driftY*(_originY) : ((x*_driftX)+_driftY)*scaled - _driftY*y, (y*(_driftY*scaled))), Quaternion.identity);
                    randomTile.name = $"Tile {x};{y}";

                    // randomTile.Init(x,y);

                    _tiles[new Vector2(x,y)] = randomTile;
                    // _spawnedTile.positon = _spawnedTile.transform.position;
                    // _tiles[_spawnedTile.positon] = _spawnedTile;
                }
                _originY+=1;
            }
            _camera.transform.position = new Vector3((float)_width/2 - 0.5f,(float)_height/2 - 0.5f, -1);
        
            GameManager.Instance.ChangeState(GameState.SpawnChar);
            return;
    }

    public Tile GetCharSpawnTile() {
        return _tiles.Where(t=>t.Key.x < _width/2 && t.Value.Walkable).OrderBy(t=>Random.value).First().Value;
    }

    public Tile GetEnemySpawnTile() {
        return _tiles.Where(t=>t.Key.x > _width/2 && t.Value.Walkable).OrderBy(t=>Random.value).First().Value;
    }

    public Tile GetTileAtPos(Vector2 pos) {
        if (_tiles.TryGetValue(pos, out var tile)) {
            return tile;
        }

        return null;
    }

    // public Tile GetRandomTile() {
    //     if (_tiles.TryGetValue(new Vector2(Random.Range(0,_width),Random.Range(0,_height)), out var tile))
    //         return tile;
    //     return _tiles.TryGetValue(new Vector2(0,0), out var defaul);
    // }
}
