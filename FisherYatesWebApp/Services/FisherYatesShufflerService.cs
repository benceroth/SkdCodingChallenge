// <copyright file="FisherYatesService.cs" company="Skd">
// Copyright (c) Skd. All rights reserved.
// </copyright>

namespace FisherYates.Services
{
    using FisherYates.Services.Interfaces;

    /// <summary>
    /// Defines a service for shuffling the characters in a string using the modern version of the algorithm.
    /// </summary>
    public class FisherYatesShufflerService : IFisherYatesShufflerService
    {
        /// <summary>
        /// Randomizes the order of characters in the specified string.
        /// </summary>
        /// <param name="input">The string whose characters will be shuffled. Cannot be <see langword="null"/>.</param>
        /// <returns>A new string with the characters of <paramref name="input"/> in randomized order.</returns>
        public string Shuffle(string input)
        {
            throw new NotImplementedException();
        }
    }
}
