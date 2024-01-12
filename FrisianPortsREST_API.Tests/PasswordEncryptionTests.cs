using FrisianPortsREST_API;
using FrisianPortsREST_API.Repositories;
using Xunit;

namespace FrisianPortsREST_API.Tests
{
    public class PasswordEncryptionTests
    {
        [Fact]
        public void Check_PasswordHashingCorrectness0() 
        {
            //Arrange
            string expectedValue = "LeQqyIH+pwGtHuEN2Sh4y3dzzA/ycRCNbZ4NkQpXc452PtlFGALSfK5TiKXPUEzKmuNxyH81l0m1OPJ7olbrAg==";


            //Act
            UserRepository userRepository = new UserRepository();
            string actualValue = userRepository.HashPassword("abc123");
        
            //Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Check_PasswordHashingCorrectness1()
        {
            //Arrange
            string expectedValue = "FD0oCMqbZ89KYDnJMlswAz6JlzZj1EcZZjOvl6yR8R2ARBaIAnty4tQQG+w6Bp3sQvLPRZjGG6R5dkFixRkwOg==";

            //Act
            UserRepository userRepository = new UserRepository();
            string actualValue = userRepository.HashPassword("Ibb-2S5il.+99");

            //Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Check_PasswordHashingCorrectness2()
        {
            //Arrange
            string expectedValue = "fcqVzWghvFvyl4fAc4IpP5E/OU3Uy+jTxEmRIvXxQAgRK3jx2wXV8bHUoc2I2Jef4mahMmaRS2Ga5nnQUPHd6Q==";

            //Act
            UserRepository userRepository = new UserRepository();
            string actualValue = userRepository.HashPassword("D}It+WaCHtWooRDSeCurI+y.,:");

            //Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Check_PasswordHashingInCorrectness()
        {
            //Arrange
            string expectedValue = "fcqVzWghvFvyl4fAc4IpP5E/OU3Uy+jTxEmRIvXxQAgRK3jx2wXV8bHUoc2I2Jef4mahMmaRS2Ga5nnQUPHd6Q==";

            //Act
            UserRepository userRepository = new UserRepository();
            string actualValue = userRepository.HashPassword("D}It+WaCHtWooRDSeCurIy.,:");

            //Assert
            Assert.NotEqual(expectedValue, actualValue);
        }

        [Fact]
        public async void Check_PasswordHashingFromDatabase0()
        {
            //Arrange
            string expectedValue = "2005.14+22";

            //Act
            UserRepository userRepository = new UserRepository();
            var actualValue = userRepository.ValidatePassword("test@gmail.com", "2005.14+22");

            //Assert
            Assert.NotNull(actualValue);
        }
    }
}
