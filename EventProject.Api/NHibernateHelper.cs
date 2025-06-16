using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using System.Reflection;
using NHibernate;
using Microsoft.Extensions.Configuration;
using System.IO;
using NHibernate.Tool.hbm2ddl; // <--- Add this using directive for SchemaValidator

namespace EventProject.Api;

public class NHibernateHelper
{
    private readonly ISessionFactory _sessionFactory;

    public NHibernateHelper(IConfiguration configuration)
    {
        var cfg = new Configuration();
        cfg.DataBaseIntegration(db =>
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            db.ConnectionString = connectionString;
            db.Driver<SQLite20Driver>();
            db.Dialect<SQLiteDialect>();
            db.LogSqlInConsole = true; // Useful for debugging
            db.LogFormattedSql = true;
        });

        // Load all mappings from the Core assembly where your Entities and their mappings reside
        cfg.AddAssembly(typeof(EventProject.Core.Entities.Events).Assembly);

        // --- Correct way to perform Schema Validation without Fluent NHibernate ---
        // Create a new SchemaValidator instance with your configuration
        var schemaValidator = new SchemaValidator(cfg);

        // Execute the validation. This will throw an exception if mappings don't match the DB schema.
        // It's good to do this at startup to catch mapping mismatches early.
        // For production, you might put this behind a flag or in a health check.
        schemaValidator.Validate();


        _sessionFactory = cfg.BuildSessionFactory();
    }

    public NHibernate.ISession OpenSession()
    {
        return _sessionFactory.OpenSession();
    }
}