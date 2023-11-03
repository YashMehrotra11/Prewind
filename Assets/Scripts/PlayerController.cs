using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
                if(CanMove(Vector3.up))StartCoroutine(MovePlayer(Vector3.up));
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (CanMove(Vector3.left)) StartCoroutine(MovePlayer(Vector3.left));
            }
            if (Input.GetKey(KeyCode.S))
            {
                if (CanMove(Vector3.down)) StartCoroutine(MovePlayer(Vector3.down));
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (CanMove(Vector3.right)) StartCoroutine(MovePlayer(Vector3.right));
            }
        }
    }

    private bool CanMove(Vector3 direction)
    {
        Vector2 playerPosition = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(playerPosition, direction, direction.magnitude, collisionLayer);

        if (hit.collider != null && hit.collider.CompareTag("Collision")) // Change the tag to match your Tilemap colliders
        {
            Debug.Log("Boundary hit!");
            Debug.DrawRay(playerPosition, direction, Color.green);
            return false;
        }

        return true;
    }

    private IEnumerator MovePlayer(Vector3 direction)
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
            RoomOneManager.Instance.Rewind();
        }
    }
}
