using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStateIdle", menuName = "ScriptableObjects/PlayerStateIdle", order = 1)]
public class PlayerStateIdle : ScriptableObject
{
    public void Idle(GroundCheck groundCheck)
    {
        if (!groundCheck.GroundedCheck())
        {
            return;
        }
        //Debug.Log("IDLING");
    }
}
