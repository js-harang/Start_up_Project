using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/GeneralStats")]
public class GeneralStats : ScriptableObject
{
    [Header("General")]
    [Tooltip("npc 정찰 속도 clear state")]
    public float patrolSpeed = 2f;
    [Tooltip("npc가 따라오는 속도 warning state")]
    public float chaseSpeed = 5f;
    [Tooltip("npc가 회피하는 속도 engage state")]
    public float evadeSpeed = 15;
    [Tooltip("웨이포인트에서 대기하는 시간")]
    public float patrolWaitTime = 2f;
    [Tooltip("장애물 레이어 마스크")]
    public LayerMask obstacleMask;
    public float angleDeadZone = 5f;
    [Tooltip("속도 Damping 시간")]
    public float speedDampTime = 0.4f;
    [Tooltip("각속도 Damping 시간")]
    public float angularSpeedDampTime = 0.2f;
    [Tooltip("각도 회전에 따른 반응 시간")]
    public float angleResponeseTime = 0.2f;
    [Header("Cover")]
    [Tooltip("장애물에 숨었을 때 고려해야할 최소 높이 값")]
    public float aboveCoverHeight = 1.5f;
    [Tooltip("장애물 레이어 마스크")]
    public LayerMask coverMask;
    [Tooltip("사격 레이어 마스크")]
    public LayerMask shotMask;
    [Tooltip("타겟 레이어 마스크")]
    public LayerMask targetMask;
}
