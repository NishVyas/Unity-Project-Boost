using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {
    [SerializeField] float levelLoadDelay = 2f;
    Movement rocketMovement;

    void OnCollisionEnter(Collision other) {
        switch (other.gameObject.tag) {
            case "Friendly": 
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence() {
        DisableRocketMovement();
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void LoadNextLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    void StartCrashSequence() {
        // TODO: Add SFX upon crash
        // TODO: Add particle effect upon crash
        DisableRocketMovement();
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void DisableRocketMovement() {
        rocketMovement = GetComponent<Movement>();
        rocketMovement.enabled = false;
    }

    void ReloadLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
