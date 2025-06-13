using UnityEngine;

namespace Config
{
    public class DisplayConfig : MonoBehaviour
    {
        private Camera _camera;

        internal void Initialize()
        {
            _camera = Camera.main;
            _camera!.orthographicSize = 11f;
        }
    }
}
