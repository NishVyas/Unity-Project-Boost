using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationalThrust = 100f;
    Rigidbody rocketRigidBody;
    AudioSource rocketAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        rocketRigidBody = GetComponent<Rigidbody>();
        rocketAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() {
        if (Input.GetKey(KeyCode.Space)) {
            rocketRigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            
            if (!rocketAudioSource.isPlaying) {
                rocketAudioSource.Play();
            }
        } else {
            rocketAudioSource.Stop();
        }
    }

    void ProcessRotation() {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            ApplyRotation(rotationalThrust);
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            ApplyRotation(-rotationalThrust);
        }
    }

    private void ApplyRotation(float rotationThisFrame) {
        rocketRigidBody.freezeRotation = true; // Freezing rotation to manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rocketRigidBody.freezeRotation = false; // Un-freezing rotation so physics engine can take over
    }
}
