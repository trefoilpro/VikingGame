using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _headAudioSource;
    [SerializeField] private AudioSource _roarAudioSource;
    [SerializeField] private AudioSource _footAudioSource;

    [SerializeField] private AudioClip _dieSound;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private AudioClip _attackSound;
    [SerializeField] private AudioClip[] _footStepSounds;
    
    public void PlayFootStepSound()
    {
        _footAudioSource.PlayOneShot(_footStepSounds[Random.Range(0, _footStepSounds.Length)]);
    }
    
    public void PlayHitSound()
    {
        _headAudioSource.PlayOneShot(_hitSound);
    }
    
    public void PlayDieSound()
    {
        _roarAudioSource.Stop();
        _headAudioSource.PlayOneShot(_dieSound);
    }
    
    public void PlayAttackSound()
    {
        _headAudioSource.PlayOneShot(_attackSound);
    }
}
