using API.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace API
{
    public class SetupService
    {
        public static void Init(DataContext context)
        {
            context.Database.Migrate();
        }
    }
}
