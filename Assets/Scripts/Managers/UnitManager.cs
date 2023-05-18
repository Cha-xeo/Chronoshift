using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    private List<ScriptableUnit> _units;

    public BaseChar SelectedChar;
    public BaseEnemy SelectedEnemy;
    
    void Awake() {
        Instance = this;

        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
    }

    public void SpawnCharacter() {
        var charCount = 1;

        for (int i = 0; i < charCount; i++) {
            var randomPrefab = GetRandomUnit<BaseChar>(Faction.Character);
            var spawnedChar = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetCharSpawnTile();

            randomSpawnTile.SetUnit(spawnedChar);
        }

        // GameManager.Instance.ChangeState(GameState.SpawnEnemy);
       GameManager.Instance.ChangeState(GameState.CharTurn);
    }

    public void SpawnUnitAt(BaseChar charr, Vector2 pos) 
    {
            BaseChar spawnedChar = Instantiate(charr);
            Tile tile = GridManager.Instance.GetTileAtPos(pos);
            tile.SetUnit(spawnedChar);
    }

    public void SpawnEnemies() {
        var enemyCount = 1;

        for (int i = 0; i < enemyCount; i++) {
            var randomPrefab = GetRandomUnit<BaseEnemy>(Faction.Enemy);
            var spawnedEnemy = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetEnemySpawnTile();

            randomSpawnTile.SetUnit(spawnedEnemy);
        }

       GameManager.Instance.ChangeState(GameState.CharTurn);
    }

    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit {
        return (T)_units.Where(u=>u.Faction == faction).OrderBy(o=>Random.value).First().UnitPrefab;
    }

    public void SetSelectedChar(BaseChar charac) {
        SelectedChar = charac;
    }
    
    public void SetSelectedEnemy(BaseEnemy enemy) {
        SelectedEnemy = enemy;
    }
}
