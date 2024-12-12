using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace WeddingPlanner.Controllers
{
    [ApiController]
    [Route("/chatbox")]
    public class ChatboxController : ControllerBase
    {
        private readonly ILogger<ChatboxController> _logger;

        public ChatboxController(ILogger<ChatboxController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post([FromBody] ChatRequest request)
        {
            _logger.LogInformation("Received user message: " + request.UserMessage);  // Log the user message

            // Generate the bot response based on the user message
            string botResponse = GetBotResponse(request.UserMessage);

            _logger.LogInformation("Bot response: " + botResponse);  // Log the bot's response

            return Ok(new { botMessage = botResponse });
        }

        private string GetBotResponse(string userMessage)
        {
            // Predefined responses to specific words/phrases
            var keywordResponses = new Dictionary<string, string>(System.StringComparer.OrdinalIgnoreCase)
    {
        { "hello", "Hi there! How can I assist you?" },
        { "help", "Sure! Let me know what you need help with." },
        { "wedding", "Are you looking for wedding planning tips or services?" },
        { "flowers", "We have a variety of flowers for your special day! Let me know your preferences." },
        { "food", "Our catering service offers a wide range of cuisines. Do you have a specific cuisine in mind?" },
        { "location", "Looking for a wedding venue? We have several options to suit your needs!" }
    };

            // Use a regex pattern to find a match for each keyword
            foreach (var keyword in keywordResponses.Keys)
            {
                var regex = new System.Text.RegularExpressions.Regex(@"\b" + System.Text.RegularExpressions.Regex.Escape(keyword) + @"\b", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                if (regex.IsMatch(userMessage)) // Match whole word boundaries
                {
                    return keywordResponses[keyword];
                }
            }

            // Default response if no keywords match
            return "I'm sorry, I didn't understand that. Could you please rephrase?";
        }
    }

    // Model for the chat request
    public class ChatRequest
    {
        public string UserMessage { get; set; }
    }
}