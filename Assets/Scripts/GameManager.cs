using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//////////////////////////////////////////////
//Assignment/Lab/Project: Guess my Number
//Name: Logan Hickman
//Section: 2021SP.SGD.213.2101
//Instructor: Aurore Wold
//Date: 01/12/2022
/////////////////////////////////////////////
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// TMP Input Field for guesses
    /// </summary>
    [SerializeField]
    private TMP_InputField input;
    /// <summary>
    /// The TMP Output Field to provide feedback to players
    /// </summary>
    [SerializeField]
    private TMP_Text output;
    /// <summary>
    /// The Restart Button to display after game ends
    /// </summary>
    [SerializeField]
    private Button restartButton;

    /// <summary>
    /// How many guesses has the player made?
    /// </summary>
    private int guesses = 0;
    /// <summary>
    /// What is the target number the player is trying to guess between 1 and 100
    /// </summary>
    private int targetNum;

    // Start is called before the first frame update
    void Start()
    {
        RestartGame();
    }

    /// <summary>
    /// Takes the input from the input field and parses the int to provide player feedback
    /// </summary>
    public void ProcessGuess()
    {
        try
        {
            //Tries to parse the int
            int guess = int.Parse(input.text);

            //Makes sure guess is in range
            if (guess < 1 || guess > 100)
            {
                output.text = guess + " is not between 1 and 100, choose a valid number";
                input.text = "";
            }
            //Checks if guess is less than target
            else if (guess < targetNum)
            {
                guesses++;
                output.text = guess + " is less than my number, " + (6 - guesses) + " tries left";
                input.text = "";
            }
            //Checks if guess is more than target
            else if (guess > targetNum)
            {
                guesses++;
                output.text = guess + " is more than my number, " + (6 - guesses) + " tries left";
                input.text = "";
            }
            //Checks if guess is correct
            else if (guess == targetNum)
            {
                guesses++;
                output.text = guess + " is my number! You got it in " + guesses + " tries";
                input.text = "";
                restartButton.gameObject.SetActive(true);
            }
            //Confused why everything broke
            else
            {
                output.text = "You shouldn't see this message, something broke";
                input.text = "";
            }
        }
        catch
        {
            output.text = input.text + " is not a valid number";
            input.text = "";
        }

        if (guesses > 5 && !restartButton.gameObject.activeInHierarchy)
        {
            output.text = "You lost, my number was " + targetNum;
            input.text = "";
            restartButton.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Handles when the restart button is pressed to restart the game
    /// </summary>
    public void RestartGame()
    {
        restartButton.gameObject.SetActive(false);
        targetNum = Random.Range(0, 101);
        guesses = 0;
        output.text = "Guess a number in the box above";
    }
}
