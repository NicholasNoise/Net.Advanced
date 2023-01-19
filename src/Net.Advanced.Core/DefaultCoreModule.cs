using Autofac;
using Net.Advanced.Core.Interfaces;
using Net.Advanced.Core.Services;

namespace Net.Advanced.Core;

public class DefaultCoreModule : Module
{
  /// <inheritdoc/>
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<ToDoItemSearchService>()
        .As<IToDoItemSearchService>().InstancePerLifetimeScope();

    builder.RegisterType<DeleteContributorService>()
        .As<IDeleteContributorService>().InstancePerLifetimeScope();
  }
}
