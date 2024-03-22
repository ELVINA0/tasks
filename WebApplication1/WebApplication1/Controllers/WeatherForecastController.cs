using Microsoft.AspNetCore.Mvc;

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

            string processedString = string.Empty;

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

            return Ok(processedString);
        }
    }
}
