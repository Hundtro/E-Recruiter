using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace erecruiter
{
    public class ConfigurationController : Controller
    {
        [HttpPost]
        [Route("/SaveConfig")]
        public IActionResult SaveConfig(Configuration config)
        {
            string fileText = JsonSerializer.Serialize(config);

            System.IO.File.WriteAllText("config.json", fileText);

            return Redirect("/Home");
        }
    }
} 
