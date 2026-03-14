using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Audio clips")]
    [SerializeField] AudioClip _shootSound;
    [SerializeField] AudioClip _bulletImpactSound;
    [Header("Audio Source")]
    [SerializeField] AudioSource _audioSource;

    float _pitchMax = 1.2f;
    float _pitchMin = 0.8f;
    private void Start()
    {
        ThirdPersonShooterController.OnPlayerShoot += ThirdPersonShooterController_OnPlayerShoot;
        bulletProjectile.OnBulletImpact += BulletProjectile_OnBulletImpact;
    }

    private void BulletProjectile_OnBulletImpact(object sender, bulletProjectile.OnBulletImpactEventArgs e)
    {
        playAtPoint(_bulletImpactSound, e.impactPosition, 0.3f);
    }

    private void ThirdPersonShooterController_OnPlayerShoot(object sender, System.EventArgs e)
    {
        playOneShot(_shootSound, 0.5f);
    }

    void playOneShot(AudioClip audioClip, float volume = 1)
    {
        _audioSource.pitch = Random.Range(_pitchMin, _pitchMax);
        _audioSource.PlayOneShot(audioClip, volume);
    }
    void playAtPoint(AudioClip audioClip,Vector3 hitPosition, float volume = 1)
    {
        AudioSource.PlayClipAtPoint(_bulletImpactSound, hitPosition, volume);
    }
}
