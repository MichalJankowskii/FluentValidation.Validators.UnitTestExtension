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

namespace FluentValidation.Validators.UnitTestExtension.Tests.Helpers
{
    using System;
    using Xunit;

    public static class AssertExtension
    {
        public static void NotThrows(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception has been thrown", ex);
            }
        }

        public static void Throws<T>(Action action, string subString) where T: Exception
        {
            if (string.IsNullOrEmpty(subString))
            {
                throw new ArgumentException("subString");
            }

            Exception exception = Assert.Throws<T>(action);
            Assert.True(exception.Message.Contains(subString), "Not found substring: " + subString + " in: " + exception.Message);
        }
    }
}
