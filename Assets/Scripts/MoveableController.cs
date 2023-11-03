using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableController : MonoBehaviour
{
    public List<MoveableController> causalObjects;

    private Vector3 originalPos, targetPos;
    private float timeToMove = 0.2f;

    public bool CanMove(Vector2 direction, LayerMask collisionLayer)
    {
        Vector2 playerPosition = transform.position;
        RaycastHit2D[] hits = Physics2D.RaycastAll(playerPosition, direction, direction.magnitude, collisionLayer);
        Debug.DrawRay(playerPosition, direction, Color.green);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Collision") || (hit.collider.CompareTag("Moveable") && hit.collider.gameObject.name !=  gameObject.name))
                {
                    Debug.Log("BLOCKED");
                    return false;
                }
            }
        }
        return true;
    }

    public void StartMove(Vector3 direction, bool isCausal = false)
    {
        if (!isCausal)
        {
            foreach (MoveableController causalObject in causalObjects)
            {
                causalObject.StartMove(direction, true);
            }
        }
        StartCoroutine(Move(direction));
    }

    private IEnumerator Move(Vector3 direction)
    {
        float elapsedTime = 0;

        originalPos = transform.position;
        targetPos = originalPos + (direction);

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(originalPos, targetPos, elapsedTime / timeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
    }
}
