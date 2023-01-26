using Player;
using UnityEngine;

namespace Enemy.Attacks
{
    public class AttacksManager : MonoBehaviour
    {
        [SerializeField] private MeleeSimpleAttack _meleeSimpleAttack;
        [SerializeField] private HealthManager _healthManager;

        private void Awake()
        {
            // _meleeSimpleAttack = _meleeSimpleAttack._enemy.gameObject.GetComponentInChildren<MeleeSimpleAttack>();
            // Debug.Log(_meleeSimpleAttack._enemy.gameObject.GetComponentsInChildren<MeleeSimpleAttack>());
        }

        private void Start()
        {
            // _meleeSimpleAttack = _meleeSimpleAttack.GetComponentInParent<Enemy>()
            //     .GetComponentInChildren<MeleeSimpleAttack>();
        }

        // private void OnEnable()
        // {
        //     _meleeSimpleAttack.OnHitSimpleAttack += PlayerTakeDamage;
        // }
        //
        // private void OnDisable()
        // {
        //     _meleeSimpleAttack.OnHitSimpleAttack -= PlayerTakeDamage;
        // }

        private void PlayerTakeDamage(int damage)
        {
            
            
            Debug.Log(damage + " AttacksManager");
        }

        private void Update()
        {
            Debug.Log(_meleeSimpleAttack.name);
        }
    }
}