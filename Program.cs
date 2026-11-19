using LocadoraVeiculos.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LocadoraContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler();
// Hospeda frontend na mesma origem da API para evitar CORS.
var frontend = Path.Combine(app.Environment.ContentRootPath, "frontend");
var provider = new PhysicalFileProvider(frontend);
app.UseDefaultFiles(new DefaultFilesOptions { FileProvider = provider });
app.UseStaticFiles(new StaticFileOptions { FileProvider = provider });
app.MapControllers();
app.Run();
