// <copyright file="FisherYatesController.cs" company="Skd">
// Copyright (c) Skd. All rights reserved.
// </copyright>

namespace FisherYates.Controllers
{
    using FisherYates.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller for Fisher-Yates shuffle operations.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class FisherYatesController(IFisherYatesShufflerService sufflerService) : ControllerBase
    {
        /// <summary>
        /// Shuffles a list of dasherized elements using the Fisher-Yates algorithm.
        /// </summary>
        /// <param name="input">List of dasherized elements to shuffle (e.g. "D-B-A-C").</param>
        /// <returns>A "200 OK" HTTP response with a content-type of `text/plain; charset=utf-8`. The content should be the dasherized output of the algorithm, e.g. "C-D-A-B".</returns>
        [HttpGet]
        public ActionResult Index([FromQuery] string input)
        {
            throw new NotImplementedException();
        }
    }
}