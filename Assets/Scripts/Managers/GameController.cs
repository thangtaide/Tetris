using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    GhostShape ghostShape;
    Spawner spawner;
    Board board;
    Shape activeShape;
    SoundManager soundManager;
    ScoreManager scoreManager;
    public float timeToDrop = 1f;
    float timeToDropModded;
    float timeNextDrop;
    bool gameOver;

    [Range(0.02f, 1f)]
    public float timeToNextKeyDown = 0.02f;
    float timeNextKeyDown;

    [Range(0.02f, 1f)]
    public float timeToNextKeyLeftRight = 0.25f;
    float timeNextKeyLeftRight;

    public GameObject gameOverPanel;

    public IconToggle toggleRotate;
    public bool rotateClockwise = true;

    public IconToggle togglePause;
    public bool isPause = false;
    public GameObject pausePanel;

    private void Start()
    {
        spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();
        board = GameObject.FindWithTag("Board").GetComponent<Board>();
        soundManager = FindObjectOfType<SoundManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        ghostShape = FindObjectOfType<GhostShape>();
        gameOver = false;
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
        timeToDropModded = timeToDrop;

        if (spawner)
        {
            if (!activeShape)
            {
                activeShape = spawner.SpawnShape();
            }
            spawner.transform.position = Vectorf.Round(spawner.transform.position);

        }

    }
    void Update()
    {
        if (!spawner || !board || !activeShape || gameOver ||!soundManager ||!scoreManager)
        {
            return;
        }
        PlayerInput();
    }
    private void LateUpdate()
    {
        if (ghostShape)
        {
            ghostShape.CreateGhostShape(activeShape, board);
        }
    }

    private void PlayerInput()
    {
        if (Input.GetButtonDown("MoveRight") || Input.GetButton("MoveRight") && Time.time >= timeNextKeyLeftRight)
        {

            timeNextKeyLeftRight = Time.time + timeToNextKeyLeftRight;
            activeShape.MoveRight();
            if (!board.IsValidPosition(activeShape))
            {
                activeShape.MoveLeft();
                PlaySound(soundManager.errSound);
            }
            else
            {
                PlaySound(soundManager.moveSound);
            }
        }
        else if (Input.GetButtonDown("MoveLeft") || Input.GetButton("MoveLeft") && Time.time >= timeNextKeyLeftRight)
        {

            timeNextKeyLeftRight = Time.time + timeToNextKeyLeftRight;
            activeShape.MoveLeft();
            if (!board.IsValidPosition(activeShape))
            {
                activeShape.MoveRight();
                PlaySound(soundManager.errSound);
            }
            else
            {
                PlaySound(soundManager.moveSound);
            }
        }
        else if (Input.GetButtonDown("Rotate"))
        {

            activeShape.RotateClockwise(rotateClockwise);
            if (!board.IsValidPosition(activeShape))
            {
                activeShape.RotateClockwise(!rotateClockwise);
                PlaySound(soundManager.errSound);
            }
            else
            {
                PlaySound(soundManager.moveSound);
            }
        }
        else if (Input.GetButton("MoveDown") && Time.time > timeNextKeyDown || Time.time > timeNextDrop)
        {

            timeNextDrop = Time.time + timeToDropModded;
            timeNextKeyDown = Time.time + timeToNextKeyDown;
            if (activeShape)
            {
                activeShape.MoveDown();
                if (!board.IsValidPosition(activeShape))
                {
                    if (board.IsOverLimit(activeShape))
                    {
                        GameOver();
                    }
                    else
                    {
                        timeNextKeyDown = Time.time;
                        activeShape.MoveUp();
                        board.StoreShapeInGrid(activeShape);
                        ghostShape.ResetGhostShape();
                        board.CleanAllRows();
                        if (board.completeRow > 0)
                        {
                            scoreManager.ScoreLine(board.completeRow);

                            if (scoreManager.didLevelUp)
                            {
                                PlaySound(soundManager.vocalLevelUp);
                                timeToDropModded = Mathf.Clamp(timeToDrop - ((float)scoreManager.level - 1) * 0.15f, 0.05f, 1f);
                            }
                            else
                            {
                                if (board.completeRow > 1)
                                {
                                    PlaySound(soundManager.RandomVocal());
                                }
                            }
                            PlaySound(soundManager.clearRowSound);
                            board.completeRow = 0;
                        }
                        if (spawner)
                        {
                            activeShape = spawner.SpawnShape();
                            PlaySound(soundManager.dropSound);
                        }
                    }
                }
            }
        }
        else if (Input.GetButtonDown("Pause"))
        {
            TogglePause();
        }
    }
    void GameOver()
    {
        activeShape.MoveUp();
        gameOver = true;
        gameOverPanel.SetActive(true);
        PlaySound(soundManager.gameOverSound);
        PlaySound(soundManager.vocalGameOver);
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    void PlaySound(AudioClip audioClip)
    {
        if (soundManager.fxEnabled && audioClip)
        {
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position, soundManager.fxVolume);
        }
    }
    public void ToggleRotate()
    {
        rotateClockwise = !rotateClockwise;
        toggleRotate.ToggleIcon(rotateClockwise);
    }
    public void TogglePause()
    {
        if (gameOver)
        {
            return;
        }
        isPause = !isPause;
        if (pausePanel)
        {
            Time.timeScale = isPause ? 0 : 1;
            pausePanel.SetActive(isPause);
            if (soundManager)
            {
                soundManager.musicSource.volume = isPause ? soundManager.musicVolume * 0.25f : soundManager.musicVolume;
            }
        }
        togglePause.ToggleIcon(isPause);
    }
}
