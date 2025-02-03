using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NumberClassificationAPI.Controllers
{
    [Route("api/classify-number")]
    [ApiController]
    public class NumberClassificationController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        public NumberClassificationController()
        {
            _httpClient = new HttpClient();
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string number)
        {

            if (!int.TryParse(number, out int num))
            {
                return BadRequest(new { number, error = true });
            }
            var isPrime = IsPrime(num);
            var isPerfect = IsPerfect(num);
            var isArmstrong = IsArmstrong(num);
            var isOdd = num % 2 != 0;
            var digitSum = num.ToString().Sum(ch => ch - '0');
            var properties = new List<string>();

            if (isArmstrong)
                properties.Add("armstrong");
            properties.Add(isOdd ? "odd" : "even");

            var funFact = await GetFunFactAsync(num);

            return Ok(new
            {
                number = num,
                is_prime = isPrime,
                is_perfect = isPerfect,
                properties,
                digit_sum = digitSum,
                fun_fact = funFact
            });
        }

        private bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = Math.Sqrt(number);

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0) return false;

            return true;
        }

        private bool IsPerfect(int number)
        {
            var boundary = Math.Sqrt(number);
            int sum = 1;
            for (int i = 2; i <= boundary; i++)
            {
                if (number % i == 0)
                {
                    sum += i;
                    if (i != number / i)
                        sum += number / i;
                }
            }

            return sum == number && number != 1;
        }

        private bool IsArmstrong(int number)
        {
            int sum = 0;
            int temp = number;
            int numDigits = number.ToString().Length;

            while (temp > 0)
            {
                int digit = temp % 10;
                sum += (int)Math.Pow(digit, numDigits);
                temp /= 10;
            }
            return sum == number;

        }

        private async Task<string> GetFunFactAsync(int number)
        {
            var response = await _httpClient.GetAsync($"http://numbersapi.com/{number}/math?json");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                dynamic? data = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                return data.text;
            }

            return $"No fun fact found for {number}.";
        }
    }
}
