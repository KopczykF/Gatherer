using System;
using System.Threading.Tasks;
using Gatherer.Core.Domain;
using Gatherer.Core.Repositories;
using Gatherer.Infrastructure.Repositories;
using Xunit;

namespace Gatherer.Tests.Repositories
{
    public class UserRepositoryTests
    {
        [Fact]
        public async Task when_adding_new_user_it_should_be_added_correctly_to_the_lists()
        {
            //Arrange
            var user = new User(Guid.NewGuid(), "user", "test", "test@email.com", "secret", "salt");
            IUserRepository repository = new UserRepository();
            //Act
            await repository.AddAsync(user);
            //Assert
            var existingUser = await repository.GetAsync(user.Id);
            Assert.Equal(user, existingUser);

        }
    }
}