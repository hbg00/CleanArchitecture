using GrpcService.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GrpcService.Model;

namespace GrpcService.Data
{
    public class DbContextgRPC : IdentityDbContext<ApplicationUser>
    {
        public DbContextgRPC(DbContextOptions<DbContextgRPC> options)
            : base(options)
        {
        }
    }
}
