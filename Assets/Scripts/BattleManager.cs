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
    [SerializeField]
    private UnityEvent onBattleEnd;
    [SerializeField]
    private UnityEvent<string> onFigtherWins;

    private int currentFightIndex = 0;
    public bool isBattleActive = false;
    private Coroutine attackCoroutine;
    

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
            Invoke ("StartBattle", secondstoStartBattle);
        }
    }
    private void StartBattle()
    {
        if (isBattleActive || fighters.Count < requieredFighters)
        {
            return;
        }
        isBattleActive = true;
        onBattleStart?.Invoke();
        attackCoroutine = StartCoroutine(Attack());
    }
    private IEnumerator Attack()
    {
        if (!isBattleActive)
        {
            yield break;
        }
        currentFightIndex = Random.Range(0, fighters.Count);
        Fighter attacker = fighters[currentFightIndex];
        Fighter defender;
        do
        {
            currentFightIndex = Random.Range(0,fighters.Count);
            defender = fighters[currentFightIndex];
        }
        while (attacker == defender);

        attacker.transform.LookAt(defender.transform.position);
        defender.transform.LookAt(attacker.transform.position);

        attacker.Attack();
        yield return new WaitForSeconds (attacker.AttackDuration);
        float damage = attacker.GetDamage();
        defender.GetComponent<Health>().TakeDamage(damage);

        yield return new WaitForSeconds(secondsBetweenAttacks);
        if (defender.GetComponent<Health>().CurrentHealth > 0)
        {
            attackCoroutine = StartCoroutine(Attack());
        }
        else
        {
            BattleFinish(attacker.FigtherName);
        }
    }
    private void BattleFinish(string winnerName)
    {
        StopBattle();
        onBattleEnd?.Invoke();
        onFigtherWins?.Invoke(winnerName);
    }
    private void StopBattle()
    {
        isBattleActive = false;
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
        onBattleStop?.Invoke();
    }
}
