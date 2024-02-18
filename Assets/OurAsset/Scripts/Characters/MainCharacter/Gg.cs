using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gg : MonoBehaviour
{
    // Start is called before the first frame update
    public int MaxHp = 100;
    public int Hp;
    public HealthBar healthbar;
    void Start()
    {
        Hp = MaxHp;
        healthbar.SetMaxHealthbar(MaxHp);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }
    void TakeDamage(int damage)
    {
        Hp -= damage;
        healthbar.SetHealth(Hp);
    }
}
