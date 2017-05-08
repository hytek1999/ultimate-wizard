using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CursorController : MonoBehaviour {
    public float cursorSpeed = 1.0f;
    private const float minCursorX = TilePosition.TileOriginX;
    private const float maxCursorX = TilePosition.TileOriginX + TilePosition.TileWidth; 
    private const float minCursorY = TilePosition.TileOriginY + 1;
    private const float maxCursorY = TilePosition.TileOriginY + TilePosition.TileHeight;

    public TilePosition tilePosition;

    private Vector3 targetPosition;
    private bool isMoving;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        tilePosition = TilePosition.FromScreenCoords(transform.position);
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
            targetPosition.x = Mathf.Clamp(targetPosition.x, minCursorX, maxCursorX);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minCursorY, maxCursorY);
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
        tilePosition = TilePosition.FromScreenCoords(targetPosition);
    }
}
