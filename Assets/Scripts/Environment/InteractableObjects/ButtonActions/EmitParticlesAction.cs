using UnityEngine;

public class EmitParticlesAction : ButtonAction
{
    [SerializeField]
    ParticleSystem system;

    [SerializeField]
    int amountOfParticles = 100;

    public override void Action()
    {
        system.Emit(amountOfParticles);
    }
}
