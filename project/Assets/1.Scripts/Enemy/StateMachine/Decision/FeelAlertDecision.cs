using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Feel Alert")]
public class FeelAlertDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return controller.variables.feelAlert;
    }
}
