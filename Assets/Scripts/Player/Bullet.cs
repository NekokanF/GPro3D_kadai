using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject MuzzleFlashObj;
    [SerializeField] float speed;

    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        Instantiate(MuzzleFlashObj,transform.position, Quaternion.identity);
        rb.AddForce(player.transform.forward * speed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
