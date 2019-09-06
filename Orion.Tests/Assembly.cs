using System.Diagnostics.CodeAnalysis;
using Xunit;

/*
 * Ideally we would be able to run the tests in parallel. However, there is simply far too much static state in
 * Terraria that we're testing in these tests. So we need to run the tests in series.
 */
[assembly: CollectionBehavior(DisableTestParallelization = true)]

/*
 * Don't bother including tests as part of the code coverage. This reduces noise.
 */
[assembly: ExcludeFromCodeCoverage]
