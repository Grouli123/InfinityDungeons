using UnityEngine;

namespace DangeonInf
{
    public class AttackState : State
    {
        public CombatStanceState _combatState;
        public EnemyAttackAction[] _attackEnemy;
        public EnemyAttackAction _attack;

        bool _itIsWillDoComboOnNextAttack = false;

        public override State Tick(EnemyManager _enemyManager, EnemyStatsManager _enemyStatsManager, EnemyAnimatorManager _enemyAnimatorManager)
        {
            if (_enemyManager.isInteracting && _enemyManager.canDoCombo == false)
            {
                return this;
            }
            else if (_enemyManager.isInteracting && _enemyManager.canDoCombo)
            {
                if (_itIsWillDoComboOnNextAttack)
                {
                    _itIsWillDoComboOnNextAttack = false;
                    _enemyAnimatorManager.PlayTargetAnimation(_attack.actionAnimation, true);
                }
            }

            Vector3 _directionTarget = _enemyManager.currentTarget.transform.position - transform.position;//Поставил EnemyManager
            float _distanceToTarget = Vector3.Distance(_enemyManager.currentTarget.transform.position, _enemyManager.transform.position);
            float _viewableAngle = Vector3.Angle(_directionTarget, transform.forward);

            RotateTowardsTargetByHandle(_enemyManager);

            if(_enemyManager.isPreformingAction)
            {                
                return _combatState;
            }
            
            if (_attack != null)
            {
                if(_distanceToTarget < _attack.mininumDistanceNeededToAttack)
                {
                    return this;
                }
                else if (_distanceToTarget < _attack.maximumDistanceNeededToAttack)
                {
                    if (_viewableAngle <= _attack.maximumAttackAngle &&
                    _viewableAngle >= _attack.minimumAttackAngle)
                    {
                        if (_enemyManager.currentRecoveryTime <= 0 && _enemyManager.isPreformingAction == false)
                        {
                            _enemyAnimatorManager.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                            _enemyAnimatorManager.animator.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
                            _enemyAnimatorManager.PlayTargetAnimation(_attack.actionAnimation, true);
                            _enemyManager.isPreformingAction = true;
                            ComboChanceByRoll(_enemyManager);
                            
                            if(_attack.canCombo && _itIsWillDoComboOnNextAttack)
                            {
                                _attack = _attack.comboAction;
                                return this;
                            }
                            else
                            {
                                _enemyManager.currentRecoveryTime = _attack.recoveryTime;
                                _attack = null;
                                return _combatState;
                            }                            
                        }
                    }
                }
            }
            else
            {
                GetAttackNew(_enemyManager);
            }          

            return _combatState;
        }

         private void GetAttackNew(EnemyManager _enemyManager)
         {
            Vector3 _directionTagret = _enemyManager.currentTarget.transform.position - transform.position;
            float _viewableAngle = Vector3.Angle(_directionTagret, transform.forward);
            float _distanceToTarget = Vector3.Distance(_enemyManager.currentTarget.transform.position, transform.position);

            int _scoreMaxValue = 0;

            for (int i = 0; i < _attackEnemy.Length; i++)
            {
                EnemyAttackAction _enemyAttackAction = _attackEnemy[i];

                if (_distanceToTarget <= _enemyAttackAction.maximumDistanceNeededToAttack 
                && _distanceToTarget >= _enemyAttackAction.mininumDistanceNeededToAttack)
                {
                    if (_viewableAngle <= _enemyAttackAction.maximumAttackAngle 
                    && _viewableAngle >= _enemyAttackAction.minimumAttackAngle)
                    {
                        _scoreMaxValue += _enemyAttackAction.attackScore;
                    }
                }
            }
            
            int _randomValueScore = Random.Range(0, _scoreMaxValue);
            int _temporaryScore = 0;

            for (int i = 0; i < _attackEnemy.Length; i++)
            {
                EnemyAttackAction _enemyAttackAction = _attackEnemy[i];

                if (_distanceToTarget <= _enemyAttackAction.maximumDistanceNeededToAttack
                && _distanceToTarget >= _enemyAttackAction.mininumDistanceNeededToAttack)
                {
                    if (_viewableAngle <= _enemyAttackAction.maximumAttackAngle 
                    && _viewableAngle >= _enemyAttackAction.minimumAttackAngle)
                    {
                       if (_attack != null)
                       return;

                       _temporaryScore += _enemyAttackAction.attackScore;

                       if (_temporaryScore > _randomValueScore)
                       {
                           _attack = _enemyAttackAction;
                       }
                    }
                }
            }
         }

        public void RotateTowardsTargetByHandle(EnemyManager _enemyManager)
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
                Vector3 _directionRelative = transform.InverseTransformDirection(_enemyManager.navMeshAgent.desiredVelocity);
                Vector3 _velocityTarget = _enemyManager.enemyRigidbody.velocity;

                _enemyManager.navMeshAgent.enabled = true;
                _enemyManager.navMeshAgent.SetDestination(_enemyManager.currentTarget.transform.position);
                float _distanceToTarget = Vector3.Distance(_enemyManager.currentTarget.transform.position, _enemyManager.transform.position);

            float _rotationApplyDynamicEnemy = Quaternion.Angle(_enemyManager.transform.rotation, Quaternion.LookRotation(_enemyManager.navMeshAgent.desiredVelocity.normalized));
            if (_distanceToTarget > 5) _enemyManager.navMeshAgent.angularSpeed = 500f;
            else if (_distanceToTarget < 5 && Mathf.Abs(_rotationApplyDynamicEnemy) < 30) _enemyManager.navMeshAgent.angularSpeed = 50f;
            else if (_distanceToTarget < 5 && Mathf.Abs(_rotationApplyDynamicEnemy) > 30) _enemyManager.navMeshAgent.angularSpeed = 500f;

            Vector3 _directionTarget = _enemyManager.currentTarget.transform.position - _enemyManager.transform.position;
            Quaternion _rotationApplyStaticEnemy = Quaternion.LookRotation(_directionTarget);


            if (_enemyManager.navMeshAgent.desiredVelocity.magnitude > 0)
            {
                _enemyManager.navMeshAgent.updateRotation = false;
                _enemyManager.transform.rotation = Quaternion.RotateTowards(_enemyManager.transform.rotation,
                Quaternion.LookRotation(_enemyManager.navMeshAgent.desiredVelocity.normalized), _enemyManager.navMeshAgent.angularSpeed * Time.deltaTime);
            }
            else
            {
                _enemyManager.transform.rotation = Quaternion.RotateTowards(_enemyManager.transform.rotation, _rotationApplyStaticEnemy, _enemyManager.navMeshAgent.angularSpeed * Time.deltaTime);
            }
    
            }
        }

        private void ComboChanceByRoll(EnemyManager _enemyManager)
        {
            float _chanceOfComno = Random.Range(0, 100);

            if (_enemyManager.allowAIPerfomConbos && _chanceOfComno <= _enemyManager.comboLikelyHood)
            {
                _itIsWillDoComboOnNextAttack = true;
            }
        }
    }
}
