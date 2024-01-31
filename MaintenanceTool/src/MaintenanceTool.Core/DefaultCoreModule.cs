using Autofac;
using MaintenanceTool.Core.Interfaces;
using MaintenanceTool.Core.Services;

namespace MaintenanceTool.Core;

public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<ToDoItemSearchService>()
      .As<IToDoItemSearchService>().InstancePerLifetimeScope();
    builder.RegisterType<GroupConfigSearchService>()
      .As<IGroupConfigSearchService>();
  }
}
