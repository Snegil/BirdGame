using UnityEngine;

public class ConfettiButton : MonoBehaviour, IInteractable
{
    [SerializeField]
    int particleCount;

    ParticleSystem system;

    [SerializeField]
    AudioSource src;

    [SerializeField]
    AudioClip SFX;


    public void Interact()
    {
        if (system == null) system = GetComponent<ParticleSystem>();

        system.Emit(particleCount);
        PlaySound();
    }

    void PlaySound()
    {
        if (SFX != null)
        {
            src.PlayOneShot(SFX);
            return;
        }
        Debug.LogWarning(this + " : HAS NO SFX ASSIGNED.");
    }
}
