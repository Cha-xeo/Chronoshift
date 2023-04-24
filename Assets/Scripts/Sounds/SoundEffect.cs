using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public AudioClip[] walkingSound;
    public AudioClip castSound;
    public AudioClip attackSound;

    public void WalkingSound(int index)
    {
        SoundManager.Instance.PlaySound(walkingSound[index]);
    }
    public void CastingSound()
    {
        SoundManager.Instance.PlaySound(castSound);
    }
    public void AttackSound()
    {
        SoundManager.Instance.PlaySound(attackSound);
    }
}
