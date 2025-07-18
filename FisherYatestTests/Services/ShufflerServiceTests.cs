// <copyright file="ShufflerServiceTests.cs" company="Skd">
// Copyright (c) Skd. All rights reserved.
// </copyright>

namespace FisherYatestTests.Services
{
    using FisherYates.Services.Interfaces;
    using FisherYatestTests.Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Contains unit tests for verifying the behavior and correctness of implementations of the <see cref="IShufflerService"/> interface.
    /// </summary>
    /// <remarks>This test class ensures that various implementations of <see cref="IShufflerService"/>
    /// conform to expected behaviors, including handling edge cases, preserving input integrity, and producing valid
    /// permutations. The tests cover scenarios such as empty input, single-element input, identical elements, and large
    /// inputs, as well as verifying randomness and correctness of shuffle results.</remarks>
    [TestClass]
    public class ShufflerServiceTests
    {
        private const int SampleCount = 100;

        /// <summary>
        /// Gets a collection of all implementations of the <see cref="IShufflerService"/> interface.
        /// </summary>
        private static IEnumerable<object[]> ShufflerImplementations => AssemblyHelper.GetAllImplementations<IShufflerService>();

        /// <summary>
        /// Tests that the <see cref="IShufflerService.Shuffle"/> method returns an empty string when the input string is empty.
        /// </summary>
        /// <remarks>This test ensures that the shuffle operation handles edge cases correctly,
        /// specifically when the input string is empty. The expected behavior is that the method returns an empty
        /// string without throwing exceptions or altering the input.</remarks>
        /// <param name="shufflerService">An implementation of <see cref="IShufflerService"/> used to perform the shuffle operation.</param>
        [DataTestMethod]
        [DynamicData(nameof(ShufflerImplementations), DynamicDataSourceType.Property)]
        public void Shuffle_EmptyString_ReturnsEmpty(IShufflerService shufflerService)
        {
            var input = string.Empty;
            var result = shufflerService.Shuffle(input);
            Assert.AreEqual(string.Empty, result, "Expected idempotent result.");
        }

        /// <summary>
        /// Tests that the <see cref="IShufflerService.Shuffle"/> method returns the same value when the input consists of a single element.
        /// </summary>
        /// <remarks>This test ensures that shuffling a single-element input does not alter the input, as there are no additional permutations possible.</remarks>
        /// <param name="shufflerService">The implementation of <see cref="IShufflerService"/> to test.</param>
        [DataTestMethod]
        [DynamicData(nameof(ShufflerImplementations), DynamicDataSourceType.Property)]
        public void Shuffle_SingleElement_ReturnsSame(IShufflerService shufflerService)
        {
            var input = "A";
            var result = shufflerService.Shuffle(input);
            Assert.AreEqual("A", result, "Expected idempotent result.");
        }

        /// <summary>
        /// Tests that the <see cref="IShufflerService.Shuffle(string)"/> method returns the same string when all elements in the input are identical.
        /// </summary>
        /// <remarks>This test verifies that shuffling a string with identical elements does not alter the input,
        /// ensuring that the shuffle operation preserves it when all elements are indistinguishable.</remarks>
        /// <param name="shufflerService">An implementation of <see cref="IShufflerService"/> used to perform the shuffle operation.</param>
        [DataTestMethod]
        [DynamicData(nameof(ShufflerImplementations), DynamicDataSourceType.Property)]
        public void Shuffle_AllElementsIdentical_ReturnsSame(IShufflerService shufflerService)
        {
            var input = "A-A-A-A";
            for (int i = 0; i < SampleCount; i++)
            {
                var result = shufflerService.Shuffle(input);
                Assert.AreEqual(input, result, "Expected idempotent result.");
            }
        }

        /// <summary>
        /// Tests that the <see cref="IShufflerService.Shuffle"/> method always returns valid permutations of the input string.
        /// </summary>
        /// <remarks>This test verifies that the shuffled output contains the same elements only as the input, regardless of the order.</remarks>
        /// <param name="shufflerService">An implementation of <see cref="IShufflerService"/> used to perform the shuffle operation.</param>
        [DataTestMethod]
        [DynamicData(nameof(ShufflerImplementations), DynamicDataSourceType.Property)]
        public void Shuffle_AlwaysReturnsValidPermutations(IShufflerService shufflerService)
        {
            var input = "A-B-C-D-E";
            var inputParts = input.Split('-');

            for (int i = 0; i < SampleCount; i++)
            {
                var result = shufflerService.Shuffle(input);
                var resultParts = result.Split('-');
                CollectionAssert.AreEquivalent(inputParts, resultParts, "Expected identical elements.");
            }
        }

