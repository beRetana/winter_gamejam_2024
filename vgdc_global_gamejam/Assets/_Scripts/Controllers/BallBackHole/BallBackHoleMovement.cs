using UnityEngine;

public class BallBackHoleMovement : MonoBehaviour
{
    private const string WALL_TAG = "Wall";

    [SerializeField] private float speed;

    private int direction = 1;
    
    private void Update()
    {
        transform.position += speed * Time.deltaTime * direction * Vector3.right;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(WALL_TAG))
        {
            direction = -direction;
        }
    }
}
