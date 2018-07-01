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

namespace FluentValidation.Validators.UnitTestExtension.Examples.Production
{
    using System;

    public class PersonValidator : AbstractValidator<Person>
    {
        private readonly string regexString = "^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$";

        public PersonValidator()
        {
            this.RuleFor(person => person.FirstName).NotNull().NotEmpty().Length(0, 20);
            this.RuleFor(person => person.LastName).NotNull().NotEmpty().Length(0, 20);
            this.RuleFor(person => person.HeightInCentimeters).GreaterThan(0).LessThanOrEqualTo(250);
            this.RuleFor(person => person.Email).SetValidator(new RegularExpressionValidator(this.regexString));
            this.RuleFor(person => person.Weight).SetValidator(new ScalePrecisionValidator(2, 4));
            this.RuleFor(person => person.FavouriteDay).SetValidator(new EnumValidator(typeof(DayOfWeek)));
            this.RuleFor(person => person.HeightInMeters).InclusiveBetween(0.0, 2.5);
        }
    }
}
