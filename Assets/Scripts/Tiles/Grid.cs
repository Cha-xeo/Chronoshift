using Chronoshift.Managers;
using Chronoshift.Spells;
using ExitGames.Client.Photon;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Chronoshift.Tiles
{
    public class Grid : MonoBehaviourPunCallbacks
    {
        private string propertiesKey = "Grid";

        private Dictionary<int, Constants.Elements> propertiesValue = new();

        public Dictionary<int, Tile> Tiles { get; private set; } = new();

        


        private static Grid instance;

        public static Grid Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<Grid>();
                }

                return instance;
            }
        }
       

        public void AddTile(int tileID, Tile tile)
        {
            Tiles.Add(tileID, tile);
        }

        public void DestroyGrid()
        {
            Tiles.Clear();

            if (PhotonNetwork.IsMasterClient)
            {
                RemoveGridFromRoomProperties();
            }
        }

        /// <summary>
        /// Gets called whenever the local client modifies any Block within this Cluster.
        /// The modification will be applied to the Block first before it is published to the Custom Room Properties.
        /// </summary>
        public void SetTileLocal(int tileID, Constants.Elements tileElem)
        {

            Tile tilev = Instantiate(GridGenerator.Instance.TilesDico[tileElem], Tiles[tileID].transform.position, Quaternion.identity);
            tilev.Init(tileID, Tiles[tileID].layer);
            Destroy(Tiles[tileID].gameObject);
            Tiles[tileID] = tilev;

            UpdateRoomProperties(tileID, tileElem);
        }

        /// <summary>
        /// Gets called when a remote client has modified a certain Block within this Cluster.
        /// Called by the WorldGenerator or the Cluster itself after the Custom Room Properties have been updated.
        /// </summary>
        public void SetTileRemote(int tileID, Constants.Elements tileElem)
        {
            Tile tilev = Instantiate(GridGenerator.Instance.TilesDico[tileElem], Tiles[tileID].transform.position, Quaternion.identity);
            tilev.Init(tileID, Tiles[tileID].layer);
            Destroy(Tiles[tileID].gameObject);
            Tiles[tileID] = tilev;
        }


        /// <summary>
        /// Gets called in order to update the Custom Room Properties with the modification made by the local client.
        /// </summary>
        private void UpdateRoomProperties(int tileID, Constants.Elements Elements)
        {
            propertiesValue[tileID] = Elements;

            Hashtable properties = new Hashtable { { propertiesKey, propertiesValue } };
            PhotonNetwork.CurrentRoom.SetCustomProperties(properties);
        }

        /// <summary>
        /// Removes the modifications of this Cluster from the Custom Room Properties.
        /// </summary>
        private void RemoveGridFromRoomProperties()
        {
            Hashtable properties = new Hashtable { { propertiesKey, null } };
            PhotonNetwork.CurrentRoom.SetCustomProperties(properties);
        }


        public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            if (propertiesThatChanged.ContainsKey(propertiesKey))
            {
                if (propertiesThatChanged[propertiesKey] == null)
                {
                    propertiesValue = new();
                    return;
                }

                propertiesValue = (Dictionary<int, Constants.Elements>)propertiesThatChanged[propertiesKey];

                foreach (KeyValuePair<int, Constants.Elements> pair in propertiesValue)
                {
                    SetTileRemote(pair.Key, pair.Value);
                }
            }
        }
    }
}