        /// <summary>
        /// Tests whether the <see cref="IShufflerService.Shuffle"/> method preserves all elements  when applied to a large input string.
        /// </summary>
        /// <remarks>This test verifies that the shuffled output contains the same elements as the input, regardless of their order.</remarks>
        /// <param name="shufflerService">An implementation of <see cref="IShufflerService"/> used to perform the shuffle operation.</param>
        [DataTestMethod]
        [DynamicData(nameof(ShufflerImplementations), DynamicDataSourceType.Property)]
        public void Shuffle_LargeInput_AllElementsPreserved(IShufflerService shufflerService)
        {
            var elements = Enumerable.Range(0, SampleCount).Select(i => i.ToString()).ToArray();
            var input = string.Join("-", elements);

            var result = shufflerService.Shuffle(input);
            var resultParts = result.Split('-');

            CollectionAssert.AreEquivalent(elements, resultParts, "Expected identical elements in large input.");
        }

        /// <summary>
        /// Tests that the <see cref="IShufflerService.Shuffle"/> method produces both possible permutations of a two-element input string.
        /// </summary>
        /// <remarks>This test verifies that the shuffle operation generates both "A-B" and "B-A" permutations when applied to the input string "A-B".
        /// The test runs multiple iterations to ensure that both permutations are observed.</remarks>
        /// <param name="shufflerService">An implementation of <see cref="IShufflerService"/> used to perform the shuffle operation.</param>
        [DataTestMethod]
        [DynamicData(nameof(ShufflerImplementations), DynamicDataSourceType.Property)]
        public void Shuffle_TwoElements_ProducesBothPermutations(IShufflerService shufflerService)
        {
            var input = "A-B";
            var seen = new HashSet<string>();

            for (int i = 0; i < SampleCount; i++)
            {
                seen.Add(shufflerService.Shuffle(input));
            }

            Assert.IsTrue(seen.Contains("A-B"), "Expected at least one A-B permutation.");
            Assert.IsTrue(seen.Contains("B-A"), "Expected at least one B-A permutation.");
        }

        /// <summary>
        /// Verifies that the <see cref="IShufflerService.Shuffle"/> method produces different results when called multiple times with the same input.
        /// </summary>
        /// <remarks>This test ensures that the shuffle operation introduces variability in its output
        /// over  repeated calls, which is a key characteristic of a shuffling algorithm.</remarks>
        /// <param name="shufflerService">An implementation of <see cref="IShufflerService"/> used to perform the shuffle operation.</param>
        [DataTestMethod]
        [DynamicData(nameof(ShufflerImplementations), DynamicDataSourceType.Property)]
        public void Shuffle_ReturnsDifferentResultsOverTime(IShufflerService shufflerService)
        {
            var input = "A-B-C";
            var seen = new HashSet<string>();

            for (int i = 0; i < SampleCount; i++)
            {
                var result = shufflerService.Shuffle(input);
                seen.Add(result);
            }

            Assert.IsTrue(seen.Count > 1, "Expected multiple distinct shuffle results.");
        }

        /// <summary>
        /// Performs a statistical randomness test to ensure that the <see cref="IShufflerService.Shuffle"/> method
        /// distributes permutations in a reasonably uniform manner over multiple iterations.
        /// </summary>
        /// <remarks>
        /// This test is not meant to prove cryptographic randomness but to detect biased or deterministic shuffling.
        /// It counts the frequency of each permutation and checks that no single permutation dominates the output.
        /// </remarks>
        /// <param name="shufflerService">An implementation of <see cref="IShufflerService"/> to test.</param>
        [DataTestMethod]
        [DynamicData(nameof(ShufflerImplementations), DynamicDataSourceType.Property)]
        public void Shuffle_NonDeterministic_DistributionIsReasonablyUniform(IShufflerService shufflerService)
        {
            var input = "A-B-C";
            var permutations = new Dictionary<string, int>();
            int iterations = 1000;

            for (int i = 0; i < iterations; i++)
            {
                var result = shufflerService.Shuffle(input);
                if (permutations.ContainsKey(result))
                {
                    permutations[result]++;
                }
                else
                {
                    permutations[result] = 1;
                }
            }

            // We expect all 6 permutations of 3 elements.
            var expectedPermutations = new HashSet<string>
            {
                "A-B-C", "A-C-B", "B-A-C", "B-C-A", "C-A-B", "C-B-A",
            };

            // Ensure all permutations are observed at least once
            foreach (var expected in expectedPermutations)
            {
                Assert.IsTrue(permutations.ContainsKey(expected), $"Expected to observe permutation: {expected}");
            }

            // Check distribution is not heavily skewed, could be adjusted to chi-squared test or similar for more rigorous statistical analysis
            int minThreshold = iterations / expectedPermutations.Count / 2; // Accept 50% tolerance
            foreach (var kvp in permutations)
            {
                Assert.IsTrue(kvp.Value > minThreshold, $"Permutation '{kvp.Key}' appears suspiciously few times: {kvp.Value}");
            }
        }
    }
}