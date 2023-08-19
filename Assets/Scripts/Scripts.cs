using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class Scripts : MonoBehaviour
{
    public GameObject[] buttonText;
    public GameObject[] button;
    public Text text;
    int playerTurn = 1; // 1 for human player (X) and 2 for AI (O)
    bool gameEnded = false;
    public int[] data = new int[9];
    int[][] winCombinations = new int[][]
    { 
        // Rows
        new int[] { 0, 1, 2 },
        new int[] { 3, 4, 5 },
        new int[] { 6, 7, 8 },

        // Columns
        new int[] { 0, 3, 6 },
        new int[] { 1, 4, 7 },
        new int[] { 2, 5, 8 },

        // Diagonals
        new int[] { 0, 4, 8 },
        new int[] { 2, 4, 6 }
    };

  
    public void btnClick(int buttonNumber)
    { 
      
        if (!gameEnded && button[buttonNumber].GetComponent<Button>().interactable)
        {
            button[buttonNumber].GetComponent<Button>().interactable = false;
            data[buttonNumber] = 1;
            buttonText[buttonNumber].GetComponent<Text>().text = "X"; // Human player's move

            if (CheckWin())
            {
                print("You win!");
                text.text = "You win!";
                gameEnded = true;
            }
            else if (IsBoardFull())
            {
                print("It's a tie!");
                text.text = "It's a tie!";
                gameEnded = true;
            }
            else
            {
                findWinCombo(buttonNumber);
               // Invoke("AIMove", 0.5f);
            }
        }
    }


    private void AIMove(int randomIndex)
    {
        playerTurn = 2;
       // int randomIndex = GetRandomAvailableButton();
        if (randomIndex != -1)
        {
            button[randomIndex].GetComponent<Button>().interactable = false;
            data[randomIndex] = 2;
            buttonText[randomIndex].GetComponent<Text>().text = "O"; // AI's move

            if (CheckWin())
            {
                print("AI wins!");
                text.text = "AI wins!";
                gameEnded = true;
            }
            else if (IsBoardFull())
            {
                print("It's a tie!");
                text.text = "It's a tie!";
                gameEnded = true;
            }
            else
            {
                playerTurn = 1;
            }
        }
    }
    private bool CheckWin()
    {
        string currentPlayer = (playerTurn == 1) ? "X" : "O";

        foreach (int[] combination in winCombinations)
        {
            bool hasWon = true;
            int emptySlots = 0;

            foreach (int buttonIndex in combination)
            {
                string buttonValue = buttonText[buttonIndex].GetComponent<Text>().text;

                if (buttonValue != currentPlayer && buttonValue != "")
                {
                    hasWon = false;
                    break;
                }
                else if (buttonValue == "")
                {
                    emptySlots++;
                }
            }

            if (hasWon && emptySlots == 0)
            {
                return true;
            }
        }

        return false;
    }

    private bool IsBoardFull()
    {
        foreach (GameObject buttonObj in button)
        {
            if (buttonObj.GetComponent<Button>().interactable)
            {
                return false;
            }
        }

        return true;
    }

    private int GetRandomAvailableButton()
    {
        List<int> interactableButtons = new List<int>();

        for (int i = 0; i < button.Length; i++)
        {
            if (button[i].GetComponent<Button>().interactable)
            {
                interactableButtons.Add(i);
            }
        }

        if (interactableButtons.Count > 0)
        {
            int randomIndex = Random.Range(0, interactableButtons.Count);
            return interactableButtons[randomIndex];
            //AIMove(interactableButtons[randomIndex]);
        }

        return -1;
    }
    private void findWinCombo(int buttonNumber)
    {
        foreach (int[] combination in winCombinations)
        {
            
            foreach (int buttonIndex in combination)
            {
                if(buttonIndex == buttonNumber)
                {
                    print(buttonNumber);

                    foreach(int buttonIndex2 in combination)
                    {
                        if (data[buttonIndex2] == 0)
                        {
                            print(buttonIndex2);
                            AIMove(buttonIndex2);
                            return;
                        }
                   
                    }
                }
            }

        }
        AIMove(GetRandomAvailableButton());
    }
    
}