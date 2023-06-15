using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/End Burst")]
public class EndBurstDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return controller.variables.currentShots >= controller.variables.shotsInRounds;
    }
}
