using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ¹«±â¸¦ È¹µæÇÏ¸é È¹µæÇÑ ¹«±â¸¦ UI¿¡ Ç¥½Ã
/// ÇöÀç ÀÜÅº·®°ú ÀüÃ¼ ÃÑ¾Ë·®À» Ãâ·Â
/// </summary>
public class WeaponUIManager : MonoBehaviour
{
    public Color bulletColor = Color.white;
    public Color emptyBulletColor = Color.black;
    private Color noBulletColor;

    [SerializeField] private Image weaponHUD;
    [SerializeField] private GameObject bulletMag;
    [SerializeField] private Text totalBulletsHUD;

    // Start is called before the first frame update
    void Start()
    {
        noBulletColor = new Color(0f, 0f, 0f, 0f);

        if (weaponHUD == null)
        {
            weaponHUD = transform.Find("WeaponHUD/Weapon").GetComponent<Image>();
        }

        if (bulletMag == null)
        {
            bulletMag = transform.Find("WeaponHUD/Data/Mag").gameObject;
        }

        if (totalBulletsHUD == null)
        {
            totalBulletsHUD = transform.Find("WeaponHUD/Data/bulletAmount").GetComponent<Text>();
        }

        Toggle(false);
    }

    public void Toggle(bool active)
    {
        weaponHUD.transform.parent.gameObject.SetActive(active);
    }

    public void UpdateWeaponHUD(Sprite weaponSprite, int bulletLeft, int fullMag, int ExtraBullets)
    {
        if (weaponSprite != null && weaponHUD.sprite != weaponSprite)
        {
            weaponHUD.sprite = weaponSprite;
            weaponHUD.type = Image.Type.Filled;
            weaponHUD.fillMethod = Image.FillMethod.Horizontal;
        }

        int bulletCount = 0;

        foreach (Transform bullet in bulletMag.transform)
        {
            // ÀÜÅº
            if (bulletCount < bulletLeft)
            {
                bullet.GetComponent<Image>().color = bulletColor;
            }
            else if (bulletCount >= fullMag)
            {
                bullet.GetComponent<Image>().color = noBulletColor;
            }
            else
            {
                bullet.GetComponent<Image>().color = emptyBulletColor;
            }

            bulletCount++;
        }
        totalBulletsHUD.text = bulletLeft + "/" + ExtraBullets;
    }
}
