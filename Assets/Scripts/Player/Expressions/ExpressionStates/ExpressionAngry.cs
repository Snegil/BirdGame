using UnityEngine;

public class ExpressionAngry : MonoBehaviour, IExpressionState
{
    public void Express(SkinnedMeshRenderer pupilLeft, SkinnedMeshRenderer pupilRight)
    {
        Debug.Log("angyboi");
        pupilLeft.SetBlendShapeWeight(3, 100);
        pupilRight.SetBlendShapeWeight(3, 100);
    }
    public void Conceal(SkinnedMeshRenderer pupilLeft, SkinnedMeshRenderer pupilRight)
    {
        pupilLeft.SetBlendShapeWeight(3, 0);
        pupilRight.SetBlendShapeWeight(3, 0);
    }
}
