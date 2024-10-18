using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy Instance;// { get; private set; }
    [SerializeField] private GameObject explosion;
    [SerializeField] private Collider2D []colliders;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnBecameVisible()
    {
        gameObject.GetComponent<Animator>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement.Instance.GetHit();
        }

        if (other.gameObject.CompareTag("Attack"))
        {
            foreach (var item in colliders)
            {
                item.enabled = false;
            }
            
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Animator>().enabled = false;
            StartExplosion();
            Destroy(gameObject, 0.6f);
        }
    }

    public void StartExplosion()
    {
        explosion.GetComponent<SpriteRenderer>().enabled = true;
        explosion.GetComponent<Animator>().enabled = true;
    }
}
