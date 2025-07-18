// <copyright file="IFisherYatesService.cs" company="Skd">
// Copyright (c) Skd. All rights reserved.
// </copyright>

namespace FisherYates.Services.Interfaces
{
    /// <summary>
    /// Defines a service for shuffling collections using the Fisher-Yates algorithm.
    /// </summary>
    /// <remarks>The Fisher-Yates algorithm is an efficient method for producing a random permutation of a
    /// finite sequence. Implementations of this interface are expected to shuffle collections in-place or return a
    /// shuffled copy, depending on the specific implementation.</remarks>
    public interface IFisherYatesShufflerService : IShufflerService
    {
    }
}
