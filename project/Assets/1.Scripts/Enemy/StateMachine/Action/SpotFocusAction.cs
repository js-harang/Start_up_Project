using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Spot Focus")]
public class SpotFocusAction : Action
{
    public override void Act(StateController controller)
    {
        controller.nav.destination = controller.personalTarget;
        controller.nav.speed = 0;
    }
}
