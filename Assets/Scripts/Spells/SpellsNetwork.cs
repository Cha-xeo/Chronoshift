using Grid = Chronoshift.Tiles.Grid;
using System.Collections.Generic;
using UnityEngine;

namespace Chronoshift.Spells
{
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
        public List<GameObject> TorchList = new();
        public List<GameObject> PlantList = new();
        public string[] SpellsPrefabName;


        public void RewindEnd()
        {
            foreach(GameObject obj in TorchList)
            {
                if (!obj.GetComponent<Fire1>().Turn()) Destroy(obj);
            }
            foreach (GameObject obj in TorchList)
            {
                Debug.Log(obj.name);
            }
            foreach(GameObject obj in PlantList)
            {
                if (!obj.GetComponent<Plant1>().Turn()) Destroy(obj);
            }
            foreach (GameObject obj in PlantList)
            {
                Debug.Log(obj.name);
            }
        }
    }
}
