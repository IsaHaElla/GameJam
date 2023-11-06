using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public const float TOLERANCE = 0.5F;
    
    private Rigidbody _rigid;
    private Transform _currentPoint;
    
    [SerializeField] private float movementSpeed = 3.0f;
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;
    
    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _currentPoint = pointB.transform;
    }

    private void Update()
    {
        HandleEnemyMovement();
        HandlePointFlip();
    }

    private void HandleEnemyMovement()
    {
        var direction = _currentPoint == pointB.transform ? +1 : -1;
        _rigid.velocity = new Vector2(movementSpeed * direction, 0);
    }

    private void HandlePointFlip()
    {
        var distance = Vector2.Distance(transform.position, _currentPoint.position);
        var closeEnough = distance < TOLERANCE;
        var isPointA = _currentPoint == pointA.transform;
        
        if (closeEnough && !isPointA)
            _currentPoint = pointA.transform;
        if (closeEnough && isPointA) 
            _currentPoint = pointB.transform;
    }
    
    private void OnDrawGizmos()
    {
        var posPointA = pointA.transform.position;
        var posPointB = pointB.transform.position;
        
        Gizmos.DrawWireSphere(posPointA, 0.5f);
        Gizmos.DrawWireSphere(posPointB, 0.5f);
        Gizmos.DrawLine(posPointA, posPointB);
    }
}

