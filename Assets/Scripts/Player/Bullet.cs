using Mono.Cecil.Cil;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public Player player;
    [SerializeField] GameObject FirePos;
    [SerializeField] GameObject MuzzleFlashObj;
    [SerializeField] GameObject HitEffect;
    [SerializeField] float speed;
    [SerializeField] LayerMask HitLayer;
    [SerializeField] string[] hitLayers;

    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        FirePos = GameObject.Find("FirePosition");
        Instantiate(MuzzleFlashObj,transform.position, Quaternion.identity);
        rb.AddForce(FirePos.transform.forward * speed, ForceMode.Impulse);
        Destroy(gameObject, 4f);
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (string layerName in hitLayers)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(layerName))
            {
                Destroy(gameObject);
                return;
            }
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            var enemy = other.GetComponent<EnemyClass>();
            enemy.CurrentHP -= player.weapon.Damage;
            Instantiate(HitEffect,enemy.HitPos.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
