using UnityEngine;

[RequireComponent(typeof(ExpressionHappy), typeof(ExpressionAngry))]
[RequireComponent(typeof(ExpressionSeen), typeof(ExpressionTired))]
public class ExpressionController : MonoBehaviour
{
    [SerializeField]
    SkinnedMeshRenderer pupilLeft;
    [SerializeField]
    SkinnedMeshRenderer pupilRight;

    IExpressionState expressionHappy;
    IExpressionState expressionAngry;
    IExpressionState expressionSeen;
    IExpressionState expressionTired;

    void Start()
    {
        expressionHappy = GetComponent<ExpressionHappy>();
        expressionAngry = GetComponent<ExpressionAngry>();
        expressionSeen = GetComponent<ExpressionSeen>();
        expressionTired = GetComponent<ExpressionTired>();
    }
    public void UpdateExpression(ExpressionStates expressionState)
    {
        switch(expressionState)
        {
            case ExpressionStates.Happy:
                expressionHappy.Express(pupilLeft, pupilRight);
                break;
            case ExpressionStates.Angry:
                expressionAngry.Express(pupilLeft, pupilRight);
                break;
            case ExpressionStates.SeenSomeShit:
                expressionSeen.Express(pupilLeft, pupilRight);
                break;
            case ExpressionStates.Tired:
                expressionTired.Express(pupilLeft, pupilRight);
                break;

            default:
            Debug.Log("default");
                expressionHappy.Conceal(pupilLeft, pupilRight);
                expressionAngry.Conceal(pupilLeft, pupilRight);
                expressionSeen.Conceal(pupilLeft, pupilRight);
                expressionTired.Conceal(pupilLeft, pupilRight);
                break;
        }
            
    }
}
