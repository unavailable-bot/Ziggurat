using UnityEngine;

namespace Config
{
    public class DisplayConfig : MonoBehaviour
    {
        private Camera _camera;
        
        private void Start()
        {
            _camera = Camera.main;
            _camera!.orthographicSize = 11f;
        }
    }
}
