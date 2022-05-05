using UnityEngine;

public class PowerPellet : MonoBehaviour
{
    public float duration = 8.0f;

    protected override void Eat()
    {
        FindObjectOfType<GameManager>().PowerPelletEaten();
    }

}
