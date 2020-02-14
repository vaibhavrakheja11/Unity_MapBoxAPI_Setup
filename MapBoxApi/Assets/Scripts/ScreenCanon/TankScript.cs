﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankScript : MonoBehaviour
{

    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject spawnPlaceHolder;
    [SerializeField] private float fireRate = 5.0f;
    [SerializeField] private Vector3 rotationAxis;
    [SerializeField] private float shotForce = 1500.0f;
    [SerializeField] private float moveSpeed = 0.2f;
    [SerializeField] private float traveltime = 1.0f;
    [SerializeField] private float killHeight = -10.0f;

    
    // Start is called before the first frame update

    void Start()
    {
        StartCoroutine(FireProjectile());
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= killHeight)
        {
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Projectile"))
        {
            StartCoroutine(DestroySelf()); 
        }
        
    }
    private IEnumerator FireProjectile()
    {
        yield return new WaitForSeconds(fireRate);
        while(true)
        {
            GameObject bullet = Instantiate(projectile, spawnPlaceHolder.transform.position, spawnPlaceHolder.transform.rotation);
            bullet.GetComponent<Rigidbody>().velocity = (bullet.transform.up * -1 * shotForce);
            Destroy(bullet, 0.8f);
            yield return new WaitForSeconds(1);

        }
    }
    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(fireRate);
        Destroy(gameObject);
    }
    private IEnumerator Move()
    {
        float time = 0f;
        while (time < traveltime)
        {
            time += Time.deltaTime;
            transform.Translate(transform.forward * -1 * moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.Rotate(rotationAxis, Random.Range(0, 360));
        StartCoroutine(Move());

    }

}
