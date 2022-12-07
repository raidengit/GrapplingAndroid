using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{

    public Camera cam;
    public LineRenderer lr;

    public LayerMask grappleMask;
    public float moveSpeed = 2;
    public float grappleLength = 10;

    public int maxPoints = 3;

    private Rigidbody2D rig;
    private List<Vector2> points = new List<Vector2>();

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        lr.positionCount = 0;
    }

    void Update()
    {
        //Input.GetMouseButtonDown(0)
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, grappleLength, grappleMask);
                if (hit.collider != null)
                {
                    Vector2 hitPoint = hit.point;
                    points.Add(hitPoint);
                    if (points.Count > maxPoints)
                    {
                        points.RemoveAt(0);
                    }
                }
                print("Touch begans");
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                print("Touch Ended");
            }

            
        }

        if (points.Count > 0)
        {
            Vector2 moveTo =centroid(points.ToArray());

            rig.MovePosition(Vector2.MoveTowards(transform.position, moveTo, moveSpeed * Time.deltaTime));

            lr.positionCount = 0;
            lr.positionCount = points.Count * 2;
            for (int n=0, j=0; n < points.Count *2; n+=2, j++)
            {
                lr.SetPosition(n, transform.position);
                lr.SetPosition(n + 1, points[j]);
            }
        }

        if (Input.touchCount == 2)
        {
            Detatch();
        }
    }

    void Detatch()
    {
        lr.positionCount = 0;
        points.Clear();
    }

    Vector2 centroid(Vector2[] points)
    {
        Vector2 center = Vector2.zero;
        foreach (Vector2 point in points)
        {
            center += point;
        }
        center /= points.Length;
        return center;
    }
}
