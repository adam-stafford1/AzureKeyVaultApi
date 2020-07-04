using System;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using AzureKeyVaultApi.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AzureKeyVaultApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KeyVaultController : ControllerBase
    {
        private readonly ILogger<KeyVaultController> _logger;

        public KeyVaultController(ILogger<KeyVaultController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetSecret(string key)
        {
            var kvUri = $"https://{Environment.GetEnvironmentVariable("KEY_VAULT_NAME")}.vault.azure.net";

            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());

            var secret = await client.GetSecretAsync(key);

            return Ok(secret.Value.Value);
        }

        [HttpPost]
        public async Task<IActionResult> SetSecret([FromBody] KeyValueDto secret)
        {
            var kvUri = $"https://{ Environment.GetEnvironmentVariable("KEY_VAULT_NAME")}.vault.azure.net";

            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());

            var response = await client.SetSecretAsync(secret.Key, secret.Value);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSecret([FromBody] string key)
        {
            var kvUri = $"https://{ Environment.GetEnvironmentVariable("KEY_VAULT_NAME")}.vault.azure.net";

            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());

            var response = await client.StartDeleteSecretAsync(key);

            return Ok(response);
        }
    }
}
