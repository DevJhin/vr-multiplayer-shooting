using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    [SerializeField] Transform origin;
    [SerializeField] float maxDistance = 100f;
    [SerializeField] LayerMask layerMask;



    [SerializeField] Gradient detectColor;
    [SerializeField] Gradient normalColor;

    LineRenderer lineRenderer;
    Transform target;
    Vector3 hitPoint;

    Vector3 offset;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;

        offset = origin.position - transform.position;
    }

    void LateUpdate()
    {
        RaycastHit hit;

        lineRenderer.SetPosition(0, transform.position);
        if (Physics.Raycast(origin.position, origin.forward, out hit, maxDistance, layerMask))
        {
            target = hit.transform;
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                lineRenderer.colorGradient = detectColor;
            }
            else
            {
                lineRenderer.colorGradient = normalColor;
            }
            hitPoint = hit.point;
            lineRenderer.SetPosition(1, hit.point + offset);

        }
        else
        {
            target = null;
            lineRenderer.colorGradient = normalColor;
            hitPoint = transform.position + transform.forward * maxDistance;
            lineRenderer.SetPosition(1, hitPoint);
        }
    }
}