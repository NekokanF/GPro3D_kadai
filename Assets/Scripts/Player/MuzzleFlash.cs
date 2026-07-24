using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Invoke("Destroy", 0.2f);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
