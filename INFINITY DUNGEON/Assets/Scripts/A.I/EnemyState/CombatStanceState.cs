using UnityEngine;

namespace DangeonInf
{
    public class CombatStanceState : State
    {
        public AttackState _attackState;
        public PursueTargetState _pursueState;
        
        public override State Tick(EnemyManager _enemyManager, EnemyStatsManager _enemyStatsManager, EnemyAnimatorManager _enemyAnimatorManager)
        {
            if (_enemyManager.isInteracting)
                return this;

            float _distanceFromTarget = Vector3.Distance(_enemyManager.currentTarget.transform.position, _enemyManager.transform.position);
            
            HandleRotateTowardsTarget(_enemyManager);

            if (_enemyManager.isPreformingAction)
            {
                _enemyAnimatorManager.animator.SetFloat("Vertical", 0, 0.01f, Time.deltaTime);
            }

            if (_enemyManager.currentRecoveryTime <= 0 && _distanceFromTarget <= _enemyManager.maximumAttackRange)
            {
                return _attackState;
            }
            else if(_distanceFromTarget > _enemyManager.maximumAttackRange)
            {
                return _pursueState;
            }
            else
            {
                return this;
            }            
        }
        
        public void HandleRotateTowardsTarget(EnemyManager _enemyManager)
        {
            if(_enemyManager.isPreformingAction)
            {
                Vector3 _direction = _enemyManager.currentTarget.transform.position - transform.position;
                _direction.y = 0;
                _direction.Normalize();

                if(_direction == Vector3.zero)
                {
                    _direction = transform.forward;
                }

                Quaternion _rotationTarget = Quaternion.LookRotation(_direction);
                _enemyManager.transform.rotation = Quaternion.Slerp(transform.rotation, _rotationTarget, _enemyManager.rotationSpeed * Time.deltaTime);
            }
            else
            {
                Vector3 relativeDirection = transform.InverseTransformDirection(_enemyManager.navMeshAgent.desiredVelocity);
                Vector3 targetVelocity = _enemyManager.enemyRigidbody.velocity;

                _enemyManager.navMeshAgent.enabled = true;
                _enemyManager.navMeshAgent.SetDestination(_enemyManager.currentTarget.transform.position);
                float distanceFromTarget = Vector3.Distance(_enemyManager.currentTarget.transform.position, _enemyManager.transform.position);

                float rotationToApplyToDynamicEnemy = Quaternion.Angle(_enemyManager.transform.rotation, Quaternion.LookRotation(_enemyManager.navMeshAgent.desiredVelocity.normalized));
                if (distanceFromTarget > 5) _enemyManager.navMeshAgent.angularSpeed = 500f;
                else if (distanceFromTarget < 5 && Mathf.Abs(rotationToApplyToDynamicEnemy) < 30) _enemyManager.navMeshAgent.angularSpeed = 50f;
                else if (distanceFromTarget < 5 && Mathf.Abs(rotationToApplyToDynamicEnemy) > 30) _enemyManager.navMeshAgent.angularSpeed = 500f;

                Vector3 targetDirection = _enemyManager.currentTarget.transform.position - _enemyManager.transform.position;
                Quaternion rotationToApplyToStaticEnemy = Quaternion.LookRotation(targetDirection);

                if (_enemyManager.navMeshAgent.desiredVelocity.magnitude > 0)
                {
                    _enemyManager.navMeshAgent.updateRotation = false;
                    _enemyManager.transform.rotation = Quaternion.RotateTowards(_enemyManager.transform.rotation,
                    Quaternion.LookRotation(_enemyManager.navMeshAgent.desiredVelocity.normalized), _enemyManager.navMeshAgent.angularSpeed * Time.deltaTime);
                }
                else
                {
                    _enemyManager.transform.rotation = Quaternion.RotateTowards(_enemyManager.transform.rotation, rotationToApplyToStaticEnemy, _enemyManager.navMeshAgent.angularSpeed * Time.deltaTime);
                }    
            }
        }
    }
}
