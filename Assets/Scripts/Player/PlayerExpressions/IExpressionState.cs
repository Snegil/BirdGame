using UnityEngine;

public interface IExpressionState
{
    public void Express(SkinnedMeshRenderer pupilLeft, SkinnedMeshRenderer pupilRight) {}
    public void Conceal(SkinnedMeshRenderer pupilLeft, SkinnedMeshRenderer pupilRight) {}
}
