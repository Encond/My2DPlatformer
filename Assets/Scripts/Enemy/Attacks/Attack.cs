using UnityEngine;

namespace Enemy.Attacks
{
    public class Attack : MonoBehaviour
    {
        [Header("Attack Properties")]
        [SerializeField] private protected int _damage;
        [SerializeField] private protected float _attackCooldown;
        [SerializeField] private protected float _rangeX;
        [SerializeField] private protected float _rangeY;
        [SerializeField] private protected float _rangeOffsetX;
        [SerializeField] private protected float _rangeOffsetY;
        [SerializeField] private protected float _colliderDistance;
    }
}
