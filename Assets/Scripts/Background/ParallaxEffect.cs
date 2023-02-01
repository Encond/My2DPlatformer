using UnityEngine;

namespace Background
{
    public class ParallaxEffect : MonoBehaviour
    {
        [Header("Camera properties")]
        [SerializeField] private Camera _camera;
        
        [Header("Parallax properties")]
        [SerializeField] private float _parallaxEffect;
        [SerializeField] private float _parallaxSpawnOffset;

        private float _startPositionX;
        private float _lengthX;

        private void Start()
        {
            _startPositionX = transform.position.x;

            Vector3 tempBoundsSize = GetComponent<SpriteRenderer>().bounds.size;
            _lengthX = tempBoundsSize.x;
        }

        private void FixedUpdate()
        {
            Vector3 tempPosition = transform.position;

            float distanceFromStartPositionX = _camera.transform.position.x * _parallaxEffect;
            transform.position = new Vector3(_startPositionX + distanceFromStartPositionX, tempPosition.y, tempPosition.z);
            
            float tempX = (_camera.transform.position.x * (1 - _parallaxEffect));
            if (tempX > _startPositionX + _lengthX / _parallaxSpawnOffset)
                _startPositionX += _lengthX;
            else if (tempX < _startPositionX - _lengthX / _parallaxSpawnOffset)
                _startPositionX -= _lengthX;
        }
    }
}