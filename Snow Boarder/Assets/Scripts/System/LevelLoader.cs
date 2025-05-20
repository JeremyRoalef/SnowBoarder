using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    static LevelLoader _instance;
    public static LevelLoader Instance
    {
        get
        {
            if (_instance == null)
            {
                //Handle null level loader issues
                Debug.LogWarning("Warning: No instance in scene. Creating one...");
                CreateLevelLoaderObj();
            }

            return _instance;
        }
    }

    List<string> sceneNames = new List<string>();

    bool gameHasStarted = false;

    const string LEVEL_1_SCENE_NAME = "Level 1";
    const string MAIN_MENU_SCENE_NAME = "Main Menu";
    const string INITIALIZATION_SCENE_NAME = "Initialization";
    const string BASE_SCENE_LEVEL_NAME = "Level";
    const string SCORE_SCENE_NAME = "Score Scene";
    const string MULTIPLE_INSTANCES_WARNING_STRING = "Warning: Multiple instances of LevelLoader detected!";
    const string MISSING_SCENE_WARNING_STRING = "Warning: Missing scene name!";

    /*
     * 
     * ----------------UNITY EVENTS----------------
     * 
     */

    private void Awake()
    {
        InitializeSingleton();
    }

    private void Start()
    {
        if (gameHasStarted) return;
        if (!SceneExists(MAIN_MENU_SCENE_NAME))
        {
            Debug.LogWarning(MISSING_SCENE_WARNING_STRING);
            return;
        }

        gameHasStarted = true;
        LoadMainMenu();
    }

    /*
     * 
     * ----------------PUBLIC METHODS----------------
     * 
     */

    /// <summary>
    /// Load a scene in the build profiles by index given delay
    /// </summary>
    /// <param name="buildIndex">Index of the scene in build settings</param>
    /// <param name="delay">Delay before loading the scene</param>
    public void LoadLevel(int buildIndex, float delay = 0)
    {
        if (!SceneExists(buildIndex))
        {
            Debug.LogWarning(MISSING_SCENE_WARNING_STRING);
            return;
        }

        StartCoroutine(LoadSceneCoroutine(buildIndex, delay));
    }

    /// <summary>
    /// Load a scene in the build profiles by name given delay
    /// </summary>
    /// <param name="levelName">The name of the scene in build settings</param>
    /// <param name="delay">Delay before loading the scene</param>
    public void LoadLevel(string levelName, float delay = 0)
    {
        if (!SceneExists(levelName))
        {
            Debug.LogWarning(MISSING_SCENE_WARNING_STRING);
            return;
        }

        StartCoroutine(LoadSceneCoroutine(levelName, delay));
    }

    /// <summary>
    /// Mehtod to load the first level in the game
    /// </summary>
    public void LoadFirstLevel(float delay = 0)
    {
        if (!SceneExists(LEVEL_1_SCENE_NAME))
        {
            Debug.LogWarning(MISSING_SCENE_WARNING_STRING);
            return;
        }

        StartCoroutine(LoadSceneCoroutine(LEVEL_1_SCENE_NAME, delay));
    }

    /// <summary>
    /// Method to load the main menu scene
    /// </summary>
    public void LoadMainMenu(float delay = 0)
    {
        if (!SceneExists(MAIN_MENU_SCENE_NAME))
        {
            Debug.LogWarning(MISSING_SCENE_WARNING_STRING);
            return;
        }

        StartCoroutine(LoadSceneCoroutine(MAIN_MENU_SCENE_NAME, delay));
    }

    /// <summary>
    /// Method to load the scene named Score
    /// </summary>
    /// <param name="delay">Delay before loading the scene</param>
    public void LoadScoreScene(float delay = 0)
    {
        if (!SceneExists(SCORE_SCENE_NAME))
        {
            Debug.LogWarning(MISSING_SCENE_WARNING_STRING);
            return;
        }

        StartCoroutine(LoadSceneCoroutine(SCORE_SCENE_NAME, delay));
    }

    /// <summary>
    /// Method to get the name of all levels in the game
    /// </summary>
    /// <returns>List of all level names</returns>
    public List<string> GetAllLevelNames()
    {
        return sceneNames;
    }

    /// <summary>
    /// Method to reload the active scene
    /// </summary>
    /// <param name="delay">Delay before reloading the active scene</param>
    public void ReloadActiveScene(float delay = 0)
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadSceneCoroutine(buildIndex, delay));
    }

    /// <summary>
    /// Method to determine whether the next scene exists given current scene
    /// </summary>
    /// <param name="currentSceneIndex">The index of the current scene</param>
    /// <returns>True if next scene exists. False otherwise</returns>
    public bool NextLevelExists(int currentSceneIndex)
    {
        return SceneExists(currentSceneIndex + 1);
    }

    /// <summary>
    /// Method to determine whether the next scene exists given current scene
    /// </summary>
    /// <param name="currentSceneName">The name of the current scene</param>
    /// <returns>True if next scene exists. False otherwise</returns>
    public bool NextLevelExists(string currentSceneName)
    {
        for (int i = 0; i < sceneNames.Count; i++)
        {
            if (sceneNames[i].Equals(currentSceneName) && i != sceneNames.Count - 1)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Method to load the next level
    /// </summary>
    /// <param name="delay">Delay before loading the next level</param>
    public void LoadNextLevel(float delay = 0)
    {
        int nextLevelBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (SceneExists(nextLevelBuildIndex))
        {
            StartCoroutine(LoadSceneCoroutine(nextLevelBuildIndex, delay));
        }
        else
        {
            LoadScoreScene(delay);
        }
    }

    /*
     * 
     * ----------------PRIVATE METHODS----------------
     * 
     */

    void InitializeSingleton()
    {
        //Singleton pattern
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            Debug.LogWarning(MULTIPLE_INSTANCES_WARNING_STRING);
        }

        //Initializing all levels into a list
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            //ChatGPT correction for Using scenes in build settings
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string name = Path.GetFileNameWithoutExtension(path);

            if (name.Contains(BASE_SCENE_LEVEL_NAME))
            {
                sceneNames.Add(name);
            }
        }
    }

    /// <summary>
    /// Method to check if a scene exists given the index
    /// </summary>
    /// <param name="buildIndex">The scene's build index from the build settings</param>
    /// <returns>True if scene exists. False otherwise</returns>
    bool SceneExists(int buildIndex)
    {
        return buildIndex >= 0 && buildIndex < SceneManager.sceneCountInBuildSettings;
    }

    /// <summary>
    /// Method to check if a scene exists given the name
    /// </summary>
    /// <param name="sceneName">The name of the scene in question</param>
    /// <returns>True if scene exists. False otherwise</returns>
    bool SceneExists(string sceneName)
    {
        //Method generated by ChatGPT to handle invalid scene names

        //Loop through all scenes in build settings
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            //Get the name of the scene
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string name = Path.GetFileNameWithoutExtension(path);

            //Check if scene names are same
            if (name.Equals(sceneName)) return true;
        }

        //No scene found
        return false;
    }

    /// <summary>
    /// Coroutine to load a given scene
    /// </summary>
    /// <param name="buildIndex">The index of the scene</param>
    /// <param name="delay">Delay before scene load</param>
    IEnumerator LoadSceneCoroutine(int buildIndex, float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(buildIndex);
    }

    /// <summary>
    /// Coroutine to load a given scene
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="delay"></param>
    IEnumerator LoadSceneCoroutine(string sceneName, float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Method to handle null _instance reference
    /// </summary>
    private static void CreateLevelLoaderObj()
    {
        //Create the game obeject
        GameObject newLevelLoaderObj = Instantiate(
            new GameObject(),
            new Vector3(0, 0, 0),
            Quaternion.identity);

        newLevelLoaderObj.name = "Level Loader";

        //Singleton
        DontDestroyOnLoad(newLevelLoaderObj);

        //Add the level loader scripts
        LevelLoader newLevelLoader = newLevelLoaderObj.AddComponent<LevelLoader>();

        //Set the instance
        _instance = newLevelLoader;
    }
}
