using Prometheus;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddHealthChecks().ForwardToPrometheus();

var app = builder.Build();

app.UseMetricServer();
app.UseHealthChecks("/health");
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();