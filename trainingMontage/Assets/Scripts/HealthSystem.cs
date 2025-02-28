using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; //don't miss that you need this for UnityEvent
public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHp = 3;
    [SerializeField] private int hp = 3;

    [SerializeField] private UnityEvent OnDamaged;
    [SerializeField] private UnityEvent OnZero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int hpAmount)
    {
        hp -= hpAmount;

        OnDamaged?.Invoke();

        if (hp <= 0)
        {
            OnZero?.Invoke();
        }
    }
}
