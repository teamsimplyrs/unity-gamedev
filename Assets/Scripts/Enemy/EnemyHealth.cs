using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    private int maxHeartAmount = 5;
    public int startHearts = 3;
    public int curHealth;
    private int maxHealth;
    private int healthPerHeart = 4;
    [SerializeField] GameObject healthBar;


    // Start is called before the first frame update
    void Start()
    {
        curHealth = startHearts * healthPerHeart;
        maxHealth = maxHeartAmount * healthPerHeart;
        UpdateHealthBar();
    }

    //Checking 0 health every frame might be unnecessary and inefficient 
    private void Update()
    {
        //if (curHealth <= 0)
        //{
        //    Destroy(gameObject);
        //}
    }

    public void TakeDamage(int amount)

    {
        curHealth -= amount;
        if(curHealth <= 0)
        {
            Destroy(gameObject);
        }
        curHealth = Mathf.Clamp(curHealth, 0, startHearts * healthPerHeart);
        UpdateHealthBar();
    }

    public void AddHeartContainer()
    {
        startHearts++;
        startHearts = Mathf.Clamp(startHearts, 0, maxHeartAmount);

    }

    public bool IsAtFullHP()
    {
        return curHealth == startHearts * healthPerHeart;
    }

    void UpdateHealthBar()
    {
        healthBar.GetComponent<Slider>().value = (float)curHealth / ((float)startHearts * healthPerHeart);
    }
}
