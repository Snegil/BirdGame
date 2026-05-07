using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStateIdle", menuName = "PlayerStates/PlayerStateIdle", order = 1)]
public class PlayerStateIdle : ScriptableObject
{
    public void Idle(GroundCheck groundCheck)
    {
        if (!groundCheck.GroundCheckBool())
        {
            return;
        }
        //Debug.Log("IDLING");
    }
}
