using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStateAttack", menuName = "PlayerStates/PlayerStateAttack", order = 5)]
public class PlayerStateAttack : ScriptableObject
{
    [SerializeField]
    float attackDamage = 1;

    public void Attack()
    {
        Debug.Log("ATTACK Dealt: " + attackDamage);
    }
}
