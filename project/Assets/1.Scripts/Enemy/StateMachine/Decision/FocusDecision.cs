using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Focus")]
public class FocusDecision : Decision
{
    public enum Sense
    {
        NEAR,
        PERCEPTION,
        VIEW,
    }

    [Tooltip("어떤 크기로 위혐요소 감지를 하시겠습니까?")]
    public Sense sense;

    [Tooltip("현재 엄폐물을 해제 할까요?")]
    public bool invalidateCoverSpot;

    private float radius;

    public override void OnEnableDecision(StateController controller)
    {
        switch (sense)
        {
            case Sense.NEAR:
                radius = controller.nearRadius;
                break;
            case Sense.PERCEPTION:
                radius = controller.perceptionRadius;
                break;
            case Sense.VIEW:
                radius = controller.viewRadius;
                break;
            default:
                radius = controller.nearRadius;
                break;
        }
    }

    private bool MyHandleTargets(StateController controller, bool hasTarget, Collider[] targetsInHearRadius)
    {
        if (hasTarget && !controller.BlockedSight())
        {
            if (invalidateCoverSpot)
            {
                controller.CoverSpot = Vector3.positiveInfinity;
            }

            controller.targetInSight = true;
            controller.personalTarget = controller.aimTarget.position;

            return true;
        }

        return false;
    }

    public override bool Decide(StateController controller)
    {
        return (sense != Sense.NEAR && controller.variables.feelAlert && !controller.BlockedSight()) ||
            Decision.CheckTargetsInRadius(controller, radius, MyHandleTargets);
    }
}
