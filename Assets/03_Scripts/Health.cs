using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    private static readonly int Die = Animator.StringToHash("Die");
    private static readonly int Fade = Animator.StringToHash("Fade");
    private static readonly int Unfade = Animator.StringToHash("Unfade");
    
    private Animator _animator;
    private PlayerControl _playerControl;
    private Rigidbody _rigidbody;
    private Collider _collider;

    [SerializeField] private int currentHealth = 1;
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private Animator screenFader;
    
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerControl = GetComponent<PlayerControl>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    public void LoseLife(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) 
            SetPlayerDead();
    }

    private void SetPlayerDead()
    {
        StartCoroutine(RespawnPlayer());
        _animator.SetTrigger(Die);
        var newDeathVFX = Instantiate(deathVFX, transform.position, Quaternion.identity);
        newDeathVFX.transform.SetParent(transform);
        Destroy(newDeathVFX, 2);
        DisablePlayerSettings();
        screenFader.SetTrigger(Fade);
    }

    private void DisablePlayerSettings()
    {
        _playerControl.enabled = false;
        _rigidbody.isKinematic = true;
        _collider.enabled = false;
    }

    private void EnablePlayerSettings()
    {
        _playerControl.enabled = true;
        _rigidbody.isKinematic = false;
        _collider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;
        if (!gameObject.CompareTag("Player")) return;
        
        LoseLife(1);
    }

    private IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(2f);
        transform.position = _playerControl.lastSpawnPoint;
        EnablePlayerSettings();
        screenFader.SetTrigger(Unfade);
        _animator.ResetTrigger(Die);
    }
    
}
