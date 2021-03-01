using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject fruitSlicedPrefab;
    public float startForce = 15f;

    Vector3 previousPosition;
    Rigidbody2D rigidBody;
    
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.AddForce(transform.up * startForce, ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Blade")
        {
            //Vector3 direction = (collision.transform.position - transform.position).normalized;
            Vector3 direction = (collision.transform.position - (Input.mousePosition - previousPosition).normalized).normalized;
            var currentPosition = Quaternion.LookRotation(direction);
            Debug.Log($"direction: {direction}, alternative direction: {(collision.transform.position - transform.position).normalized}");

            var slicedFruit = Instantiate(fruitSlicedPrefab, transform.position, currentPosition);
            Destroy(gameObject);
            Destroy(slicedFruit, 3f);
        }
    }

    private void Update()
    {
        previousPosition = Input.mousePosition;
    }
}
