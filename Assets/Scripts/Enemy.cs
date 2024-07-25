using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    [Header("Enemy Stat")]
    public float speed = 10f;
    private float originalSpeed;
    public float maxHp;
    public int dropGold;
    public GameObject dieEffect;
    private float currentHp;
    private Transform target;
    private int wayPointIdx = 0;

    [Header("Unity Stuff")]
    public Image healthBar;

    void Start()
    {
        target = Waypoints.points[wayPointIdx];
        currentHp = maxHp;
        originalSpeed = speed;
    }
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            GetNextWayPoint();
        }
    }
    void GetNextWayPoint()
    {
        if(wayPointIdx >= Waypoints.points.Length - 2)
        {
            Destroy(gameObject);
            SpawnManager.instance.enemiesAlive--;
            PlayManager.instance.LoseLife();
        }
        wayPointIdx++;
        target = Waypoints.points[wayPointIdx];
    }
    public void GetDamage(float damage)
    {
        currentHp -= damage;
        healthBar.fillAmount = currentHp / maxHp;

        if(currentHp <= 0)
        {
            Die();
        }
    }
    public void GetSlowDown(float rate)
    {
        speed *= rate/100;
        StartCoroutine("RecoverTimer");
    }
    IEnumerator RecoverTimer()
    {
        yield return new WaitForSeconds(3);
        speed = originalSpeed;
    }
    public void Die()
    {
        Destroy(gameObject);
        // 죽는 이펙트 추가
        GameObject effectIns = (GameObject)Instantiate(dieEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2.0f);
        // 재화 주는거 추가
        PlayManager.instance.EarnGold(dropGold);
        SpawnManager.instance.enemiesAlive--;
    }
}
