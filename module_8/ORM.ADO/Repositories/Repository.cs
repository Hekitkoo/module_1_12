using System.Data.SqlClient;

namespace ORM.ADO.Repositories
{
    public abstract class Repository
    {
        private readonly SqlConnection context;

        private readonly SqlTransaction transaction;

        protected Repository(SqlTransaction transaction, SqlConnection context)
        {
            this.transaction = transaction;
            this.context = context;
        }

        protected SqlCommand CreateCommand(string query)
        {
            return new(query, context, transaction);
        }
    }
}