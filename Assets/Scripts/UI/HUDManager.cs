using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class HUDManager : MonoBehaviour
{
    public UIHealth healthUI;
    public UIScore scoreUI;

    public bool gamePaused = false;

    public GameObject PauseMenu;
    public GameObject DeathMenu;
    private GameManager gameManager;
    public EventSystem eventSystem;
    public GameObject retryOption;


    void Awake()
    {
       healthUI = FindObjectOfType<UIHealth>();
       scoreUI = FindObjectOfType<UIScore>();
       gameManager = FindObjectOfType<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CollisionHandler.PlayerDeath += ActivateDeathHUD;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (gamePaused){
                ResumeGame();
            }
            else{
                PauseGame();
            }

        }
    }

    private void ActivateDeathHUD(){
        if (healthUI != null){
            healthUI.gameObject.SetActive(false);
        }

        if (scoreUI != null){
            scoreUI.gameObject.SetActive(false);
        }

        DeathMenu.SetActive(true);
        eventSystem.SetSelectedGameObject(retryOption);
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        DeathMenu.SetActive(false);
        Time.timeScale = 1.0f;
        gamePaused = false;
    }

    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void QuitToMenu()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ResetGame()
    {
        gameManager.ReloadScene();
    }


}
