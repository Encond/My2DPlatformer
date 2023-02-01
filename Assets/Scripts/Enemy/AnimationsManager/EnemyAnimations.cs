using UnityEngine;

namespace Enemy.AnimationsManager
{
    public static class EnemyAnimations
    {
        private static readonly int _simpleAttack = Animator.StringToHash("SimpleAttack");
        public static int GetSimpleAttack() => _simpleAttack;
        
        private static readonly int _isAlive = Animator.StringToHash("IsAlive");
        public static int GetIsAlive() => _isAlive;
        
        private static readonly int _running = Animator.StringToHash("Running");
        public static int GetRunning() => _running;
    }
}
