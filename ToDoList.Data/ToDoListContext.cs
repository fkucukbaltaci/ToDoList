using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ToDoList.Entity;

namespace ToDoList.Data
{
    public class ToDoListContext : DbContext
    {
        private static System.Data.Entity.SqlServer.SqlProviderServices _instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        public ToDoListContext() : base("name=connectionString")
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ToDoListContext>());
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Todo> Todos { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<Log> Logs { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }


   }
}
