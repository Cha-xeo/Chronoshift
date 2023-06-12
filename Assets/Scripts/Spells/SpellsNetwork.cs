using Grid = Chronoshift.Tiles.Grid;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Photon.Pun;
using UnityEngine.UIElements;
using static Constants;
using System.Linq;

namespace Chronoshift.Spells
{
    public class SpellData
    {
        public GameObject obj;
        public int MaxTurn;
        public int Turn;

        public SpellData(GameObject obj, int MaxTurn)
        {
            this.obj = obj;
            this.MaxTurn = MaxTurn;
            this.Turn = 0;
        }

        public bool TurnUp()
        {
            Turn++;
            return Turn >= MaxTurn;

        }
    }

    public class TileData
    {
        public GameObject obj;
        public int TileId;

        public TileData(GameObject obj, int TileId)
        {
            this.obj = obj;
            this.TileId = TileId;
        }
    }

    public class SpellsNetwork : MonoBehaviour
    {
        private static SpellsNetwork instance;

        public static SpellsNetwork Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<SpellsNetwork>();
                }

                return instance;
            }
        }
        public List<SpellData> TorchList = new();
        public List<SpellData> PlantList = new();
        public List<TileData> FlagList = new();
        [Header("Same order as element or wont work")]
        [SerializeField] BaseChar[] _flagArray; 
        [SerializeField] PhotonView _view;
        BaseChar flag;
        public int length = 0;

        public void RewindEnd()
        {
            /*foreach (SpellData data in TorchList)
            {
                if (data.TurnUp())
                {
                    Destroy(data.obj);
                    TorchList.Remove(data);
                }
            }
            foreach (SpellData data in PlantList)
            {
                if (data.TurnUp())
                {
                    PhotonNetwork.Destroy(data.obj);
                    PlantList.Remove(data);
                }
            }*/
            //length = TorchList.Count;
            //Debug.Log("TorchList lenght: " + length);
            for (int i = 0; i < TorchList.Count; i++)
            {
                Debug.Log(i);
                if (TorchList[i].TurnUp())
                {
                    Destroy(TorchList[i].obj);
                    TorchList.RemoveAt(i);
                    //i--;
                }
            }
            //length = PlantList.Count;
            for (int i = 0; i < PlantList.Count; i++)
            {
                if (PlantList[i].TurnUp())
                {
                    PhotonNetwork.Destroy(PlantList[i].obj);
                    PlantList.RemoveAt(i);
                    //i--;
                }
            }
        }

        public void OccupieTile(int tileID, Constants.Elements elem)
        {
            Debug.Log("Tile added: " + tileID);
            FlagList.Add(new TileData(PhotonNetwork.Instantiate("Photon/Flag/" + _flagArray[(int)elem].gameObject.name, Grid.Instance.Tiles[tileID].transform.position, Quaternion.identity), tileID));
            foreach (var item in FlagList)
            {
                Debug.Log("FlagList: " + item.TileId);
            }
            _view.RPC("RPC_OccupieTile", RpcTarget.AllViaServer, tileID, elem);
        }
        [PunRPC]
        void RPC_OccupieTile(int tileID, Constants.Elements elem)
        {
            Grid.Instance.Tiles[tileID].OccupiedUnit = _flagArray[(int)elem];
        }
        public void UnoOccupieTile(int tileID)
        {
            //length = FlagList.Count;
            Debug.Log("Aled: " + FlagList.Last().obj.name);
            PhotonNetwork.Destroy(FlagList.Last().obj);
            FlagList.Remove(FlagList.Last());
            /*for (int i = 0; i < length; i++)
            {
                Debug.Log(i);
                if (FlagList[i].TileId == tileID)
                {
                    PhotonNetwork.Destroy(FlagList[i].obj);
                    FlagList.RemoveAt(i);
                }
            }*/
            _view.RPC("RPC_UnoOccupieTile", RpcTarget.AllViaServer, tileID);
        }



        [PunRPC]
        void RPC_UnoOccupieTile(int tileID)
        {
            Grid.Instance.Tiles[tileID].OccupiedUnit = null;
        }
    }
}
