using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LSP.Api.Controllers;

[SwaggerTag("ExampleController")]
// [Route("api/[controller]")]
[Route("api/example")]
[ApiController]
public class ExampleController : ControllerBase
{
    // Tag for what I have no idea!
    //[SwaggerOperation(Tags = (new[] { "B", "T", "C" }))]


    [SwaggerOperation(Summary = "Getting Crypto Data", Description = "Getting Crypto Data")]
    [ProducesResponseType(typeof(Crypto), (int)HttpStatusCode.OK)]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "There is not data.", typeof(string))]
    [HttpGet("crypto/{cryptoId:min(1)}")]
    public IActionResult GetCrypto()
    {
        Crypto crypto = new();

        return Ok(crypto);
    }

    //When you want to pass string as summary and description for API
    [SwaggerOperation(Summary = "Creating Crypto Data", Description = "Example Description")]
    //When you want to pass an object as example of response
    [ProducesResponseType(typeof(Crypto), (int)HttpStatusCode.OK)]
    //When you want to pass a string as example of response
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Validation Error.", typeof(string))]
    [HttpPost("crypto")]
    public IActionResult CreateCrypto([Required][FromBody] Crypto crypto)
    {
        return Created("ARestRules/crypto", crypto);
    }

    [SwaggerOperation(Summary = "In general usage Put method uses to update to whole resource.",
        Description = "Example Description")]
    //When you want to pass an object as example of response
    [ProducesResponseType(typeof(Crypto), (int)HttpStatusCode.OK)]
    //When you want to pass a string as example of response
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid Id.", typeof(string))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Crypto Not Found.", typeof(string))]
    [HttpPut("crypto")]
    public IActionResult UpdateWholeCrypto([Required][FromBody] Crypto crypto)
    {
        if (crypto.Id < 0)
            return BadRequest();

        if (crypto.Id != 1)
            return NotFound();

        return Ok(crypto);
    }

    [SwaggerOperation(Summary = "In general usage Patch method uses to update to partial of resource.",
        Description = "Example Description")]
    //When you want to pass an object as example of response
    [ProducesResponseType(typeof(Crypto), (int)HttpStatusCode.OK)]
    //When you want to pass a string as example of response
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid Id.", typeof(string))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Crypto Not Found.", typeof(string))]
    [HttpPatch("{cryptoId:min(1)}")]
    public IActionResult UpdatePartialCrypto(
        [Required][FromRoute] int cryptoId,
        [FromQuery] [Required] [DefaultValue("BTC")] [MaxLength(32)] [MinLength(1)]
        string name,
        [Required] CryptoCategoryEnum category)
    {
        if (cryptoId < 0)
            return BadRequest();

        if (cryptoId != 1)
            return NotFound();

        Crypto crypto = new()
        {
            Name = name,
            Category = category
        };

        return Ok(crypto);
    }

    [SwaggerOperation(Summary = "Deleting Crypto Data.", Description = "Example Description")]
    //When you want to pass an object as example of response
    [ProducesResponseType(typeof(Crypto), (int)HttpStatusCode.OK)]
    //When you want to pass a string as example of response
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid Id.", typeof(string))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Crypto Not Found.", typeof(string))]
    [HttpDelete("{cryptoId:min(1)}")]
    public IActionResult DeleteCrypto(
        [FromRoute] [SwaggerParameter(Description = "Id of Crypto Data")] [Range(1, short.MaxValue)] [DefaultValue(1)]
        int cryptoId)
    {
        Crypto crypto = new();

        if (cryptoId < 0)
            return BadRequest();

        if (cryptoId != 1)
            return NotFound();

        return Ok(crypto);
    }


    public class Crypto
    {
        /// <summary> Id of Crypto Data </summary>
        /// <example>1</example>
        [Required]
        [DefaultValue(1)]
        [Range(1, int.MaxValue)]
        public int Id { get; set; } = 1;

        /// <example>BTC</example>
        [DefaultValue("BTC")]
        [Required]
        [MaxLength(32)]
        [MinLength(1)]
        public string Name { get; set; } = "Crytpo";

        [Required] public CryptoCategoryEnum Category { get; set; } = CryptoCategoryEnum.BTC;

        [Required]
        public List<HistoryOfPrices> PriceHistories { get; set; } = new List<HistoryOfPrices>()
        {
            new HistoryOfPrices()
                { Date = DateTime.Now.AddHours(-10), Price = 123456 },
            new HistoryOfPrices()
                { Date = DateTime.Now.AddHours(-9), Price = 123455 },
            new HistoryOfPrices()
                { Date = DateTime.Now.AddHours(-8), Price = 123454 },
            new HistoryOfPrices()
                { Date = DateTime.Now.AddHours(-7), Price = 123453 },
            new HistoryOfPrices()
                { Date = DateTime.Now.AddHours(-6), Price = 123452 },
        };

        public decimal CurrentValue { get; set; } = 123450;

        public DateTime CreatedDate { get; set; } = DateTime.Now.AddDays(-1);
    }


    /// <summary>
    /// Defines the categories of Crypto
    /// </summary>
    public enum CryptoCategoryEnum
    {
        BTC,
        ETH,
        DOGE
    }

    public class HistoryOfPrices
    {
        [Required] public DateTime Date { get; set; }

        /// <example>234.888</example>
        [Required]
        [Range(1, float.MaxValue)]
        [DefaultValue(234.888)]
        public decimal Price { get; set; }
    }
}