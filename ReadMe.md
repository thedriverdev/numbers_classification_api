# HNG12 STAGE-1: NUMBER CLASSIFICATION API

This is a simple API that classifies numbers based on their mathematical properties,
and returns a fun fact about the number using the Numbers API.

## Features

* Classifies numbers as prime, perfect, or Armstrong numbers.
* Identifies if the number is odd or even.
* Calculates the sum of the digits.
* Fetches a fun fact from the Numbers API
* Returns responses in JSON format.
* Handles CORS for cross-origin requests.

## Technologies Used

* Backend Framework: ASP.NET Core
* Language: C#
* Hosting: SmarterASP.NET
* CORS Handling: Configured to allow cross-origin requests
* Version Control: Git and GitHub
* Deployment: FTP

## API Documentation

### Endpoint:

GET
api/classify-number?number=666

Response Format (200 OK)
```json
{
  "number": 666,
  "is_prime": false,
  "is_perfect": false,
  "properties": [
    "even"
  ],
  "digit_sum": 18,
  "fun_fact": "666 is the largest rep-digit triangular number."
}

```
GET
api/classify-number?number=God

Response Format (400 Bad Request)
```json
{
  "number": "Thanos",
  "error": true
}

```

## Author

Alfred Mamman Odey
@TheDriverDev
HNG12 Backend Intern