using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public PlayerStatus status; // プレイヤーのステータス
    public Weapon weapon;       // 武器のステータス

    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject FirePos;
    [SerializeField] public GameObject HitPos;

    [SerializeField] public int CurrentHP;

    [SerializeField] float CurrentSpeed;
    [SerializeField] float RotateSpeed;

    [SerializeField] bool OnDeath;
    [SerializeField] bool FireCooldown = false;
    [SerializeField] public bool OnReload = false;

    [SerializeField] LayerMask GroundLayer;

    Animator animator;
    PlayerInput playerInput;
    CapsuleCollider capsuleCollider;
    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetWeapon();
        SetStatus();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!OnDeath)
        {
            // 移動
            var move = playerInput.actions["Move"].ReadValue<Vector2>();
            var cameraDir = playerInput.camera.transform.forward;
            cameraDir.y = 0;
            var cameraRight = playerInput.camera.transform.right;
            cameraDir = cameraDir.normalized;
            var moveDir = cameraDir * move.y + cameraRight * move.x;
            rb.AddForce(moveDir * CurrentSpeed, ForceMode.VelocityChange);
            //Debug.Log(moveDir * status.CurrentSpeed);

            // スプリント
            if (playerInput.actions["Sprint"].IsPressed())
            {
                CurrentSpeed = status.BaseSpeed * 1.3f;
            }
            else
            {
                CurrentSpeed = status.BaseSpeed;
            }

            if (moveDir.magnitude >= 0.1f)
            {
                animator.SetBool("Move", true);
                animator.SetFloat("MoveSpeed", CurrentSpeed);
            }
            else
            {
                animator.SetBool("Move", false);
                animator.SetFloat("MoveSpeed", 0);
            }

            if (!OnReload)
            {
                // 射撃
                if (playerInput.actions["Attack"].IsPressed() && !FireCooldown && weapon.CurrentBulletAmount > 0)
                {
                    animator.SetBool("Fire", true);
                    CurrentSpeed = CurrentSpeed * 0.6f;
                    weapon.FireCurrentRate -= Time.deltaTime;
                    if (weapon.FireCurrentRate <= 0)
                    {
                        Instantiate(Bullet, FirePos.transform.position, Quaternion.identity);

                        weapon.CurrentBulletAmount--;
                        weapon.FireCurrentRate = weapon.FireBaseRate;
                    }
                }
                else if (!playerInput.actions["Attack"].IsPressed() && weapon.FireCurrentRate != weapon.FireBaseRate)
                {
                    animator.SetBool("Fire", false);
                    FireCooldown = true;
                }

                // リロード
                if (playerInput.actions["Reload"].WasPressedThisFrame() && weapon.CurrentMagazineAmount > 0)
                {
                    OnReload = true;
                }
            }

            // プレイヤー回転
            if (playerInput.actions["CameraStop"].IsPressed())
            {
                Vector2 mousePos = Mouse.current.position.ReadValue();
                Ray ray = Camera.main.ScreenPointToRay(mousePos);

                Plane groundPlane = new Plane(Vector3.up, transform.position);

                if (groundPlane.Raycast(ray, out float distance))
                {
                    Vector3 mouseWorldPos = ray.GetPoint(distance);

                    Vector3 direction = mouseWorldPos - transform.position;

                    direction.y = 0;

                    if (direction.sqrMagnitude > 0.5f)
                    {
                        Quaternion targetRotation = Quaternion.LookRotation(direction);
                        transform.rotation = Quaternion.Slerp(
                            transform.rotation,
                            targetRotation,
                            Time.deltaTime * RotateSpeed
                        );
                    }
                }
            }

            if (CurrentHP <= 0)
            {
                OnDeath = true;
                Death();
            }
        }

        if (FireCooldown)
        {
            weapon.FireCurrentRate -= Time.deltaTime;
            if (weapon.FireCurrentRate <= 0)
            {
                weapon.FireCurrentRate = 0;
                FireCooldown = false;
            }
        }

        if (OnReload)
        {
            weapon.CurrentReloadTime -= Time.deltaTime;
            if (weapon.CurrentReloadTime <= 0)
            {
                OnReload = false;
                weapon.CurrentBulletAmount = weapon.BaseBulletAmount;
                weapon.CurrentMagazineAmount--;
                weapon.CurrentReloadTime = weapon.BaseReloadTime;
            }
        }
    }

    // 死亡処理
    private void Death()
    {
        CurrentHP = 0;
        rb.useGravity = false;
        capsuleCollider.enabled = false;
        animator.SetTrigger("Death");
        Invoke("ReloadScene", 4f);
    }

    // 武器の状態初期化
    public void SetWeapon()
    {
        weapon.FireCurrentRate = 0;
        weapon.CurrentBulletAmount = weapon.BaseBulletAmount;
        weapon.CurrentMagazineAmount = weapon.BaseMagazineAmount;
        weapon.CurrentReloadTime = weapon.BaseReloadTime;
    }

    // ステータス初期化
    public void SetStatus()
    {
        CurrentHP = status.BaseHP;
        CurrentSpeed = status.BaseSpeed;
    }

    // デバッグ用
    private void ReloadScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
