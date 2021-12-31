using System;
using System.Text;
using System.Text.Json;
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
        _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
        _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        Console.WriteLine($"--> connected to message bus.");
      }
      catch (System.Exception e)
      {
        Console.WriteLine($"--> could not connect to the server {e.Message}");
      }
    }

    // untuk tahu kalo server shutdown
    private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
    {
      Console.WriteLine($"--> Rabbit MQ Connection shutdown.");
    }

    public void PublishNewPlatform(PlatformPublishedDto platformPublishedDto)
    {
      var message = JsonSerializer.Serialize(platformPublishedDto);
      if (_connection.IsOpen)
      {
        Console.WriteLine($"--> RabbitMQ Connection open, sending message...");
        // send message
        SendMessage(message);
      }
      else
      {
        Console.WriteLine($"--> RabbitMQ connections closed, not sending");
      }
    }

    private void SendMessage(string message)
    {
      var body = Encoding.UTF8.GetBytes(message);
      // code utk send disini
      _channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: null, body: body);
      Console.WriteLine($"--> {message} succesfully sent");

    }

    public void Dispose()
    {
      Console.WriteLine("MessageBus Disposed");
      if (_channel.IsOpen)
      {
        _channel.Close();
        _connection.Close();
      }
    }
  }
}