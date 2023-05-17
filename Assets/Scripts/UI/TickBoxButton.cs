using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Chronoshift.UI
{
    public class TickBoxButton : MonoBehaviour
    {
        [SerializeField] Sprite _spriteOn, _spriteOff;
        [SerializeField] Image _image;
        bool _status;

        public void changeStatus()
        {
            _status = !_status;
            switch (_status)
            {
                case false:
                    break;
                case true:
                    break;
            }
        }

    }
}
