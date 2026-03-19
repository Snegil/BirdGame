using UnityEngine;

public class ExpressionHappy : MonoBehaviour, IExpressionState
{
    public void Express(SkinnedMeshRenderer pupilLeft, SkinnedMeshRenderer pupilRight)
    {
        Debug.Log("Kiwihappi");
        pupilLeft.SetBlendShapeWeight(0, 100);
        pupilRight.SetBlendShapeWeight(0, 100);
    }
    public void Conceal(SkinnedMeshRenderer pupilLeft, SkinnedMeshRenderer pupilRight)
    {
        pupilLeft.SetBlendShapeWeight(0, 0);
        pupilRight.SetBlendShapeWeight(0, 0);
    }
}
