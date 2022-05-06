using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;

    public int ghostMultiplier { get; private set; } = 1;
    public int score { get; private set; }
    public int lives { get; private set; }


        private void Start() 
        {
            NewGame();
        }

        private void Update() 
        {
            if (lives <= 0 && Input.anyKeyDown)
            {
                NewGame();
            }
        }

        private void NewGame()
        {
            SetScore(0);
            SetLives(3);
            NewRound();
        }

        private void NewRound()
        {
            foreach (Transform pellet in pellets)
            {
                pellet.gameObject.SetActive(true);
            }
            ResetState();
        }

        private void ResetState()
        {
            ResetGhostMultiplier();
            for (int i = 0; i < ghosts.Length; i++)
            {
                ghosts[i].gameObject.SetActive(true);
            }
            pacman.gameObject.SetActive(true);
        }

        private void GameOver()
        {
            for (int i = 0; i < ghosts.Length; i++)
            {
                ghosts[i].gameObject.SetActive(false);
            }
            pacman.gameObject.SetActive(false);
        }

        private void SetScore(int score)
        {
            score = score;
        }

        private void SetLives(int lives)
        {
            lives = lives;
        }

        public void GhostEaten(Ghost ghost)
        {
            int points = score + ghost.points * ghostMultiplier;
            SetScore(score + points);
            ghostMultiplier++;
        }

        public void PacmanEaten()
        {
            pacman.gameObject.SetActive(false);

            SetLives(lives - 1);

            if (lives > 0)
            {
                Invoke(nameof(ResetState), 3.0f);
            }
            else if (lives == 0)
            {
                GameOver();
            }
        }

        public void PelletEaten(Pellet pellet)
        {
            pellet.gameObject.SetActive(false);
            SetScore(score + pellet.points);

            if (!HasRemainingPellets())
            {
                pacman.gameObject.SetActive(false);
                Invoke(nameof(NewRound), 3.0f);
            }
        }

        public void PowerPelletEaten(PowerPellet pellet)
        {           
            PelletEaten(pellet);
            CancelInvoke();
            Invoke(nameof(ResetGhostMultiplier), pellet.duration);
        }

        private bool HasRemainingPellets()
        {
            foreach (Transform pellet in this.pellets)
            {
                if (pellet.gameObject.activeSelf) 
                {
                    return true;
                }
                
            }
            return false;
        }

        private void ResetGhostMultiplier()
        {
            ghostMultiplier = 1;
        }
}



