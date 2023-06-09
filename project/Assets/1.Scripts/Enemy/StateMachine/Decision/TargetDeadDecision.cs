using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Target Dead")]
public class TargetDeadDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        try
        {
            return controller.aimTarget.root.GetComponent<HealthBase>().IsDead;
        }
        catch (UnassignedReferenceException)
        {
            Debug.LogError("생명력 관리 컴포넌트 HealthBase를 붙여주세요" + controller.name, controller.gameObject);
        }

        return false;
    }
}
