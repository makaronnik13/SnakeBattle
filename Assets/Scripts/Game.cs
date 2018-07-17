using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;
using System.Linq;
using System;

public class Game : MonoBehaviour
{
    /// <summary>
    /// Variable used for game update delay calcuations.
    /// </summary>
    private float time;



    private List<Snake> snakes;


    /// <summary>
    /// Menu panel.
    /// </summary>
    public MenuPanel Menu;
    /// <summary>
    /// Game over panel.
    /// </summary>
    public GameOverPanel GameOver;
    /// <summary>
    /// Main game panel (with board).
    /// </summary>
    public GamePanel GamePanel;

    /// <summary>
    /// Parameter specyfying delay between snake movements (in seconds).
    /// </summary>
    [Range(0f, 3f)]
    public float GameSpeed;

    /// <summary>
    /// Has to be set to game's board object.
    /// </summary>
    public Board Board;

    // Use this for initialization
    void Start()
    {
        // Show main menu
        ShowMenu(); 
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        while (time > GameSpeed)
        {
            time -= GameSpeed;
            UpdateGameState();
        }
    }

    /// <summary>
    /// Updates game state.
    /// </summary>
    private void UpdateGameState()
    {
        if (snakes != null)
        {
            foreach (Snake snake in snakes)
            {
                var dir = snake.GetDirection();//controller.NextDirection();

                // New head position
                var head = snake.NextHeadPosition(dir);

                var x = head.x;
                var y = head.y;

                if (snake.WithoutTail.Contains(head))
                {
                    // Snake has bitten its tail - game over
                    //StartCoroutine(GameOverCoroutine());
                    return;
                }

                if (x >= 0 && x < Board.Columns && y >= 0 && y < Board.Rows)
                {

                    //snake.Move(dir, false);
                }
                else
                {
                    // Head is outside board's bounds - game over.
                    //StartCoroutine(GameOverCoroutine());
                }
            }
        }
    }

    /// <summary>
    /// Shows main menu.
    /// </summary>
    public void ShowMenu()
    {
        HideAllPanels();
        Menu.gameObject.SetActive(true);
    }

    /// <summary>
    /// Shows game over panel.
    /// </summary>
    public void ShowGameOver()
    {
        HideAllPanels();
        GameOver.gameObject.SetActive(true);
    }

    /// <summary>
    /// Shows the board and starts the game.
    /// </summary>
    public void StartGame()
    {
        List<SnakeProfile> snakes = Player.Instance.Snakes.Take(Mathf.Min(Player.Instance.Snakes.Count, 5)).ToList();
        BoardTemplate template = DefaultResources.GetRandomTemplate(snakes.Count);

        HideAllPanels();
        Restart(snakes, template);
        GamePanel.gameObject.SetActive(true);
    }

    /// <summary>
    /// Hides all panels.
    /// </summary>
    private void HideAllPanels()
    {
        Menu.gameObject.SetActive(false);
        GamePanel.gameObject.SetActive(false);
        GameOver.gameObject.SetActive(false);
    }

    /// <summary>
    /// Returns game to initial conditions.
    /// </summary>
    private void Restart(List<SnakeProfile> snakeProfiles, BoardTemplate template)
    {
        snakes = new List<Snake>();

        Board.Reset(template);


        foreach (SnakeProfile sp in snakeProfiles)
        {
            Snake snake = new Snake(Board, sp);
            snakes.Add(snake);
        }

        
        for(int i = 0; i< snakes.Count; i++)
        {
            snakes[i].Reset(template.GetSnakeBasePositions(i, snakes[i].Profile.Length));
        }

        time = 0;
    }




    /*
            var emptyPositions = Board.EmptyPositions.ToList();
            if (emptyPositions.Count == 0)
            {
                yield break;
            }
            bonusPosition = emptyPositions.RandomElement();
            Board[bonusPosition].Content = TileContent.Bonus;
      */


}
