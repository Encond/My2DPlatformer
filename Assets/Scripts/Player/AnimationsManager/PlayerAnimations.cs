using UnityEngine;

namespace Player.AnimationsManager
{
    public static class PlayerAnimations
    {
        private static readonly int _isAlive = Animator.StringToHash("IsAlive");
        public static int GetIsAlive() => _isAlive;
        
        private static readonly int _simpleAttack = Animator.StringToHash("SimpleAttack");
        public static int GetSimpleAttack() => _simpleAttack;
        
        private static readonly int _chargedAttack = Animator.StringToHash("ChargedAttack");
        public static int GetChargedAttack() => _chargedAttack;
        
        private static readonly int _running = Animator.StringToHash("Running");
        public static int GetRunning() => _running;
        
        private static readonly int _falling = Animator.StringToHash("Falling");
        public static int GetFalling() => _falling;
        
        private static readonly int _jumping = Animator.StringToHash("Jumping");
        public static int GetJumping() => _jumping;
        
        private static readonly int _isGrounded = Animator.StringToHash("IsGrounded");
        public static int GetIsGrounded() => _isGrounded;
        
        private static readonly int _isDamaged = Animator.StringToHash("Damaged");
        public static int GetIsDamaged() => _isDamaged;
    }
}
