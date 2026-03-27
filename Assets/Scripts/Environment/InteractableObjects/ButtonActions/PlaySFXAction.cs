using UnityEngine;

public class PlaySFXAction : ButtonAction
{
    [SerializeField]
    AudioClip SFX;

    [SerializeField]
    AudioSource src;

    public override void Action()
    {
        if (SFX != null)
        {
            Debug.Log("PLAYING SFX");
            src.PlayOneShot(SFX);
            return;
        }
        Debug.LogWarning(this + " SFX NOT ASSIGNED");
    }
}
