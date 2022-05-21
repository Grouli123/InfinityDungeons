using UnityEngine;

namespace DangeonInf
{
    public class IdleState : State
    {
        public PursueTargetState _pursueState;
        public LayerMask _layerDetection;

        public override State Tick(EnemyManager _enemyManager, EnemyStatsManager _enemyStatsManager, EnemyAnimatorManager _enemyAnimatorManager)
        {
            #region Handle Enemy Target Detection
            Collider[] _colliders = Physics.OverlapSphere(transform.position, _enemyManager.detectionRadius, _layerDetection);

            for (int i = 0; i < _colliders.Length; i++)
            {
                CharacterStatsManager _characterStats = _colliders[i].transform.GetComponent<CharacterStatsManager>();

                if(_characterStats != null)
                {
                    Vector3 _directionTarget = _characterStats.transform.position - transform.position;
                    float _viewableAngle = Vector3.Angle(_directionTarget, transform.forward);

                    if (_viewableAngle > _enemyManager.mininumDetectionAngle && _viewableAngle < _enemyManager.maximumDetectionAngle)
                    {
                        _enemyManager.currentTarget = _characterStats;
                    }                
                }
            }
            #endregion

            #region Handle Switching To Next State
            if(_enemyManager.currentTarget !=null)
            {
                return _pursueState;
            }
            else
            {
                return this;
            }
            #endregion            
        }
    }
}
