using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlazmaField : MonoBehaviour
{
    public float plazmaRadius = 5f;
    public float plazmaDamage = 5;
    public int tickTime = 10;
    public AudioClip clip;
    public void SetPlazmaDamage(float damage)
    {
        this.plazmaDamage = damage;

    }
    void Start()
    {
        StartCoroutine("PlazmaDamage");
    }
    IEnumerator PlazmaDamage()
    {
        for(int i=0; i<tickTime; i++)
        {
            SoundManager.instance.SFXPlay("Electron", clip);
            Collider[] colliders = Physics.OverlapSphere(transform.position, plazmaRadius);
            foreach(Collider collider in colliders)
            {
                if(collider.CompareTag("Enemy"))
                {
                    collider.GetComponent<Enemy>().GetDamage(plazmaDamage);
                }
            }
            yield return new WaitForSeconds(.5f);
        }
        Destroy(gameObject);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, plazmaRadius);
    }
}
