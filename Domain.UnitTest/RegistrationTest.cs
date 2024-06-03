using Domain.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Domain.UnitTest
{
    public class RegistrationTest
    {
        [Theory]
        [InlineData("", "", "", "", "")]
        [InlineData(" ", " ", " ", " ", " ")]
        [InlineData(null, null, null, null, null)]
        public void CreateRegistrationRequest_IfDataIsNullOrEmpty_ShouldThrowValidationException(
            string firstName,
            string lastName,
            string email,
            string userName,
            string password)
        {
            // Arrange
            var request = new RegistrationRequest
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = userName,
                Password = password
            };

            // Act 
            var validationContext = new ValidationContext(request);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(request, validationContext, validationResults, true);


            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains(nameof(request.FirstName)));
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains(nameof(request.LastName)));
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains(nameof(request.Email)));
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains(nameof(request.UserName)));
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains(nameof(request.Password)));
        }

        [Theory]
        [InlineData("Carl", "Jenkins", "carljenkins@gmail.com", "kkkk", "kkkkk")]
        public void CreateRegistrationRequest_IfDataLengthIsLessThan6_ShouldThrowValidationException(
            string firstName,
            string lastName,
            string email,
            string userName,
            string password)
        {
            // Arrange
            var request = new RegistrationRequest
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = userName,
                Password = password
            };

            // Act
            var validationContext = new ValidationContext(request);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(request, validationContext, validationResults, true);
            
            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.ErrorMessage.Contains("Username must be at least 6 characters long."));
            Assert.Contains(validationResults, vr => vr.ErrorMessage.Contains("Password must be at least 6 characters long."));
        }

     
        [Theory]
        [InlineData("Carl", "Jenkins", "carljenkins@gmail.com", "kkkkSSS", "testssss")]
        [InlineData("Carl", "Jenkins", "carljenkins@gmail.com", "kkkkSSS", "test1234")]
        [InlineData("Carl", "Jenkins", "carljenkins@gmail.com", "kkkkSSS", "Test1234")]
        [InlineData("Carl", "Jenkins", "carljenkins@gmail.com", "kkkkSSS", "Test 1234$")]
        [InlineData("Carl", "Jenkins", "carljenkins@gmail.com", "kkkkSSS", "TEST1234$")]
        public void CreateRegistrationRequest_IfPasswordIsValidWithoutLength_ShouldThrowValidationException(
            string firstName,
            string lastName,
            string email,
            string userName,
            string password)
        {
            // Arrange
            var request = new RegistrationRequest
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = userName,
                Password = password
            };

            // Act
            var validationContext = new ValidationContext(request);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(request, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
        }

        [Theory]
        [InlineData("Carl", "Jenkins", "carljenkinsgmail.com", "kkkkSSS", "Test1234$")]
        [InlineData("Carl", "Jenkins", "carljenkins@gmailcom", "kkkkSSS", "Test1234$")]
        [InlineData("Carl", "Jenkins", "carl@21312", "kkkkSSS", "Test1234$")]
        [InlineData("Carl", "Jenkins", "11111.com", "kkkkSSS", "Test1234$")]
        [InlineData("Carl", "Jenkins", "asdad.com", "kkkkSSS", "Test1234$")]
        [InlineData("Carl", "Jenkins", "asdad", "kkkkSSS", "Test1234$")]
        public void CreateRegistrationRequest_IfEmailIsValid_ShouldThrowValidationException(
            string firstName,
            string lastName,
            string email,
            string userName,
            string password)
        {
            // Arrange
            var request = new RegistrationRequest
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = userName,
                Password = password
            };

            // Act
            var validationContext = new ValidationContext(request);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(request, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid, $"Email '{email}' should be invalid.");
        }
    }
}
    