using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMotor : MonoBehaviour
{
    private const float LANE_DISTANCE = 1.0f;
    private int kolonyaCount = 0;
    public Text kolonyaCountText;

    // Movement
    private CharacterController controller;
    public float speed = 3.0f;
    private int desiredLane = 1; // 0 = Left, 1 = Middle, 2 = Right
    private Rigidbody rb;

    // Sound
    public AudioSource coinCollectSound;
    public AudioSource deathSound;
    public AudioSource bgSound;

    // Animator
    private Animator animator;

    // GameOver UI
    public Canvas gameOverUI;
    private bool isGameOver = false;
    public Button restartButton;
    public Button nextLevelButton;
    public Text gameOverText;

    // Scene
    private int nextSceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        restartButton.onClick.AddListener(RestartButtonClick);
        nextLevelButton.onClick.AddListener(NextLevelButtonClick);

        bgSound.playOnAwake = true;
        bgSound.loop = true;

        coinCollectSound.playOnAwake = false;
        deathSound.playOnAwake = false;

        gameOverUI.enabled = false;
        restartButton.gameObject.SetActive(true);
        nextLevelButton.gameObject.SetActive(true);

        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            // Gather the inputs on which lane we should be
            if (MobileInput.Instance.SwipeLeft)
                MoveLane(false);
            if (MobileInput.Instance.SwipeRight)
                MoveLane(true);

            // Calculate where we should be in the future
            Vector3 targetPosition = transform.position.z * Vector3.forward;
            if (desiredLane == 0)
                targetPosition += Vector3.left * LANE_DISTANCE;
            else if (desiredLane == 2)
                targetPosition += Vector3.right * LANE_DISTANCE;

            // Calculate move delta
            Vector3 moveVector = Vector3.zero;
            moveVector.x = (targetPosition - transform.position).normalized.x * speed;
            moveVector.y = 0f;
            moveVector.z = speed;

            // Move player
            controller.Move(moveVector * Time.deltaTime);
        }
    }

    private void MoveLane(bool goingRight)
    {
        desiredLane += (goingRight) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.tag == "kolonya" || other.tag == "mask")
        {
            Destroy(other.gameObject);
            kolonyaCount += 1;
            kolonyaCountText.text = kolonyaCount.ToString();
            coinCollectSound.Play();
        }

        if (other.tag == "construction" || other.tag == "car" || other.tag == "dub" || other.tag == "infected")
        {
            Debug.Log("GameOver");
            nextLevelButton.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(true);
            gameOverHandler();
        }

        if (other.tag == "finishLine")
        {
            restartButton.gameObject.SetActive(true);
            nextLevelButton.gameObject.SetActive(true);
            finishLineHandler();
        }
    }

    private void gameOverHandler()
    {
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        animator.Rebind();
        isGameOver = true;
        gameOverUI.enabled = true;
        deathSound.Play();
        bgSound.Stop();
    }

    private void finishLineHandler()
    {
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        animator.Rebind();
        isGameOver = true;
        gameOverText.text = "Congratulations!";
        gameOverText.fontSize = 34;
        gameOverUI.enabled = true;
        bgSound.Stop();
    }

    private void RestartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void NextLevelButtonClick()
    {
        SceneManager.LoadScene(nextSceneToLoad);
    }
}
