using System;
using TMPro;
using UnityEngine;

public class Calculator : MonoBehaviour
{

    [Header("Elements")]
    public TextMeshProUGUI displayText;

    //Helpers 
    private string currentInput = "";
    private double result = 0;

    public void OnButonClickCallback(string buttonValue)
    {
        if (buttonValue == "=")
        {
            //Calculate the Final result with try catch
            try
            {
                if (currentInput.Contains("÷0"))  //Checks if Division by zero is happening
                    throw new DivideByZeroException("");

                result = Calculate(currentInput);
                currentInput = result.ToString();
                UpdateDisplay();
            }
            catch(DivideByZeroException dBZE)
            {
                currentInput = "Error: DBZE" + dBZE.Message;
                UpdateDisplay();
            }
            catch
            {
                currentInput = "Error";
                UpdateDisplay();
            }
        }
        else if (buttonValue == "AC")
        {
            //Clear the dispaly
            ClearDisplay();
        }
        else
        {
            //Adding the pressed button in textfield
            currentInput += buttonValue;
            UpdateDisplay();
        }
    }

    #region Calculate Method
    public double Calculate(string input)
    {
        //Check for Division First in input
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == '÷')
            {
                string leftOperand = ""; //Left number to the operator
                string rightOperand = ""; //Right number to the operator

                int leftIndex = i - 1;

                while (leftIndex >= 0 && (char.IsDigit(input[leftIndex]) || input[leftIndex] == '.'))
                {
                    leftOperand = input[leftIndex] + leftOperand; //extracting the complete Operand from Left to Right
                    leftIndex--;
                }

                int rightindex = i + 1;
                while (rightindex < input.Length && (char.IsDigit(input[rightindex]) || input[rightindex] == '.'))
                {
                    rightOperand += input[rightindex]; //extracting the complete Operand from Right to left
                    rightindex++;
                }

                double leftValue = double.Parse(leftOperand);
                double rightValue = double.Parse(rightOperand);
                double divResult = leftValue / rightValue;

                string DivInput = input.Substring(0, leftIndex + 1) + divResult.ToString() + input.Substring(rightindex); //Restructing the input after the operator solution 
                return Calculate(DivInput); //Returning the result as a recursive Method
            }
        }

        //Check for Multiplication in input
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == '×')
            {
                string leftOperand = ""; //Left number to the operator
                string rightOperand = ""; //Right number to the operator

                int leftIndex = i - 1;

                while (leftIndex >= 0 && (char.IsDigit(input[leftIndex]) || input[leftIndex] == '.')) //Getting the compelete Operand for any number of digits or decimal
                {
                    leftOperand = input[leftIndex] + leftOperand; //extracting the complete Operand from Left to Right
                    leftIndex--;
                }

                int rightindex = i + 1;
                while (rightindex < input.Length && (char.IsDigit(input[rightindex]) || input[rightindex] == '.'))
                {
                    rightOperand += input[rightindex]; //extracting the complete Operand from Right to left
                    rightindex++;
                }

                double leftValue = double.Parse(leftOperand);
                double rightValue = double.Parse(rightOperand);
                double MulResult = leftValue * rightValue;

                string MulInput = input.Substring(0, leftIndex + 1) + MulResult.ToString() + input.Substring(rightindex); //Restructing the input after the operator solution 
                return Calculate(MulInput); //Returning the result as a recursive Method
            }
        }

        //Check for Addiiton sign in input string
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == '+')
            {
                string leftOperand = ""; //Left number to the operator
                string rightOperand = ""; //Right number to the operator

                int leftIndex = i - 1;

                while (leftIndex >= 0 && (char.IsDigit(input[leftIndex]) || input[leftIndex] == '.')) //Getting the compelete Operand for any number of digits or decimal
                {
                    leftOperand = input[leftIndex] + leftOperand; //extracting the complete Operand from Left to Right
                    leftIndex--;
                }

                int rightindex = i + 1;
                while (rightindex < input.Length && (char.IsDigit(input[rightindex]) || input[rightindex] == '.'))
                {
                    rightOperand += input[rightindex]; //extracting the complete Operand from Right to left
                    rightindex++;
                }

                double leftValue = double.Parse(leftOperand);
                double rightValue = double.Parse(rightOperand);
                double addResult = leftValue + rightValue;

                string addInput = input.Substring(0, leftIndex + 1) + addResult.ToString() + input.Substring(rightindex); //Restructuring the input after the operator solution 
                return Calculate(addInput); //Returning the result as a recursive Method
            }
        }

        //Check for substraction sign in input string
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == '-')
            {
                string leftOperand = ""; //Left number to the operator
                string rightOperand = ""; //Right number to the operator

                int leftIndex = i - 1;

                while (leftIndex >= 0 && (char.IsDigit(input[leftIndex]) || input[leftIndex] == '.'))//Getting the compelete Operand for any number of digits or decimal
                {
                    leftOperand = input[leftIndex] + leftOperand; //extracting the complete Operand from Left to Right
                    leftIndex--;
                }

                int rightindex = i + 1;
                while (rightindex < input.Length && (char.IsDigit(input[rightindex]) || input[rightindex] == '.'))
                {
                    rightOperand += input[rightindex]; 
                    rightindex++;
                }

                double leftValue = double.Parse(leftOperand);
                double rightValue = double.Parse(rightOperand);
                double subResult = leftValue - rightValue; // Doing the operation as per the operator

                string subInput = input.Substring(0, leftIndex + 1) + subResult.ToString() + input.Substring(rightindex); //Restructing the input after the operator solution 
                return Calculate(subInput);//Returning the result as a recursive Method
            }
        }

        return double.Parse(input); // returning the entire result
    }
    #endregion

    void UpdateDisplay()
    {
        displayText.text = currentInput;
    }

    void ClearDisplay()
    {
        currentInput = "";
        result = 0;
        UpdateDisplay();
    }
}
