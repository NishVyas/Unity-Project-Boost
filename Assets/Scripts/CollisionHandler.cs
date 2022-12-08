using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crashExplosion;
    Movement rocketMovement;
    AudioSource collisionAudioSource;

    bool isTransitioningLevel = false;

    void Start() {
        collisionAudioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other) {
        if (isTransitioningLevel) {
            return;
        }

        switch (other.gameObject.tag) {
            case "Friendly":
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
        isTransitioningLevel = true;
        collisionAudioSource.Stop();
        collisionAudioSource.PlayOneShot(success);
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
        // TODO: Add particle effect upon crash
        isTransitioningLevel = true;
        collisionAudioSource.Stop();
        collisionAudioSource.PlayOneShot(crashExplosion);
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
