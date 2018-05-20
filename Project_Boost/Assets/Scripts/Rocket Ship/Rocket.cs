using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {
    [SerializeField] float rcsThrust = 200f;
    [SerializeField] float mainThrust = 200f;
    [SerializeField] float levelLoadDelay = 2f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;


    Rigidbody rigidBody;
    AudioSource audioSource;
    bool collisionsDisabled = false;

    bool isTransitioning = false;

    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }


    void Update() {
        if (!isTransitioning) {
            RespoindToThrustInput();
            RespoindToRotateInput();
        }

        if (Debug.isDebugBuild)
            RespoindToDebugKeys();

    }

    private void RespoindToDebugKeys() {
        if (Input.GetKeyDown(KeyCode.L)) {
            LoadNextScene();
        } else if (Input.GetKeyDown(KeyCode.C)) {
            collisionsDisabled = !collisionsDisabled;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (isTransitioning || collisionsDisabled)
            return;

        switch (collision.gameObject.tag) {
            case "Friendly":
                //do nothing
                break;
            case "Fuel":
                //hit fuel
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default: //dead
                StartDeathSequence();
                break;

        }
    }

    private void StartSuccessSequence() {
        successParticles.Play();
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        Invoke("LoadNextScene", levelLoadDelay); //load after one second
    }

    private void StartDeathSequence() {
        deathParticles.Play();
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        Invoke("LoadFirstLevel", levelLoadDelay); //load after one second
    }



    private void LoadNextScene() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = ++currentSceneIndex;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);




    }

    private void LoadFirstLevel() {
        SceneManager.LoadScene(0);
    }


    private void RespoindToThrustInput() {
        if (Input.GetKey(KeyCode.Space)) {
            ApplyThrust();
        } else {
            StopApplyingThrust();
        }
    }

    private void StopApplyingThrust() {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void ApplyThrust() {
        float thrust = mainThrust * Time.deltaTime;
        rigidBody.AddRelativeForce(Vector3.up * thrust);
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(mainEngine);
        mainEngineParticles.Play();

    }

    private void RespoindToRotateInput() {
        rigidBody.angularVelocity = Vector3.zero; //remove rotation due to physics
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        } else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
    }




}
