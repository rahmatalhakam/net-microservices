using System.Collections.Generic;
using CommandsService.Models;

namespace CommandsService.Data
{
  public interface ICommandRepo
  {
    bool SaveChanges();
    IEnumerable<Platform> GetAllPltforms();
    void CreatePlatform(Platform plat);

    bool PlatformExits(int platformid);
    IEnumerable<Command> GetCommandsForPlatform(int platformid);

    Command GetCommand(int platformId, int commandId);
    void CreateCommand(int platformId, Command command);
  }
}