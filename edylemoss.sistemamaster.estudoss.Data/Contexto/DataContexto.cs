using Microsoft.EntityFrameworkCore;

namespace edylemoss.sistemamaster.estudoss.Data.Contexto
{
    public class DataContexto: DbContext
    {
        public DataContexto(DbContextOptions<DataContexto> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
