using UnityEngine;

public class ExpressionTired : MonoBehaviour, IExpressionState
{
    public void Express(SkinnedMeshRenderer pupilLeft, SkinnedMeshRenderer pupilRight)
    {
        Debug.Log("tired");
        pupilLeft.SetBlendShapeWeight(1, 100);
        pupilRight.SetBlendShapeWeight(1, 100);
    }
    public void Conceal(SkinnedMeshRenderer pupilLeft, SkinnedMeshRenderer pupilRight)
    {
        pupilLeft.SetBlendShapeWeight(1, 0);
        pupilRight.SetBlendShapeWeight(1, 0);
    }
}
