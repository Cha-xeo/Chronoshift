using Chronoshift.PlayerController;
using Grid = Chronoshift.Tiles.Grid;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Chronoshift.Spells;
using Photon.Pun;

namespace Chronoshift.Spells
{
    public class Fire1 : Spells
    {
        [SerializeField] GameObject _torchePrefab;
        Light2D Torch;
        public int MaxTurn = 3;
        public override void Use(int tileID)
        {
            base.Use(tileID);

        }
        public override void ChronoUse(int tileID)
        {
            base.ChronoUse(tileID);
            SpellsNetwork.Instance.TorchList.Add(new SpellData(Instantiate(_torchePrefab, Grid.Instance.Tiles[tileID].transform.position, Quaternion.identity), MaxTurn));
        }
    }
}