using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Reached Point")]
public class ReachedPointDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        if (Application.isPlaying == false)
        {
            return false;
        }

        if (controller.nav.remainingDistance <= controller.nav.stoppingDistance && !controller.nav.pathPending)
        {
            return true;
        }
        else
        {
            return false;

        }
    }
}
