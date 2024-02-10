using UnityEngine;

public class Follow2D : MonoBehaviour
{
    private Transform target;

    [SerializeField] private Vector3 pose;
    [SerializeField] private float speed = 0.1f;

    private Vector3 velocity;

    private void Awake()
    {
        target = FindObjectOfType<Player>().transform;        
    }

    private void Start()
    {
        pose = target.position;
    }

    private void FixedUpdate()
    {
        pose.x = Mathf.SmoothDamp(pose.x, target.position.x, ref velocity.x, speed);
        pose.y = Mathf.SmoothDamp(pose.y, target.position.y, ref velocity.y, speed);
        transform.position = pose;
    }
}
