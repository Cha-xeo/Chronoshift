using UnityEngine;
using Photon.Pun;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance { get; private set; }

    //private List<ScriptableUnit> _units;
    [SerializeField] GameObject _playerPrefab;
    [SerializeField] Transform _playerParent;
    public GameObject _player;
    [SerializeField] PhotonView _view;
    public BaseChar SelectedChar;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnCharacter()
    {
        /*var randomPrefab = GetRandomUnit<BaseChar>(Faction.Character);
        var spawnedChar = Instantiate(randomPrefab);*/
        _player = PhotonNetwork.Instantiate(_playerPrefab.name, Vector3.zero, Quaternion.identity);
        
        _player.transform.parent = _playerParent;
        Tile randomSpawnTile = GridManager.Instance.GetCharSpawnTile();
        randomSpawnTile.SetUnit(_player.GetComponent<BaseUnit>());
        //if (PhotonNetwork.IsMasterClient)
          //  _view.RPC("RPC_AskForSpawn", RpcTarget.All);
        GameManager.Instance.ChangeState(GameState.CharTurn);
    }
    [PunRPC]
    public void RPC_AskForSpawn()
    {
    }
    /*private T GetRandomUnit<T>(Faction faction) where T : BaseUnit {
        return (T)_units.Where(u=>u.Faction == faction).OrderBy(o=>Random.value).First().UnitPrefab;
    }*/

    public void SetSelectedChar(BaseChar charac) {
        SelectedChar = charac;
    }
}
