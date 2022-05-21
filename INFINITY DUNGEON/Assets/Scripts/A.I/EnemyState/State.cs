using UnityEngine;

namespace DangeonInf
{
    public abstract class State : MonoBehaviour
    {
        public abstract State Tick(EnemyManager enemyManager, EnemyStatsManager enemyStatsManager, EnemyAnimatorManager enemyAnimatorManager);
    }
}
