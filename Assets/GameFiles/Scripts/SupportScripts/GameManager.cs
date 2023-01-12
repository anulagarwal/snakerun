using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    [Header("Attributes")]
    [SerializeField] public int currentLevel;
    public static GameManager Instance = null;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        Application.targetFrameRate = 60;
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("level", 1);
        UIPackSingleton.Instance.UpdateLevelText(currentLevel);
    }


    public void Win()
    {
        currentLevel++;
        PlayerPrefs.SetInt("level", currentLevel);
    }

    public void ChangeLevel()
    {
        SceneManager.LoadScene("Core");
    }
}
