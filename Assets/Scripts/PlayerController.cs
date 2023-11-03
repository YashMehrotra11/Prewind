using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private bool isMoving;
    private Vector3 originalPos, targetPos;
    private float timeToMove = 0.2f;

    public LayerMask collisionLayer;

    void Update()
    {
        if (!isMoving)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if(CanMove(Vector3.up))StartCoroutine(Move(Vector3.up));
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (CanMove(Vector3.left)) StartCoroutine(Move(Vector3.left));
            }
            if (Input.GetKey(KeyCode.S))
            {
                if (CanMove(Vector3.down)) StartCoroutine(Move(Vector3.down));
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (CanMove(Vector3.right)) StartCoroutine(Move(Vector3.right));
            }
        }
    }

    private bool CanMove(Vector3 direction)
    {
        Vector2 playerPosition = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(playerPosition, direction, direction.magnitude, collisionLayer);
        Debug.DrawRay(playerPosition, direction, Color.green);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Collision"))
            {
                return false;
            }
            else if (hit.collider.CompareTag("Moveable"))
            {
                if(hit.collider.gameObject.GetComponent<MoveableController>().CanMove(direction, collisionLayer))
                {
                    hit.collider.gameObject.GetComponent<MoveableController>().StartMove(direction);
                }
                else
                {
                    return false;
                }
            }
        }

        return true;
    }

    private IEnumerator Move(Vector3 direction)
    {
        isMoving = true;

        float elapsedTime = 0;

        originalPos = transform.position;
        targetPos = originalPos + (direction);

        while(elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(originalPos, targetPos, elapsedTime / timeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Exit"))
        {
            RoomManager.Instance.Exit();
        }
    }
}
