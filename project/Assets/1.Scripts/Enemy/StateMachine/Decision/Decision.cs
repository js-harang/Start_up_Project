using UnityEngine;

public abstract class Decision : ScriptableObject
{
    public abstract bool Decide(StateController controller);

    public virtual void OnEnableDecision(StateController controller)
    {

    }

    public delegate bool HandleTargets(StateController controller, bool hasTargets, Collider[] targetInRadius);
    public static bool CheckTargetsInRadius(StateController controller, float radius, HandleTargets handleTargets)
    {
        if (controller.aimTarget.root.GetComponent<HealthBase>().IsDead)
        {
            return false;
        }
        else
        {
            Collider[] targetsInRadius = Physics.OverlapSphere(controller.transform.position, radius, controller.generalStats.targetMask);

            return handleTargets(controller, targetsInRadius.Length > 0, targetsInRadius);
        }
    }
}
