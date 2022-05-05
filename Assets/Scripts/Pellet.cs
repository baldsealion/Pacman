using UnityEngine;

public class Pellet : MonoBehaviour
{
    public int points = 10;

    // Protected = this class and any subclass can see and edit 
    // virtual = allow to override it
    protected virtual void Eat()
    {
        FindObjectOfType<GameManager>().PelletEaten();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the game object colliding with pellets is Pacman, eat pellet
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Eat();
        }
    }
}
