using System;
using UnityEngine;
using Photon.Pun;

namespace Chronoshift.PhotonNetworking
{
    public class SpawnPLayers : MonoBehaviour
    {
        [SerializeField] Vector2 _playerPos;
        [SerializeField] GameObject _playerPrefab;

        private void Start()
        {
            _playerPos = Vector2.left;
            PhotonNetwork.Instantiate(_playerPrefab.name, _playerPos, Quaternion.identity);
        }
    }
}
