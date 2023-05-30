using Chronoshift.PlayerController;
using Photon.Pun;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Chronoshift
{
    public class ChargedElement : MonoBehaviour
    {
        public static ChargedElement Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public Constants.Elements CurrentElement;
        public List<SpellsScriptable> SpellArray;
        [SerializeField] Transform _spellTransform;
        public GameObject HoldingSpell;
        //public bool holding;
        public bool canCast;
        //public Vector2 lastPos;
        public int LastTileID;

        private void Update()
        {
            //if (!PlayerNController.Instance.PlayerView.IsMine || PlayerNController.Instance.mode != Mode.Casting || !canCast) return;
            /*if (InputManager.GetInstance().GetLeftMousePressed())
            {
                HoldingSpell.GetComponent<Chronoshift.Spells.Spells>().Use();
                PlayerNController.Instance.mode = Mode.Move;
                //holding = false;
                PhotonNetwork.Destroy(HoldingSpell);
                //_holdingSpell = null;
                PlayerNController.Instance.mana--;
            }
            else */if (PlayerNController.Instance.IsPlaying && PlayerNController.Instance.mode == Mode.Casting && InputManager.GetInstance().GetRightMousePressed())
            {
                Debug.Log("Stop Casting");
                PlayerNController.Instance.mode = Mode.Move;
                //holding = false;
                //_holdingSpell = null;
                PhotonNetwork.Destroy(HoldingSpell);
            }
        }

        public void HoldSpell(int i)
        {
            PlayerNController.Instance.mode = Mode.Casting;
            HoldingSpell = PhotonNetwork.Instantiate("Photon/SpellsPrefab/" + SpellArray[i]._name, _spellTransform.position, Quaternion.identity);
        }
    }
}