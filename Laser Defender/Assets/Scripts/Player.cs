using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 200;


    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float ProjectileFiringPeriod = 0.15f;

    [Header("Sounds")]
    [SerializeField] AudioClip dyingSound;
    [SerializeField] [Range(0, 1)] float dyingVolume = 0.3f;
    [SerializeField] AudioClip shootingSound;
    [SerializeField] [Range(0, 1)] float shootingVolume = 0.3f;


    Coroutine firingCoroutine;

    float xMin, xMax, yMin, yMax; 

	// Use this for initialization
	void Start () {
        SetUpBoundries();
	}

    // Update is called once per frame
    void Update () {
        Move();
        Fire();
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) return;
        health -= damageDealer.GetDamage();
        AudioSource.PlayClipAtPoint(dyingSound, Camera.main.transform.position, dyingVolume);
        damageDealer.Hit();
        if (health <= 0) Die();
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(dyingSound, Camera.main.transform.position, dyingVolume);
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
    }

    private void Fire()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
            StopCoroutine(firingCoroutine);           
    }

    public int GetHealth()
    {
        return health;
    }

    IEnumerator FireContinuously()
    {
        while(true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootingSound, Camera.main.transform.position, shootingVolume);
            yield return new WaitForSeconds(ProjectileFiringPeriod);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newPosX = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newPosY = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newPosX, newPosY);
    }
    private void SetUpBoundries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}
