using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : MonoBehaviour
{
    public float damage = 20f;
    public float speed = 70f;
    public GameObject hitEffect;
    void Update()
    {
        gameObject.transform.Translate(Vector3.forward * Time.deltaTime * speed);

    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            HitTarget(other.gameObject);
        }
    }
    
    void HitTarget(GameObject target)
    {
        // 총알 깨지는 이펙트와 총알 오브젝 삭제
        GameObject effectIns = (GameObject)Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2.0f);
        Destroy(gameObject);
        // enemy에 damage 주는 코드
        Enemy enemyStatus = target.GetComponent<Enemy>();
        enemyStatus.GetDamage(damage);
        return;
    }
}
