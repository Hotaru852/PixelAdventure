using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedBallController : MonoBehaviour
{
    [SerializeField] private GameObject Anchor;
    [SerializeField] private float Speed;
    [SerializeField] private float AngleRange;
    [SerializeField] private bool ClockWise;
    [SerializeField] private float ChainDistance;
    [SerializeField] private GameObject Chain;
    [SerializeField] private float StartingAngle;

    private void Start()
    {
        transform.RotateAround(Anchor.transform.position, Vector3.forward, Mathf.Rad2Deg * StartingAngle);
        createChains();
    }

    private void Update()
    {
        if (transform.rotation.z > AngleRange)
        {
            ClockWise = false;
        }

        if (transform.rotation.z < -AngleRange)
        {
            ClockWise = true;
        }

        if (ClockWise) transform.RotateAround(Anchor.transform.position, Vector3.forward, Speed);
        else transform.RotateAround(Anchor.transform.position, Vector3.forward, -Speed);
    }

    private void createChains()
    {
        Vector2 startPos = new Vector2(Anchor.transform.position.x, Anchor.transform.position.y);
        Vector2 endPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 currentPos = startPos;
        Vector2 direction = (endPos - startPos).normalized;

        while ((endPos - startPos).magnitude > (currentPos - startPos).magnitude)
        {
            Instantiate(Chain, currentPos, Quaternion.identity, transform);
            currentPos += direction * ChainDistance;
        }
    }
}
