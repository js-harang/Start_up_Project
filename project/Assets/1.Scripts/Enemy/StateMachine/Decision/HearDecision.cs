using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Hear")]
public class HearDecision : Decision
{
    private Vector3 lastPos, currentPos;

    public override void OnEnableDecision(StateController controller)
    {
        lastPos = currentPos = Vector3.positiveInfinity;
    }

    public bool MyHandleTargets(StateController controller, bool hasTarget, Collider[] targetInHearRadius)
    {
        if (hasTarget)
        {
            currentPos = targetInHearRadius[0].transform.position;

            if (!Equals(lastPos, Vector3.positiveInfinity))
            {
                if (!Equals(lastPos, currentPos))
                {
                    controller.personalTarget = currentPos;

                    return true;
                }
            }

            lastPos = currentPos;
        }

        return false;
    }

    public override bool Decide(StateController controller)
    {
        if (controller.variables.hearAlert)
        {
            controller.variables.hearAlert = false;

            return true;
        }
        else
        {
            return CheckTargetsInRadius(controller, controller.perceptionRadius, MyHandleTargets);
        }
    }
}
