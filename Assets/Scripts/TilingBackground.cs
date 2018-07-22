using UnityEngine;
using System.Collections;

#pragma warning disable 649

namespace Zenject.Asteroids
{

    public class TilingBackground : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private OffsetDirection _direction;

        Vector2 _offset;
        Renderer _renderer;

        void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        void Update()
        {
            switch (_direction)
            {
                case OffsetDirection.Right:
                    _offset.x += _speed * Time.deltaTime;
                    break;
                case OffsetDirection.Left:
                    _offset.x -= _speed * Time.deltaTime;
                    break;
                case OffsetDirection.Up:
                    _offset.y += _speed * Time.deltaTime;
                    break;
                case OffsetDirection.Down:
                    _offset.y -= _speed * Time.deltaTime;
                    break;
            }
            _renderer.material.mainTextureOffset = _offset;
        }

        public enum OffsetDirection { Right, Left, Up, Down }
    }
}
