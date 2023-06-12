using Chronoshift.Spells;
using Grid = Chronoshift.Tiles.Grid;
using Photon.Pun;
using UnityEngine;

namespace Chronoshift.Spells
{
    public class Plant1 : Spells
    {
        public int MaxTurn = 3;
        [SerializeField]
        BaseChar _unit;

        public override void Use(int tileID)
        {
            base.Use(tileID);

        }
        public override void ChronoUse(int tileID)
        {
            base.ChronoUse(tileID);
            SpellsNetwork.Instance.PlantList.Add(new SpellData(PhotonNetwork.Instantiate("Photon/Plant", Grid.Instance.Tiles[tileID].transform.position, Quaternion.identity), MaxTurn));
        }
    }
}