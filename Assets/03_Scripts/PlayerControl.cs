using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    private static readonly int Shoot = Animator.StringToHash("Shoot");
    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");

    private Animator _animator;
    private Rigidbody _rigid;
    private Health _health;
    private SpriteRenderer _sprite;

    [SerializeField] private float maxSpeed ;
    [SerializeField] private float speed;
    [SerializeField] private float speedDecreaseSpiritForm;
    [SerializeField] private float jump = 2f;
    [SerializeField] private bool isGrounded;
    [SerializeField] public Vector3 lastSpawnPoint;
    [SerializeField] private string sceneToLoad;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletOrigin;
    [SerializeField] private bool inShootingCooldown;
    [SerializeField] private float shootingCooldownTime = 1f;
    [SerializeField] private bool inSpiritForm;
    [SerializeField] private float spiritTime = 3f;
    [SerializeField] private float spiritCooldown = 5f;
    [SerializeField] private float switchAnimationTime = 2f;
    [SerializeField] private bool inCooldown;
    [SerializeField] private GameObject normalSprite;
    [SerializeField] private GameObject spiritSprite;
    [SerializeField] private GameObject switchVFX;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _health = GetComponent<Health>();
        _animator = GetComponentInChildren<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        inSpiritForm = false;
        inCooldown = false;
        speed = maxSpeed;
    }

    private void Update()
    {
        HandleFormSwitch();
        HandleShoot();
        HandleXMovement();
        HandleYMovement();
        HandleAnimations();
    }

    private void HandleAnimations()
    {
        _animator.SetFloat(MoveX, Mathf.Abs(_rigid.velocity.x));
        _animator.SetBool(IsGrounded, isGrounded);
    }

    private void HandleXMovement()
    {
        var direction = GetDirection();
        HandleCharacterSpeed(direction);
        HandleSpriteFlip(direction);
    }

    private void HandleCharacterSpeed(int direction)
    {
        var horizontalSpeed = speed * direction;
        _rigid.velocity = new Vector2(horizontalSpeed, _rigid.velocity.y);
    }

    private static int GetDirection()
    {
        var direction = 0;
        if (Input.GetKey(KeyCode.A)) direction += 1;
        if (Input.GetKey(KeyCode.D)) direction -= 1;
        return direction;
    }

    private void HandleSpriteFlip(int direction)
    {
        if (direction > 0 && _sprite.flipX)
            _sprite.flipX = false;
        if (direction < 0 && !_sprite.flipX)
            _sprite.flipX = true;
    }

    private void HandleYMovement()
    {
        if (!isGrounded) return;
        if (inSpiritForm) return;
        if (!Input.GetKeyDown(KeyCode.Space)) return;

        var velocity = new Vector2(_rigid.velocity.x, jump);
        _rigid.velocity = velocity;
    }

    private void HandleShoot()
    {
        if (!Input.GetKeyDown(KeyCode.Mouse0)) return;
        if (inShootingCooldown) return;

        var posBullet = bulletOrigin.transform.position;
        var newBullet = Instantiate(bulletPrefab, posBullet, Quaternion.identity);
        newBullet.GetComponent<BulletBehavior>().player = transform.gameObject;
        
        inShootingCooldown = true;
        _animator.SetTrigger(Shoot);
        StartCoroutine(nameof(ShootingTimerCooldown));
    }

    private void HandleFormSwitch()
    {
        if (!Input.GetKeyDown(KeyCode.E)) return;
        if (inCooldown) return;
        if (inSpiritForm) return;

        SwitchForm();
    }

    private void SwitchForm()
    {
        StartCoroutine(nameof(SpiritTimer));
        StartCoroutine(ToggleFormTimer());
        PlaySwitchVFX();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) 
            isGrounded = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeathZone"))
            TeleportToSpawnPoint();

        if (other.gameObject.CompareTag("SpawnPoint"))
        {
            var position = transform.position;
            lastSpawnPoint = new Vector3(position.x, position.y, position.z);
        }

        if (other.gameObject.CompareTag("WinZone"))
            SceneManager.LoadScene(sceneToLoad);
    }
    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground")) 
            return;
        
        isGrounded = false;
    }
    
    private IEnumerator ShootingTimerCooldown()
    {
        
        yield return new WaitForSeconds(shootingCooldownTime);
        inShootingCooldown = false;
    }

    private IEnumerator ToggleFormTimer()
    {
        inSpiritForm = true;
        
        yield return new WaitForSeconds(switchAnimationTime);
        
        normalSprite.SetActive(false);
        spiritSprite.SetActive(true);
        
        speed -= speedDecreaseSpiritForm;
        
        var go = gameObject;
        go.tag = "Spirit";
        go.layer = 7;
        
        StartCoroutine(ResetForm());
    }

    private IEnumerator ResetForm()
    {
        yield return new WaitForSeconds(spiritTime);
        
        inSpiritForm = false;
        speed = maxSpeed;
        
        _health.enabled = true;
        gameObject.layer = 0;
        isGrounded = true;
        inCooldown = true;
        
        StartCoroutine(nameof(SpiritTimerCooldown));
        ReturnForm();
    }

    private void ReturnForm()
    {
        normalSprite.SetActive(true);
        spiritSprite.SetActive(false);
        gameObject.tag = "Player";
    }

    private void PlaySwitchVFX()
    {
        var newSwitchEffect = Instantiate(switchVFX, transform.position, Quaternion.identity);
        newSwitchEffect.transform.SetParent(transform);
        Destroy(newSwitchEffect, 3);
    }

    private void TeleportToSpawnPoint()
    {
        transform.position = lastSpawnPoint;
    }

    private IEnumerator SpiritTimer()
    {
        yield return new WaitForSeconds(spiritTime);
        SwitchForm();
    }

    private IEnumerator SpiritTimerCooldown()
    {
        yield return new WaitForSeconds(spiritCooldown);
        inCooldown = false;
    }
}
