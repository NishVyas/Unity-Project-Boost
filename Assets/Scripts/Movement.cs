using UnityEngine;

public class Movement : MonoBehaviour {
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationalThrust = 100f;
    [SerializeField] AudioClip rocketEngine;
    [SerializeField] ParticleSystem mainBoosterParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;

    Rigidbody rocketRigidBody;
    AudioSource rocketAudioSource;

    void Start() {
        rocketRigidBody = GetComponent<Rigidbody>();
        rocketAudioSource = GetComponent<AudioSource>();
    }

    void Update() {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() {
        if (Input.GetKey(KeyCode.Space)) {
            StartThrusting();
        }
        else {
            StopThrusting();
        }
    }

    void ProcessRotation() {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            RotateLeft();
        } else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            RotateRight();
        } else {
            StopRotating();
        }
    }

    void StartThrusting() {
        rocketRigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!rocketAudioSource.isPlaying) {
            rocketAudioSource.PlayOneShot(rocketEngine);
        }

        if (!mainBoosterParticles.isPlaying) {
            mainBoosterParticles.Play();
        }
    }

    void StopThrusting()
    {
        rocketAudioSource.Stop();
        mainBoosterParticles.Stop();
    }

    void RotateLeft() {
        ApplyRotation(rotationalThrust);

        if (!rightBoosterParticles.isPlaying) {
            rightBoosterParticles.Play();
        }
    }

    void RotateRight() {
        ApplyRotation(-rotationalThrust);

        if (!leftBoosterParticles.isPlaying) {
            leftBoosterParticles.Play();
        }
    }

    void StopRotating() {
        leftBoosterParticles.Stop();
        rightBoosterParticles.Stop();
    }

    private void ApplyRotation(float rotationThisFrame) {
        rocketRigidBody.freezeRotation = true; // Freezing rotation to manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rocketRigidBody.freezeRotation = false; // Un-freezing rotation so physics engine can take over
    }
}
