using ApplicationService;
using Microsoft.EntityFrameworkCore;
using SqlPersistence;
using SqlPersistence.Abstract;
using SqlPersistence.Persistence;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Elastic.Clients.Elasticsearch;
using Serilog;
using ElasticPersistence.Abstract;
using ElasticPersistence.Persistence;
using KafkaPersistence;
using KafkaPersistence.Interface;
using Confluent.Kafka;
using Domain.Models.DTO;
using KafkaPersistence.Serializers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(logger));

var settingsElastic = new ElasticsearchClientSettings(new Uri("http://localhost:9200"));

var kafkaConfiguration = new KafkaConfiguration()
{
  Server = builder.Configuration.GetSection("KafkaConfiguration")["Server"],
  Topic = builder.Configuration.GetSection("KafkaConfiguration")["Topic"]
};

settingsElastic.DefaultIndex(kafkaConfiguration.Topic);
var client = new ElasticsearchClient(settingsElastic);
builder.Services.AddSingleton<IElasticsearchService, ElasticsearchService>(builder => new ElasticsearchService(client));

var config = new ProducerConfig()
{
    BootstrapServers = kafkaConfiguration.Server,
    ClientId = "ChallengeN5",
    SecurityProtocol = SecurityProtocol.Plaintext,
    EnableDeliveryReports = false,
    QueueBufferingMaxMessages = 10000000,
    QueueBufferingMaxKbytes = 100000000,
    BatchNumMessages = 500,
    Acks = Acks.None,
    DeliveryReportFields = "none"
};

builder.Services.AddSingleton<IKafkaProducer, KafkaProducer>(options => new KafkaProducer(new ProducerBuilder<string, MessageDTO>(config)
    .SetValueSerializer(new MessageDTOSerializer())
    .Build(), kafkaConfiguration.Topic));

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DummyApplication).Assembly));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
