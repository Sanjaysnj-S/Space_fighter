using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private float elasepedTime = 0f;
    private int score = 0;
    //private int highScore = 0;
    public float scoreMultiplier = 10f;
    public GameObject Booster;
    public GameObject ExplosionEffect;

    public float maxSpeed = 5f;
    public float force = 1f;
    Rigidbody2D rb;
    public UIDocument uiDocument;
    private Label scoreText;
    //private Label highScoreText;
    private Button restartButton;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
        //highScoreText = uiDocument.rootVisualElement.Q<Label>("highScore");
        //highScoreText.style.display = DisplayStyle.None;


        restartButton = uiDocument.rootVisualElement.Q<Button>("RestartButton");
        restartButton.style.display = DisplayStyle.None;
        restartButton.clicked += ReloadScene;

    }

    void Update()
    {
        Movement();
        UpdateScore();
        Boost();
    }
    void UpdateScore()
    {
        elasepedTime += Time.deltaTime;
        score = Mathf.FloorToInt(elasepedTime * scoreMultiplier);
        scoreText.text = "Score: " + score;       

    }
    void Movement()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            // Camera.main.ScreenToWorldPoint ithu nomarl mouse click panura place ahh game world oota x,y coordinates laa sollum
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            Vector2 direction = (mousePos - transform.position).normalized;//normalized is user for make the force into same number(not affect the direction)

            //player move towaords mouse
            transform.up = direction; //player ahh mouse click panura place ku thirupum
            rb.AddForce(direction * force);
            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }
    }
    void Boost()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Booster.SetActive(true);
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            Booster.SetActive(false);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        Instantiate(ExplosionEffect, transform.position, transform.rotation);
        restartButton.style.display = DisplayStyle.Flex;

        //highScoreText.style.display = DisplayStyle.Flex;
        
       
    }
    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
