using EmptyProjectTesting.Entites.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmptyProjectTesting.Entity_Configuration_Auth
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //throw new NotImplementedException();
            builder.HasKey(x => x.Id);
        }
    }
}
    