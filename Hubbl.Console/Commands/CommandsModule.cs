using Autofac;

namespace Hubbl.Console.Commands
{
	class CommandsModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);
			builder.RegisterType<QuitCommand>()
				.As<ICommand>()
				.SingleInstance();
			builder.RegisterType<UserListCommand>()
				.As<ICommand>()
				.SingleInstance();
			builder.RegisterType<SendTextCommand>()
				.As<ICommand>()
				.SingleInstance();
			builder.RegisterType<HelpCommand>()
				.As<ICommand>()
				.SingleInstance();
			builder.RegisterType<HelloCommand>()
				.As<ICommand>()
				.SingleInstance();
			builder.RegisterType<SendFileCommand>()
				.As<ICommand>()
				.SingleInstance();
            builder.RegisterType<BecomeClientCommand>()
                .As<ICommand>()
                .SingleInstance(); 
            builder.RegisterType<BecomeServerCommand>()
                .As<ICommand>()
                .SingleInstance();
            builder.RegisterType<AddCloudTrackCommand>()
                .As<ICommand>()
                .SingleInstance();
            builder.RegisterType<TrackListCommand>()
                .As<ICommand>()
                .SingleInstance();
        }
	}
}
