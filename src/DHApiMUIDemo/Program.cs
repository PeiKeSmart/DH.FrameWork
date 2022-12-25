using DH.Api.MUI;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins()
                .WithHeaders()
                .WithMethods();
        });
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "天气", Version = "v1" });
    options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "用户", Version = "v1" });
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "DHApiMUIDemo.xml"), true);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
}

//app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);
app.UseFytApiUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "天气", "v1");
    c.SwaggerEndpoint("/swagger/v2/swagger.json", "用户", "v2");
});
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();


app.Run();