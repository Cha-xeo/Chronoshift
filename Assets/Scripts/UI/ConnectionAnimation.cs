using UnityEngine;

namespace Chronoshift.Gameplay.UI
{
    public class ConnectionAnimation : MonoBehaviour
    {
        [SerializeField]
        float m_RotationSpeed;

        void Update()
        {
            transform.Rotate(new Vector3(0, 0, m_RotationSpeed * Mathf.PI * Time.deltaTime));
        }
    }
}
