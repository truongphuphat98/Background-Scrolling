using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSpriteScrolling : MonoBehaviour {

    [Range(1,5)]
    [SerializeField]
    private float scrollSpeed = 1f;

    private float rightEdge;
    private float leftEdge;
    private Vector3 distanceBetweenEdges;

    private void Start()
    {
        CalculateEdges();

        distanceBetweenEdges = new Vector3(rightEdge - leftEdge, 0f, 0f);
    }

    private void CalculateEdges()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        rightEdge = transform.position.x + spriteRenderer.bounds.extents.x / 3f;
        leftEdge = transform.position.x - spriteRenderer.bounds.extents.x / 3f;
    }

    private void Update()
    {
        transform.localPosition += scrollSpeed * Vector3.right * Time.deltaTime;

        if (PassedEdge())
        {
            MoveRightSpriteToOppositeEdge();
        }
    }


    private bool PassedEdge()
    {
        return scrollSpeed > 0 && transform.position.x > rightEdge ||
            scrollSpeed < 0 && transform.position.x < leftEdge;
    }


    private void MoveRightSpriteToOppositeEdge()
    {
        if (scrollSpeed > 0)
            transform.position -= distanceBetweenEdges;
        else
            transform.position += distanceBetweenEdges;
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.DrawCube(new Vector3(rightEdge, 0f, 0f), new Vector3(0.1f, 2f, 0.1f));
            Gizmos.DrawCube(new Vector3(leftEdge, 0f, 0f), new Vector3(0.1f, 2f, 0.1f));
        }
    }
}