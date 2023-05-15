using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;
using Chronoshift.AplicationController;
using UnityEngine.UIElements;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }
    [SerializeField] private Tile _tileDefault, _obstacleTile;
    [SerializeField] private Transform _camera;
    [SerializeField] Transform _world;
    [SerializeField] private float scaled = 1;

    private Dictionary<Vector2, Tile> _tiles = new Dictionary<Vector2, Tile>();
    Vector3 _pos = new Vector3();
    private bool odd=true;
    private float _driftX = 0.759f;
    private float _driftY = 0.569f;
    [SerializeField] PhotonView _view;
    [SerializeField] float _baseLineWidth, _baseWidthGrowth, _width, _height;

    

    void Awake() {
        // GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // private void OnDestroy() {
    //     GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    // }

    // private void GameManagerOnGameStateChanged(GameState state) {
    //     GenerateGrid();
    // }

    public void GenerateGrid()
    {
        // GameManager.Instance.ChangeState(GameState.SpawnChar);
        //-----------------------------------------Code Grille 2d Cubes ^ --------------------------------------------------------------
        for (float y = 0; y < _height; y += _driftY * scaled, odd = !odd)
        {
            for (float x = 0; x < _width; x += _driftX * scaled)
            {
                //var randomTile = Instantiate(Random.Range(0, 6) != 3 ? _tileDefault : _obstacleTile, new Vector3(odd ? x + 0.379f*scaled : x, y), Quaternion.identity, _world);
                //if (!AplicationController.Instance.isClient)
                //{
                    GameObject randomTile = PhotonNetwork.InstantiateRoomObject(Random.Range(0, 6) != 3 ? _tileDefault.name : _obstacleTile.name, new Vector3(odd ? x + 0.379f * scaled : x, y), Quaternion.identity);
                    //_view.RPC("RPC_SetWorld", RpcTarget.All, randomTile, x, y);

                    // randomTile.Init(x,y);

                // _spawnedTile.positon = _spawnedTile.transform.position;
                // _tiles[_spawnedTile.positon] = _spawnedTile;
                //}
            }
        }
        _view.RPC("RPC_SetCam", RpcTarget.All, (float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f);
        GameManager.Instance.ChangeState(GameState.SpawnChar);
        return;
    }

    [PunRPC]
    public void RPC_SetCam(float x, float y)
    {
        _camera.transform.position = new Vector3(x, y, -1);
    }

    [PunRPC]
    public void RPC_SetWorld(GameObject randomTile, float x, float y)
    {
        randomTile.transform.parent = _world;
        randomTile.name = $"Tile {x - _driftX * scaled};{y - _driftY * scaled}";
        _tiles[new Vector2(x, y)] = randomTile.GetComponent<Tile>();
    }
    public Tile GetCharSpawnTile()
    {
        return _tiles.Where(t=>t.Key.x < _width/2 && t.Value.Walkable).OrderBy(t=>Random.value).First().Value;
    }

    // public Tile GetEnemySpawnTile() {
    //     return _tiles.Where(t=>t.Key.x > _width/2 && t.Value.Walkable).OrderBy(y=>RandomValue).First().Value;
    // }

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
