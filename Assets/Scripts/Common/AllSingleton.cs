using UnityEngine;
using UnityEngine.UI;

public class AllSingleton : MonoBehaviour
{
    public static AllSingleton Instance;

    [Header("Controllers")] 
    public GameManager gameManager;
    public GridController gridController;
    public CubeController cubeController;
    public GameOver gameOver;

    [Header("UI")] 
    public Button resetGameButton;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
        resetGameButton.onClick.AddListener(gameManager.Restart);
    }
}
