using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Attributes")]
    public string towerColor;
    public float range;
    public float damage;
    private float originalDamage;
    public float fireRate = 1f;
    public bool isShotgun = false;
    [Header("Laser Tower")]
    public bool isLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    private float fireCountdown = 0f;
    [Header("Buff Tower")]
    public bool isBuff = false;
    public float buffRate;
    public GameObject buffEffect;
    [Header("SFX")]
    public AudioClip shootClip;
    public AudioClip lazerClip;
    public AudioClip canonClip;
    public AudioClip buffClip;
    public AudioClip elecClip;

    [Header("Unity Setup Fields")]
    private float turnSpeed = 10f;
    private GameObject target;
    public Transform partToRotate;
    public GameObject bulletPrefab;
    public Transform firePoint;
    // Start is called before the first frame update
    void Start()
    {
        originalDamage = damage;
        // 1초마다 타겟을 업데이트
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        // 버프타워면 6초마다 버프 쏨
        if(isBuff)
            InvokeRepeating("GiveBuff", 0.2f, 5.2f);
    }
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float minDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(enemy.transform.position, transform.position);
            if(distanceToEnemy < minDistance)
            {
                minDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }

            if(nearestEnemy != null && minDistance <= range)
            {
                target = nearestEnemy;
            }
            else
            {
                target = null;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(isBuff)
        {
            return;
        }
        if(!PlayManager.isGameOver)
        {
            if(target == null){
                if(isLaser)
                {
                    if(lineRenderer.enabled)
                    {
                        lineRenderer.enabled = false;
                        impactEffect.Stop();
                    }
                }
                return;
            }

            LockOnTarget();

            if(isLaser)
            {
                Laser();
            }
            else
            {
                if(fireCountdown <= 0f)
                {
                    Shoot();
                    fireCountdown = 1f / fireRate;
                }
                fireCountdown -= Time.deltaTime;
            }

        }
    }
    void LockOnTarget()
    {
        Vector3 direction = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    void Laser()
    {
        //SoundManager.instance.SFXPlay("Lazer", lazerClip);
        target.GetComponent<Enemy>().GetDamage(damage * Time.deltaTime);
        if(!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
        }
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.transform.position);

        Vector3 dir = firePoint.position - target.transform.position;

        impactEffect.transform.position = target.transform.position + dir.normalized * 0.5f;

        impactEffect.transform.position = target.transform.position;    
    }
    public float SGAngle;
    void Shoot()
    {
        SoundManager.instance.SFXPlay("Canon", canonClip);
        if(isShotgun)
        {
            GameObject bullet1 = (GameObject)Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(firePoint.rotation.eulerAngles.x, firePoint.rotation.eulerAngles.y - 2*SGAngle, firePoint.rotation.eulerAngles.z));
            GameObject bullet2 = (GameObject)Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(firePoint.rotation.eulerAngles.x, firePoint.rotation.eulerAngles.y - SGAngle, firePoint.rotation.eulerAngles.z));
            GameObject bullet3 = (GameObject)Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(firePoint.rotation.eulerAngles.x, firePoint.rotation.eulerAngles.y, firePoint.rotation.eulerAngles.z));
            GameObject bullet4 = (GameObject)Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(firePoint.rotation.eulerAngles.x, firePoint.rotation.eulerAngles.y + SGAngle, firePoint.rotation.eulerAngles.z));
            GameObject bullet5 = (GameObject)Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(firePoint.rotation.eulerAngles.x, firePoint.rotation.eulerAngles.y + 2*SGAngle, firePoint.rotation.eulerAngles.z));

        }
        else
        {
            GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bullet = bulletGo.GetComponent<Bullet>();

            if(bullet != null)
                bullet.Seek(target, damage);
        }
    }
    void GiveBuff()
    {
        SoundManager.instance.SFXPlay("Buff", buffClip);
        // 타워 범위에 들어오는 타워들 버프해줌
        GameObject effectIns = (GameObject)Instantiate(buffEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2.0f);
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach(Collider collider in colliders)
        {
            if(collider.CompareTag("Tower"))
            {
                collider.GetComponent<Tower>().GetDamageBuff(buffRate);
            }
        }
    }
    void GetDamageBuff(float rate)
    {
        damage *= 1 + rate/100;
        StartCoroutine("RecoverBuff");
    }
    IEnumerator RecoverBuff()
    {
        yield return new WaitForSeconds(5);
        damage = originalDamage;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
