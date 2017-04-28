using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CursorController : MonoBehaviour {
    public float cursorSpeed = 1.0f; 

    private Vector3 targetPosition;
    private bool isMoving;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        this.AddObserver(OnMoveEvent, InputController.MoveNotification);
        
    }

    void OnDisable()
    {
        this.RemoveObserver(OnMoveEvent, InputController.MoveNotification);
    }

    private void OnMoveEvent(object sender, object e)
    {
        Vector3 moveTo = (Vector3)e;
        if (!isMoving)
        {
            targetPosition = transform.position + moveTo;
            StartCoroutine(MoveCursor());
        }
    }

    private IEnumerator MoveCursor()
    {
        isMoving = true;
        animator.SetBool("isMoving", true);
        Vector3 currentPosition = transform.position;
        while (Mathf.Abs(Vector3.Distance(transform.position, targetPosition)) > 0.1f)
        {
            transform.position = Vector3.Lerp(currentPosition, targetPosition, 1.0f / (cursorSpeed * Time.deltaTime));
            yield return null;
        }
        transform.position = targetPosition;
        animator.SetBool("isMoving", false);
        isMoving = false;
    }
}
