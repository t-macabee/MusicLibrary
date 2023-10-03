using API.Data;
using Microsoft.EntityFrameworkCore;

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
