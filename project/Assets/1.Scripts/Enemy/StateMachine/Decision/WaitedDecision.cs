using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Waited")]
public class WaitedDecision : Decision
{
    public float maxTimeToWait;
    private float timeToWait;
    private float startTime;

    public override void OnEnableDecision(StateController controller)
    {
        timeToWait = Random.Range(0, maxTimeToWait);
        startTime = Time.time;
    }

    public override bool Decide(StateController controller)
    {
        return (Time.time - startTime) >= timeToWait;
    }
}
