using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public GameObject bladeTrailPrefab;
    public float minimumCuttingVelocity = 0.001f;

    bool isCutting = false;
    Rigidbody2D rigidBody;
    GameObject currentBladeTrail;
    Vector2 previousPosition;
    CircleCollider2D circleCollider;
    Camera cam;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        cam = Camera.main;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCutting();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopCutting();
        }

        if(isCutting)
        {
            UpdateCut();
        }
    }

    private void UpdateCut()
    {
        Vector2 newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        rigidBody.position = newPosition;

        float velocity = (newPosition - previousPosition).magnitude * Time.deltaTime;
        if(velocity > minimumCuttingVelocity)
        {
            //circleCollider.enabled = true;
        }
        else
        {
            //circleCollider.enabled = false;
        }
        previousPosition = newPosition;
    }

    private void StartCutting()
    {
        isCutting = true;
        currentBladeTrail = Instantiate(bladeTrailPrefab, transform);
        previousPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        //circleCollider.enabled = false;
        circleCollider.enabled = true;
    }

    private void StopCutting()
    {
        isCutting = false;
        currentBladeTrail.transform.SetParent(null);
        Destroy(currentBladeTrail, 2f);
        circleCollider.enabled = false;
    }
}
