using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float speed = 8.0f;
    public float speedMultiplier = 1.0f;
    public Vector2 initialDirection;
    public LayerMask obstacleLayer;
    public new Rigidbody2D rigidBody { get; private set; }
    public Vector2 direction { get; private set; }
    public Vector2 nextDirection { get; private set; }
    public Vector3 startingPosition { get; private set; }

    private void Awake()
    {
        // Set direction on new game
        rigidBody = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
    }

    private void Start() 
    {
        ResetState();
    }

    public void ResetState()
    {
        // Setting directions after dying

        speedMultiplier = 1.0f;
        direction = initialDirection;
        nextDirection = Vector2.zero;
        transform.position = startingPosition;
        rigidBody.isKinematic = false;
        enabled = true;
    }

    private void Update() 
    
    {
        // Continuously try to set the direction every frame
        if (nextDirection != Vector2.zero)    
        {
            SetDirection(nextDirection);
        }
    }

    private void FixedUpdate() 
    {
        // Specifying movement speed / translation
        Vector2 position = rigidBody.position;
        Vector2 translation = direction * speed * speedMultiplier * Time.fixedDeltaTime;

        rigidBody.MovePosition(position + translation);

    }

    public void SetDirection(Vector2 direction, bool forced = false)
    {
        // Logic to queue up the direction movement, to be responsive like Pacman is
        if (forced || !Occupied(direction))
        {
            this.direction = direction;
            nextDirection = Vector2.zero;
        }

        else
        {
            nextDirection = direction;
        }
    }

    public bool Occupied(Vector2 direction)
    {
        // Boxcast so for it to check the entire space. Vector2.one * scale, angle, direction, distance, check boxcast only on obstacle layer
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f, obstacleLayer);
        return hit.collider != null;
    }
}

