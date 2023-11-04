using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint mNextWaypoint;
    private Vector3 _mPosition;
    // Start is called before the first frame update
    void Start()
    {
        _mPosition = transform.position;
    }

    public float GetDistance(Vector3 characterPosition)
    {
        return Vector3.Distance(_mPosition, characterPosition);
    }

    public Vector3 GetDirection(Vector3 characterPosition)
    {
        Vector3 heading = _mPosition - characterPosition;
        return heading / heading.magnitude;
    }

    public Waypoint GetNextWaypoint()
    {
        return mNextWaypoint;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
