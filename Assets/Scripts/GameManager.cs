using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score;
    public GameObject[] moles;
    public float popTimer;
    public float popInterval = 0.8f;
    public float timeLeft = 10f;
    public bool gameOver;
    public TMP_Text scoreText;
    public TMP_Text timerText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < moles.Length; i++) // loop through all the moles length at the start
        {
            moles[i].SetActive(false); // moles[i] to go through each mole to hide it 
        }
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = "timer: " + Mathf.Round(timeLeft);
        scoreText.text =  "score: " + score;
        if (gameOver)
        {
            return;
            
        }
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {   
            gameOver = true;
            for (int i = 0; i < moles.Length; i++)
            {
                moles[i].SetActive(false);
            }
            Debug.Log("final score is: " + score);
            return;
        }
        
        popTimer += Time.deltaTime; // counting up in seconds 
        int randomIndex = Random.Range(0, moles.Length); // picking a random number from 0 to moles length
       if (popTimer >= popInterval)
        {
            for (int i = 0; i < moles.Length; i++) // hiding moles again to avoid the board to be full
            {
                moles[i].SetActive(false);
            }
            popTimer = 0; // resetting timer 
            moles[randomIndex].SetActive(true); // showing a random mole

        }
        if (Mouse.current.leftButton.wasPressedThisFrame) // was it pressed this frame?
        {
            Vector2 mousePos = Mouse.current.position.ReadValue(); // reading mouse position as  x and y value
            Debug.Log(mousePos);
            Ray ray = Camera.main.ScreenPointToRay(mousePos); // laser from the mouse
            if (Physics.Raycast(ray, out RaycastHit hit)) // if laser hit something
            {
                Debug.Log(hit.collider.gameObject.name); // getting name 
                if (hit.collider.CompareTag("Mole"))// counting only if it is a mole
                {
                    hit.collider.gameObject.SetActive(false); // dissapearing from the board 
                    score++;
                    Debug.Log("the score is " + score);
                }
            }
        }
    }
}
