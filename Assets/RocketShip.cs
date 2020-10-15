
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
    enum State { Alive, Dying, Transcending }
    State state;

    void Start()
    {
        state = State.Alive;
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
      if (state == State.Alive){
        RespondToThrustInput();
        RespondToRotateInput();
      }
	}


    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive){ return; }
        switch(collision.gameObject.tag)
        {
          case "Friendly":
            print("OK");
            break;

          case "Finish":
            StartSuccessSequence();
            break;

          default:
            StartDeathSequence();
            break;
        }
    }
    private void StartSuccessSequence()
    {
        print("bey");
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        Invoke("LoadNextScene", levelLoadDelay); // parameterise time
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        deathParticles.Play();
        Invoke("LoadFirstScene", levelLoadDelay); // parameterise time
    }

    private void LoadNextScene(){
      SceneManager.LoadScene(1);
    }

    private void LoadFirstScene(){
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
