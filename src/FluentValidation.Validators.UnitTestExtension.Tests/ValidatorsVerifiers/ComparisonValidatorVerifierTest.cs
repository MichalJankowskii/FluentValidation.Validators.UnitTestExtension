#region License
// MIT License
// 
// Copyright(c) 2016 Michał Jankowski (http://www.jankowskimichal.pl)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// 
// The latest version of this file can be found at https://github.com/MichalJankowskii/FluentValidation.Validators.UnitTestExtension
#endregion

namespace FluentValidation.Validators.UnitTestExtension.Tests.ValidatorsVerifiers
{
    using System.Collections.Generic;
    using Exceptions;
    using Helpers;
    using Helpers.Fakes;
    using ValidatorVerifiers;
    using Xunit;
    using Xunit.Sdk;

    public class ComparisonValidatorVerifierTest
    {
        [Fact]
        public void Given_CustomValidatorType_When_ConstructingVerifier_Then_Exception()
        {
            // Act & Assert
            Assert.Throws<ComparisonNotProvidedException>(() => new ComparisonValidatorVerifier<FakeComparisonValidator>(10));
        }

        [Fact]
        public void Given_DifferentValidatorType_When_Verifying_Then_ValidationMustFail()
        {
            // Arrange
            var otherValidator = new FakeComparisonValidator() { ValueToCompare = 15, Comparison = Comparison.Equal };
            var verifier = new ComparisonValidatorVerifier<EqualValidator>(10);

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(otherValidator), "(wrong type)");
        }

        [Fact]
        public void Given_CorrectValidatorWithDifferentValueToCompare_When_Verifying_Then_ValidationFail()
        {
            // Arrange
            var comparisonValidator = new FakeComparisonValidator {ValueToCompare = 15, Comparison = Comparison.Equal};
            var verifier = new ComparisonValidatorVerifier<FakeComparisonValidator>(10, Comparison.Equal);

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(comparisonValidator), "(ValueToCompare property)");
        }

        [Fact]
        public void Given_CorrectBuildInValidatorWithDifferentValueToCompare_When_Verifying_Then_ValidationFail()
        {
            // Arrange
            var comparisonValidator = new EqualValidator(10);
            var verifier = new ComparisonValidatorVerifier<EqualValidator>(15);

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(comparisonValidator), "(ValueToCompare property)");
        }

        [Fact]
        public void Given_CorrectValidatorWithDifferentMemberToCompare_When_Verifying_Then_ValidationFail()
        {
            // Arrange
            var memberToCompare1 = typeof(FakeMemeberInfoProvider).GetMember("Member1")[0];
            var memberToCompare2 = typeof(FakeMemeberInfoProvider).GetMember("Member2")[0];
            var comparisonValidator = new FakeComparisonValidator { ValueToCompare = 10, Comparison = Comparison.Equal, MemberToCompare = memberToCompare1};
            var verifier = new ComparisonValidatorVerifier<FakeComparisonValidator>(10, Comparison.Equal, memberToCompare2);

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(comparisonValidator), "(MemberToCompare property)");
        }

        [Fact]
        public void Given_CorrectValidatorWithDifferentComparison_When_Verifying_Then_ValidationFail()
        {
            // Arrange
            var comparisonValidator = new FakeComparisonValidator { ValueToCompare = 10, Comparison = Comparison.NotEqual };
            var verifier = new ComparisonValidatorVerifier<FakeComparisonValidator>(10, Comparison.Equal);

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(comparisonValidator), "(Comparison property)");
        }

        [Fact]
        public void Given_CorrectValidatorWithSameValueAndBuildInValidator_When_Verifying_Then_ValidationPass()
        {
            // Arrange
            var comparisonValidator = new EqualValidator(15);
            var verifier = new ComparisonValidatorVerifier<EqualValidator>(15);

            // Act & Assert
            AssertExtension.NotThrows(() => verifier.Verify(comparisonValidator));
        }

        [Fact]
        public void Given_CorrectValidatorWithSameValueToCompareAndComparison_When_Verifying_Then_ValidationPass()
        {
            // Arrange
            var comparisonValidator = new FakeComparisonValidator { ValueToCompare = 15, Comparison = Comparison.Equal };
            var verifier = new ComparisonValidatorVerifier<FakeComparisonValidator>(15, Comparison.Equal);

            // Act & Assert
            AssertExtension.NotThrows(() => verifier.Verify(comparisonValidator));
        }

        [Fact]
        public void Given_CorrectValidatorWithSameValueToCompareAndComparisonAndMemberToCompare_When_Verifying_Then_ValidationPass()
        {
            // Arrange
            var memberToCompare1A = typeof(FakeMemeberInfoProvider).GetMember("Member1")[0];
            var memberToCompare1B = typeof(FakeMemeberInfoProvider).GetMember("Member1")[0];
            var comparisonValidator = new FakeComparisonValidator { ValueToCompare = 10, Comparison = Comparison.Equal, MemberToCompare = memberToCompare1A };
            var verifier = new ComparisonValidatorVerifier<FakeComparisonValidator>(10, Comparison.Equal, memberToCompare1B);

            // Act & Assert
            AssertExtension.NotThrows(() => verifier.Verify(comparisonValidator));
        }

        [Theory]
        [MemberData("ProvideBuildInComparisonValidatorMapping")]
        public void Given_BuildInValidator_When_Verifying_Then_ValidationPass<T>(ComparisonValidatorVerifier<T> verifier, T validator) where T : IComparisonValidator
        {
            // Act & assert
            AssertExtension.NotThrows(() => verifier.Verify(validator));
        }

        private static IEnumerable<object[]> ProvideBuildInComparisonValidatorMapping()
        {
            // Arrange
            const int sameValue = 1;

            yield return new object[] { new ComparisonValidatorVerifier<EqualValidator>(sameValue), new EqualValidator(sameValue) };
            yield return new object[] { new ComparisonValidatorVerifier<NotEqualValidator>(sameValue), new NotEqualValidator(sameValue) };
            yield return new object[] { new ComparisonValidatorVerifier<LessThanValidator>(sameValue), new LessThanValidator(sameValue) };
            yield return new object[] { new ComparisonValidatorVerifier<LessThanOrEqualValidator>(sameValue), new LessThanOrEqualValidator(sameValue) };
            yield return new object[] { new ComparisonValidatorVerifier<GreaterThanValidator>(sameValue), new GreaterThanValidator(sameValue) };
            yield return new object[] { new ComparisonValidatorVerifier<GreaterThanOrEqualValidator>(sameValue), new GreaterThanOrEqualValidator(sameValue) };
        }

        private class FakeMemeberInfoProvider
        {
            public object Member1 { get; set; }
            public object Member2 { get; set; }
        }
    }
}
