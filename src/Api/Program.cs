using Api.Mappers;
using Api.DependencyInjection;
using Api.Middlewares;
using Api.Endpoints;
using Api.Models.Settings;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllerAndJsonConfigurations();
builder.Services.AddHealthChecks();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddFluentValidation();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWTSettings"));
builder.Services.AddSwaggerOpenAPI();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnlyPolicy", policy => policy.RequireRole("admin"));
});
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", $"Minimal Api Practices");
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    c.InjectStylesheet("/css/swagger-ui/swagger-dark.css");
});
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseHealthChecks("/health");
app.UseStaticFiles();
app.UseExceptionHandler();

// routes
#region [ Api Routes ]
app.ConfigureCouponEndpoints();
app.ConfigureAuthEndpoints();
#endregion


app.Run();

