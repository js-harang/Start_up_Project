using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Take Cover")]
public class TakeCoverDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        if (controller.variables.currentShots < controller.variables.shotsInRounds ||
            controller.variables.waitInCoverTime > controller.variables.coverTime ||
            Equals(controller.CoverSpot, Vector3.positiveInfinity))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
