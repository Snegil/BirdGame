using UnityEngine;

public class ExpressionSeen : MonoBehaviour, IExpressionState
{
    public void Express(SkinnedMeshRenderer pupilLeft, SkinnedMeshRenderer pupilRight)
    {
        Debug.Log("SEEEN SOME SHITTTTT");
        pupilLeft.SetBlendShapeWeight(2, 100);
        pupilRight.SetBlendShapeWeight(2, 100);
    }
    public void Conceal(SkinnedMeshRenderer pupilLeft, SkinnedMeshRenderer pupilRight)
    {
        pupilLeft.SetBlendShapeWeight(2, 100);
        pupilRight.SetBlendShapeWeight(2, 100);
    }
}
