using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    private List<Fighter> fighters = new List<Fighter>();
    [SerializeField]
    private int requieredFighters = 2;
    [SerializeField]
    private float secondsBetweenAttacks = 1f;
    [SerializeField]
    private float secondstoStartBattle = 1f;
    [SerializeField]
    public UnityEvent onBattleStart;
    [SerializeField]
    private UnityEvent onBattleStop;
    private int currentFightIndex = 0;
    public bool isBattleActive;
    

    public void AddFighter(Fighter fighter)
    {
        fighters.Add(fighter);
        CheckFighters();
    }
    public void RemoveFighter(Fighter fighter)
    {
        fighters.Remove(fighter);
        CheckFighters();
    }
    private void CheckFighters()
    {
        if (fighters.Count < requieredFighters)
        {
            StopBattle(); //Debug.Log("All fighters are out of the battle.");
        }
        else
        {
            StartBattle();
        }
    }
    private void StartBattle()
    {
        isBattleActive = true;
        onBattleStart?.Invoke();
        StartCoroutine(Attack());
    }
    private IEnumerator Attack()
    {
        currentFightIndex = Random.Range(0, fighters.Count);
        Fighter attacker = fighters[currentFightIndex];
        Fighter defender;
        do
        {
            currentFightIndex = Random.Range(0,fighters.Count);
            defender = fighters[currentFightIndex];
        }
        while (attacker == defender);

        attacker.Attack();
        float damage = attacker.GetDamage();
        defender.GetComponent<Health>().TakeDamage(damage);

        yield return new WaitForSeconds(secondsBetweenAttacks);
        if (defender.GetComponent<Health>().CurrentHealth > 0)
        {
            StartCoroutine(Attack());
        }
        else
        {
            StopBattle();
        }
    }
    private void StopBattle()
    {
        StopCoroutine(Attack());
        onBattleStop?.Invoke();
    }
}
