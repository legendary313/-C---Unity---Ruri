using UnityEngine;
using UnityEngine.UI;

public class UIEnemyManager : MonoBehaviour
{
    [SerializeField] private Transform enemy;
    [SerializeField] private RectTransform imageHP;
    [SerializeField] private Image fillHP;
    public float hp_cur;
    public float hp_total;
    private void Update()
    {
        UpdateUIEnemy();
        if (enemy.localScale.x < 0)
            imageHP.localScale = new Vector3(Mathf.Abs(imageHP.localScale.x) * (-1f), imageHP.localScale.y, imageHP.localScale.z);
        else
            imageHP.localScale = new Vector3(Mathf.Abs(imageHP.localScale.x), imageHP.localScale.y, imageHP.localScale.z);
    }
    private void UpdateUIEnemy()
    {
        if (hp_cur > 0)
        {
            fillHP.fillAmount = hp_cur / hp_total;
            if (fillHP.fillAmount > 0.3f) fillHP.color = Color.green;
            else fillHP.color = Color.red;
        }
        if (hp_cur <= 0)
        {
            fillHP.fillAmount = 0f;
        }
    }
}
