
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketShip : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float rocketThrust = 100f;
    [SerializeField] float mainThrust = 6.5f;
    [SerializeField] float levelLoadDelay = 2f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;

    Rigidbody rigidBody;
    AudioSource audioSource;

    float rotationSpeed;
    bool isAlive = true;

    bool collisionsDisabled = false;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.8f;
    }

    // Update is called once per frame
    void Update () {
      if (isAlive){
        RespondToThrustInput();
        RespondToRotateInput();
      }

      if (Debug.isDebugBuild)
        {
            RespondToDebugKeys();
        }
	}



  private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionsDisabled = !collisionsDisabled; // toggle
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isAlive){ return; }
        switch(collision.gameObject.tag)
        {
          case "Friendly":
            print("OK");
            break;

          case "Finish":
            audioSource.volume = 0.1f;
            StartSuccessSequence();
            break;

          default:
            audioSource.volume = 0.1f;
            StartDeathSequence();
            break;
        }
    }
    private void StartSuccessSequence()
    {
        isAlive = !isAlive;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        Invoke("LoadNextLevel", levelLoadDelay); // parameterise time
    }

    private void StartDeathSequence()
    {
        isAlive = !isAlive;
        audioSource.Stop();

        audioSource.PlayOneShot(death);
        deathParticles.Play();
        Invoke("LoadFirstLevel", levelLoadDelay); // parameterise time
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0; // loop back to start
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space)) // can thrust while rotating
        {
            ApplyThrust();
        }
        else{
          audioSource.Stop();
          mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
       rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

       if (!audioSource.isPlaying) // so it doesn't layer
       {
           audioSource.PlayOneShot(mainEngine);
       }

       if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }


    }

    private void RespondToRotateInput()
    {
      rigidBody.freezeRotation = true;

        rotationSpeed = rocketThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }

      rigidBody.freezeRotation = false;
    }
}
