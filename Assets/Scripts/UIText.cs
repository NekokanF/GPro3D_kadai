using TMPro;
using UnityEngine;

public class UIText : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] TMP_Text BulletAmountText;
    [SerializeField] TMP_Text MagazineAmountText;
    [SerializeField] TMP_Text HPAmountText;
    [SerializeField] GameObject ReloadObj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        BulletAmountText.text = ((int)player.weapon.CurrentBulletAmount + " / ").ToString() + ((int)player.weapon.BaseBulletAmount).ToString();
        MagazineAmountText.text = ((int)player.weapon.CurrentMagazineAmount + " / ").ToString() + ((int)player.weapon.BaseMagazineAmount).ToString();
        HPAmountText.text = ((int)player.CurrentHP + " / ").ToString() + ((int)player.status.BaseHP).ToString();

        if (player.OnReload)
        {
            ReloadObj.SetActive(true);
        }
        else
        {
            ReloadObj.SetActive(false);
        }
    }
}
