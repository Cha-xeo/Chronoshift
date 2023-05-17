using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Chronoshift.UI
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] GameObject _hud;
        [SerializeField] GameObject _select;

        private void Awake()
        {
            _hud.SetActive(false);
            _select.SetActive(true);
        }
    }
}
