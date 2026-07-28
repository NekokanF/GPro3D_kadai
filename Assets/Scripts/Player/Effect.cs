using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField] float lifespan;
    // Update is called once per frame
    void Update()
    {
        Invoke("Destroy", lifespan);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
