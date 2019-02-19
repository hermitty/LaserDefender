using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] float health = 100;
    [SerializeField] float shootCunter;
    [SerializeField] float minTimeBetweenShoots = 0.2f;
    [SerializeField] float maxTimeBetweenShoots = 3f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = -10f;

    // Use this for initialization
    void Start () {
        shootCunter = UnityEngine.Random.Range(minTimeBetweenShoots, maxTimeBetweenShoots);
	}
	
	// Update is called once per frame
	void Update () {
        CountDownAndShoot();
	}
    private void CountDownAndShoot()
    {
        shootCunter -= Time.deltaTime;
        if (shootCunter <= 0)
        {
            Fire();
            shootCunter = UnityEngine.Random.Range(minTimeBetweenShoots, maxTimeBetweenShoots);

        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) return;
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0) Destroy(gameObject);
    }
}
