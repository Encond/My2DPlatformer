using UnityEngine;

namespace Background
{
    public class ParallaxEffect : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _parallaxEffect;

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
            if (tempX > _startPositionX + _lengthX / 2)
                _startPositionX += _lengthX;
            else if (tempX < _startPositionX - _lengthX / 2)
                _startPositionX -= _lengthX;
        }
    }
}