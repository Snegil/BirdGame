using UnityEngine;

public class ExpressionRegular : MonoBehaviour, IExpressionState
{
    public void Express(SkinnedMeshRenderer PupilLeft, SkinnedMeshRenderer PupilRight)
    {
        PupilRight.SetBlendShapeWeight(0, 0);
        PupilLeft.SetBlendShapeWeight(0, 0);
    }
    public void Conceal(SkinnedMeshRenderer PupilLeft, SkinnedMeshRenderer PupilRight) {}
}
