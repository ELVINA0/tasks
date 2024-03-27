using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace StringManipulationApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StringManipulationController : ControllerBase
    {
        [HttpPost]
        public ActionResult<string> ProcessString([FromBody] string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
            {
                return BadRequest("Input string cannot be empty");
            }

            // Check if the input string contains only lowercase English alphabet letters
            string invalidCharacters = new string(inputString.Where(c => !Char.IsLetter(c) || !Char.IsLower(c) || c < 'a' || c > 'z').ToArray());
            if (!string.IsNullOrEmpty(invalidCharacters))
            {
                return BadRequest($"Invalid character(s) detected: {invalidCharacters}");
            }

            string processedString = string.Empty;
            Dictionary<char, int> charCount = new Dictionary<char, int>();

            foreach (char c in inputString)
            {
                if (charCount.ContainsKey(c))
                {
                    charCount[c]++;
                }
                else
                {
                    charCount[c] = 1;
                }
            }

            if (inputString.Length % 2 == 0)
            {
                int middleIndex = inputString.Length / 2;
                string firstHalf = inputString.Substring(0, middleIndex);
                string secondHalf = inputString.Substring(middleIndex);

                char[] reversedFirstHalf = firstHalf.ToCharArray();
                Array.Reverse(reversedFirstHalf);
                string reversedFirstHalfStr = new string(reversedFirstHalf);

                char[] reversedSecondHalf = secondHalf.ToCharArray();
                Array.Reverse(reversedSecondHalf);
                string reversedSecondHalfStr = new string(reversedSecondHalf);

                processedString = reversedFirstHalfStr + reversedSecondHalfStr;
            }
            else
            {
                char[] charArray = inputString.ToCharArray();
                Array.Reverse(charArray);
                string reversedString = new string(charArray);

                processedString = reversedString + inputString;
            }

            return Ok(new { processedString, charCount });
        }
    }
}
