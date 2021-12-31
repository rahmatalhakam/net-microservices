using System;
using Microsoft.Extensions.Configuration;
using PlatformService.Dtos;
using RabbitMQ.Client;

namespace PlatformService.AsyncDataServices
{
  public class MessageBusClient : IMessageBusClient
  {
    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public MessageBusClient(IConfiguration configuration)
    {
      _configuration = configuration;
      var factory = new ConnectionFactory()
      {
        HostName = _configuration["RabbitMQHost"],
        Port = int.Parse(_configuration["RabbitMQPort"])
      };
      try
      {
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
      }
      catch (System.Exception e)
      {
        Console.WriteLine($"--> could not connect to the server {e.Message}");
      }
    }
    public void PublishNewPlatform(PlatformPublishedDto platformPublishedDto)
    {
      throw new NotImplementedException();
    }
  }
}