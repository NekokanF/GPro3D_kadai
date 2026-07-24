using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject FirePos;

    [SerializeField] float Currentspeed;
    [SerializeField] float Basespeed;
    
    [SerializeField] float CurrentHp;
    [SerializeField] float BaseHp;

    [SerializeField] float Firetime = 0f;

    PlayerInput playerInput;
    Rigidbody rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        // ˆÚ“®
        var move = playerInput.actions["Move"].ReadValue<Vector2>();
        var moveDir = new Vector3(move.x,0,move.y);
        rb.AddForce(moveDir * Currentspeed ,ForceMode.VelocityChange);
        Debug.Log(moveDir * Currentspeed);

        // ƒXƒvƒŠƒ“ƒg
        if (playerInput.actions["Sprint"].IsPressed())
        {
            Currentspeed = Basespeed * 1.3f;
        }
        else
        {
            Currentspeed = Basespeed;
        }

        if (playerInput.actions["Attack"].IsPressed())
        {
            Firetime -= Time.deltaTime;
            if (Firetime <= 0)
            {
                Instantiate(Bullet, FirePos.transform.position, Quaternion.identity);
                Firetime = 0.2f;
            }
        }
        else
        {
            Firetime = 0f;
        }
    }
}
