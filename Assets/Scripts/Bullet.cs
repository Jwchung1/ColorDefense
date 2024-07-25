using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject target;
    public float damage;
    public float explosionRadius = 0f;
    public float slowRate = 0f;
    public float speed = 70f;
    public float plazmaRadius = 0f;
    public GameObject plazmaField;
    public GameObject hitEffect;
    public AudioClip explodeClip;
    public AudioClip slowClip;
    public void Seek(GameObject _target, float _damage)
    {
        target = _target;
        damage = _damage;
    }
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.transform.position - transform.position;
        // 한 프레임 동안 간 거리
        float distanceThisFrame = speed * Time.deltaTime;
        
        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

    }
    void HitTarget()
    {
        // 총알 깨지는 이펙트와 총알 오브젝 삭제        
        GameObject effectIns = (GameObject)Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2.0f);
        Destroy(gameObject);
        // 총알 종류에 따른 데미지 메커니즘 적용
        if(explosionRadius > 0f)
        {
            Explode();
        }
        else if(slowRate > 0f)
        {
            SlowDown();
        }
        else if(plazmaRadius > 0f)
        {
            Plazma();
        }
        else
        {
            Damage(target.GetComponent<Enemy>());
        }


    }
    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in colliders)
        {
            if(collider.CompareTag("Enemy"))
            {
                Damage(collider.GetComponent<Enemy>());
            }
        }
        SoundManager.instance.SFXPlay("Explode", explodeClip);
    }
    void SlowDown()
    {
        target.GetComponent<Enemy>().GetSlowDown(slowRate);
        Damage(target.GetComponent<Enemy>());
        SoundManager.instance.SFXPlay("Slow", slowClip);
    }
    void Plazma()
    {
        plazmaField.GetComponent<PlazmaField>().SetPlazmaDamage(damage);
        Instantiate(plazmaField, transform.position, transform.rotation);
    }
    
    void Damage(Enemy enemy)
    {
        enemy.GetDamage(damage);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
