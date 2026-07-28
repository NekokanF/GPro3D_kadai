using TMPro;
using UnityEngine;

public class BulletText : MonoBehaviour
{
    [SerializeField] Weapon weapon;
    [SerializeField] TMP_Text BulletAmountText;
    [SerializeField] TMP_Text MagazineAmountText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BulletAmountText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        BulletAmountText.text = ((int)weapon.CurrentBulletAmount + " / ").ToString() + ((int)weapon.BaseBulletAmount).ToString();
        MagazineAmountText.text = ((int)weapon.CurrentMagazineAmount + " / ").ToString() + ((int)weapon.BaseMagazineAmount).ToString();
    }
}
