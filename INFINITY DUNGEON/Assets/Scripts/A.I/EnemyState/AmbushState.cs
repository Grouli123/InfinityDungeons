using UnityEngine;

namespace DangeonInf
{
    public class AmbushState : State
    {
		public bool _isSleep;
		public float _detectionRadius = 2;
		public string animSleep;
		public string _wakeUpAnimation;
		public LayerMask _layerDetection;

		public PursueTargetState _pursueState;

        public override State Tick(EnemyManager _enemyManager, EnemyStatsManager _enemyStatsManager, EnemyAnimatorManager _enemyAnimatorManager)
        {
			if(_isSleep && _enemyManager.isInteracting == false)
			{
				_enemyAnimatorManager.PlayTargetAnimation(animSleep, true);
			}

			#region  Handle Target Detection:

			Collider[] colliders = Physics.OverlapSphere(transform.transform.position, _enemyManager.detectionRadius, _layerDetection);

			for (int i = 0; i < colliders.Length; i++)
			{
				CharacterStatsManager _characterStats = colliders[i].transform.GetComponent<CharacterStatsManager>();

				if(_characterStats != null)
				{
					Vector3 _directionTarget = _characterStats.transform.position - _enemyManager.transform.position;
					float _viewableAngle = Vector3.Angle(_directionTarget, _enemyManager.transform.forward);

					if(_viewableAngle > _enemyManager.mininumDetectionAngle &&
					_viewableAngle < _enemyManager.maximumDetectionAngle)
					{
						_enemyManager.currentTarget = _characterStats;
						_isSleep = false;
						_enemyAnimatorManager.PlayTargetAnimation(_wakeUpAnimation, true);
					}
				}
			}

			#endregion
	
			# region Handle State Change

			if (_enemyManager.currentTarget != null)
			{
				return _pursueState;
			}
			else
			{
				return this;
			}

			# endregion
		}
    }
}