using System.Collections;
using System.Collections.Generic;
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
        this.rigidBody = GetComponent<Rigidbody2D>();
        this.startingPosition = this.transform.position;
    }

    private void Start() 
    {
        ResetState();
    }

    public void ResetState()
    {
        // Setting directions after dying

        this.speedMultiplier = 1.0f;
        this.direction = this.initialDirection;
        this.nextDirection = Vector2.zero;
        this.transform.position = this.startingPosition;
        this.rigidBody.isKinematic = false;
        this.enabled = true;
    }

    private void Update() 
    
    {
        // Continuously try to set the direction every frame
        if (this.nextDirection != Vector2.zero)    
        {
            SetDirection(this.nextDirection);
        }
    }

    private void FixedUpdate() 
    {
        // Specifying movement speed / translation
        Vector2 position = this.rigidBody.position;
        Vector2 translation = this.direction * this.speed * this.speedMultiplier * Time.fixedDeltaTime;

        this.rigidBody.MovePosition(position + translation);

    }

    public void SetDirection(Vector2 direction, bool forced = false)
    {
        // Logic to queue up the direction movement, to be responsive like Pacman is
        if (forced || !Occupied(direction))
        {
            this.direction = direction;
            this.nextDirection = Vector2.zero;
        }

        else
        {
            this.nextDirection = direction;
        }
    }

    public bool Occupied(Vector2 direction)
    {
        // Boxcast so for it to check the entire space. Vector2.one * scale, angle, direction, distance, check boxcast only on obstacle layer
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f, this.obstacleLayer);
        return hit.collider != null;
    }
}

