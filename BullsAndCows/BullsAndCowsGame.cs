﻿using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace BullsAndCows
{
    public class BullsAndCowsGame
    {
        private readonly SecretGenerator secretGenerator;
        private readonly string secret;
        private int counter;

        public BullsAndCowsGame(SecretGenerator secretGenerator)
        {
            this.secretGenerator = secretGenerator;
            this.secret = secretGenerator.GenerateSecret();
            this.counter = 0;
            this.CanContinue = true;
        }

        public bool CanContinue { get; set; }
        public bool IsWinner { get; set; }

        public bool IsGuessValid(string guess)
        {
            var guessChars = guess.ToCharArray();
            var isNumber = guessChars.All(c => !(c < '0' || c > '9'));
            var isUnique = guessChars.Distinct().Count() == 4;
            return isNumber && isUnique;
        }

        public string Guess(string guess)
        {
            if (!IsGuessValid(guess))
            {
                return "Wrong Input, input again";
            }

            string guessResult = GenerateGuessResult(guess);
            UpdateCanContinue(guessResult);

            return guessResult;
        }

        private void UpdateCanContinue(string guessResult)
        {
            counter++;

            if (guessResult == "4A0B")
            {
                IsWinner = true;
                CanContinue = false;
            }
            else if (counter >= 6)
            {
                CanContinue = false;
            }
        }

        private string GenerateGuessResult(string guess)
        {
            char[] guessArr = guess.ToCharArray();

            (int numOfA, int numOfB) = CountMatchedAndCorrectDigits(guessArr);

            return $"{numOfA}A{numOfB}B";
        }

        private (int, int) CountMatchedAndCorrectDigits(char[] guessArr)
        {
            char[] secretArr = secret.ToCharArray();
            int fullMatchedDigitNum = CountFullMatchGuessDigits(guessArr, secretArr);

            int correctNotMatchedDigitNum = CountCorrectGuessDigits(guessArr, secretArr) - fullMatchedDigitNum;

            return (fullMatchedDigitNum, correctNotMatchedDigitNum);
        }

        private int CountFullMatchGuessDigits(char[] guessArr, char[] secretArr)
        {
            int count = 0;
            for (int i = 0; i < guessArr.Length; i++)
            {
                if (guessArr[i] == secretArr[i])
                {
                    count++;
                }
            }

            return count;
        }

        private int CountCorrectGuessDigits(char[] guessArr, char[] secretArr)
        {
            int count = 0;
            for (int i = 0; i < guessArr.Length; i++)
            {
                if (secretArr.Contains(guessArr[i]))
                {
                    count++;
                }
            }

            return count;
        }
    }
}