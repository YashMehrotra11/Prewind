using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isMoving;
    private Vector3 originalPos, targetPos;
    private float timeToMove = 0.2f;

    void Update()
    {
        if (!isMoving)
        {
            if (Input.GetKey(KeyCode.W))
            {
                StartCoroutine(MovePlayer(Vector3.up));
            }
            if (Input.GetKey(KeyCode.A))
            {
                StartCoroutine(MovePlayer(Vector3.left));
            }
            if (Input.GetKey(KeyCode.S))
            {
                StartCoroutine(MovePlayer(Vector3.down));
            }
            if (Input.GetKey(KeyCode.D))
            {
                StartCoroutine(MovePlayer(Vector3.right));
            }
        }
    }

    private bool CanMove()
    {
        return false;
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
            GameManager.Instance.Rewind();
        }
    }
}
