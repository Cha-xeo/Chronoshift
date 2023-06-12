using Chronoshift.Tiles;
using Photon.Pun;
using Photon.Pun.Demo.Procedural;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Grid = Chronoshift.Tiles.Grid;
using System.Linq;
using Photon.Pun.Demo.PunBasics;
using Chronoshift.PlayerController;

namespace Chronoshift.Managers
{
    public class GridGenerator : MonoBehaviour
    {
        public readonly string SeedPropertiesKey = "Seed";
        public readonly string WorldHeightPropertiesKey = "WorldHeight";
        public readonly string WorldWidthPropertiesKey = "WorldWidth";
        private static GridGenerator instance;

        public static GridGenerator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<GridGenerator>();
                }

                return instance;
            }
        }

        [SerializeField] float scaled = 1;
        bool odd = false;
        float _driftX = 0.759f;
        float _driftY = 0.571f;
        int orderlayer;
        int _tileId = 0;
        //public int Seed;
        public Transform World;
        [SerializeField] private int _width, _height;

        [SerializeField] List<Tile> _tiles;
        public Dictionary<Constants.Elements, Tile> TilesDico = new();

        private void Awake()
        {
            TilesDico = _tiles.ToDictionary(x => x.Elements, x => x);
        }

        public void CreateWorld()
        {
            StopAllCoroutines();
            DestroyWorld();
            StartCoroutine(GenerateWorld());
        }
        private void DestroyWorld()
        {
            foreach(GameObject b in World)
            {
                Debug.Log("Destroy " + b.name);
            }
            Grid.Instance.DestroyGrid();
        }

        public void ConfirmAndUpdateProperties()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                return;
            }

            Hashtable properties = new Hashtable
            {
                {WorldHeightPropertiesKey, _height},
                {WorldWidthPropertiesKey, _width},
            };

            PhotonNetwork.CurrentRoom.SetCustomProperties(properties);
        }

        private IEnumerator GenerateWorld()
        {
            //Simplex.Noise.Seed = Seed;
            for (float y = 0; y < _height; y += 1, odd = !odd, orderlayer--)
            {
                for (float x = y; x < _width + y; x += 1)
                {
                    float noiseValue = Simplex.Noise.CalcPixel2D((int)x, (int)y, 0.02f);

                    Tile randomTile = Instantiate(TilesDico[Constants.Elements.None], new Vector3(odd ? (
                        (x * _driftX) + _driftY) * scaled - _driftY * y
                        : ((x * _driftX) + _driftY) * scaled - _driftY * y
                        , y * (_driftY * scaled)), Quaternion.identity);
                    randomTile.Init(_tileId, orderlayer);
                    Grid.Instance.AddTile(_tileId++, randomTile);
                }
                yield return new WaitForEndOfFrame();
            }
            int pos = Grid.Instance.Tiles.Count / 2 + _width / 2;
            Vector3 aled = new Vector3(Grid.Instance.Tiles[pos].transform.position.x, Grid.Instance.Tiles[pos].transform.position.y, -10);
            Camera.main.transform.position = aled;
            // Applying modifications made to the world when joining the room later or while it is created
            /*foreach (DictionaryEntry entry in PhotonNetwork.CurrentRoom.CustomProperties)
            {
                Debug.Log(entry.Key + " " + entry.Value);
            }
            foreach (DictionaryEntry entry in PhotonNetwork.CurrentRoom.CustomProperties)
            {
                if (entry.Value == null)
                {
                    continue;
                }
                string key = entry.Key.ToString();

                if ((key == SeedPropertiesKey) || (key == WorldHeightPropertiesKey) || (key == WorldWidthPropertiesKey))
                {
                    continue;
                }

                // ¯\_(ツ)_/¯

                int indexOfBlank = key.IndexOf(' ');
                key = key.Substring(indexOfBlank + 1, (key.Length - (indexOfBlank + 1)));

                if (Grid.Instance != null)
                {
                    Debug.Log(key);
                    Debug.Log(entry.Value);
                    Dictionary<int, Constants.TileElements> x = (Dictionary<int, Constants.TileElements>)entry.Value;
                    foreach (KeyValuePair<int, Constants.TileElements> pair in x)
                    {
                        Grid.Instance.SetTileRemote(pair.Key, pair.Value);
                    }
                }
            }*/
            PlayerNController.Instance.GenerateLight();
        }


    }
}
