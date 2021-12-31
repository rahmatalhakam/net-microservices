using System;
using System.Collections.Generic;
using System.Linq;
using CommandsService.Models;

namespace CommandsService.Data
{
  public class CommandRepo : ICommandRepo
  {
    private readonly AppDbContext _context;

    public CommandRepo(AppDbContext context)
    {
      _context = context;
    }
    public void CreateCommand(int platformId, Command command)
    {
      if (command == null) throw new ArgumentNullException(nameof(command));
      command.PlatformId = platformId;
      _context.Commands.Add(command);
    }

    public void CreatePlatform(Platform plat)
    {
      if (plat == null) throw new ArgumentException(nameof(plat));
      _context.Platforms.Add(plat);
    }

    public IEnumerable<Platform> GetAllPltforms()
    {
      return _context.Platforms.ToList();
    }

    public Command GetCommand(int platformId, int commandId)
    {
      return _context.Commands.Where(c => c.PlatformId == platformId && c.Id == commandId).FirstOrDefault();
    }

    public IEnumerable<Command> GetCommandsForPlatform(int platformid)
    {
      return _context.Commands.Where(c => c.PlatformId == platformid).OrderBy(c => c.Platform.Name);
    }

    public bool PlatformExits(int platformid)
    {
      return _context.Platforms.Any(p => p.Id == platformid);
    }

    public bool SaveChanges()
    {
      return (_context.SaveChanges() >= 0);
    }
  }
}