using System;
using System.Linq;
using NUnit.Framework;

namespace TddOneOhOne
{
    [TestFixture]
    public class BankOcrTests
    {
        [Test]
        public void GivenMalformedInput_ThenShouldThrowException()
        {
            const string input = " _ \n" +
                                 "| |";

            var target = new Ocr();
            Assert.Throws<MalformedDigitsException>(() => target.Recognize(input));
        }

        [Test]
        public void GivenMalformedLines_ThenShouldThrowException()
        {
            const string input = "    _  _     _  _  _  _  _ \n" +
                                 "  | _| _||_||_ |_   ||_||_|\n" +
                                 "  ||_  _|  | _||_||\n";

            var target = new Ocr();
            Assert.Throws<MalformedDigitsException>(() => target.Recognize(input));
        }

        [Test]
        public void GivenUnrecognizedDigit_ThenShouldThrowException()
        {
            const string input = "   \n" +
                                 "|_|\n" +
                                 "| |\n";

            var target = new Ocr();
            Assert.Throws<MalformedDigitsException>(() => target.Recognize(input));
        }

        [Test]
        public void GivenAllDigits_ThenAllRecognizedDigitsReturned()
        {
            const string input = "    _  _     _  _  _  _  _ \n" +
                                 "  | _| _||_||_ |_   ||_||_|\n" +
                                 "  ||_  _|  | _||_|  ||_| _|\n";

            var target = new Ocr();
            Assert.That(target.Recognize(input), Is.EquivalentTo(Enumerable.Range(1, 9)));
        }
    }
}
