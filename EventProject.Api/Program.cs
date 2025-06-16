using EventProject.Core;
using EventProject.Api;
using NHibernate;

var builder = WebApplication.CreateBuilder(args);

// 1. Add Connection String
builder.Configuration.AddJsonFile("appsettings.json");

// 2. Configure NHibernate Session Factory as a Singleton
builder.Services.AddSingleton(provider => new NHibernateHelper(provider.GetRequiredService<IConfiguration>()));
builder.Services.AddSingleton(x => x.GetRequiredService<NHibernateHelper>().OpenSession().SessionFactory);


// 3. Register NHibernate ISession with a scoped lifetime, tied to the HTTP request
builder.Services.AddScoped<NHibernate.ISession>(provider =>
{
    var factory = provider.GetRequiredService<ISessionFactory>();
    return factory.OpenSession();
});

// 4. Register Services for Dependency Injection 
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<ITicketService, TicketService>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS policy to allow frontend access
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use the CORS policy
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();