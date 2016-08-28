using System;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;

namespace Hubbl.Mobile.PageModels
{
	[ImplementPropertyChanged]
	public class NicknamePageModel : FreshBasePageModel
	{
		public NicknamePageModel()
		{
		    Username = "Hendr1x";
		}

		public string Username { get; set; }

		public Command AcceptCommand
		{
			get
			{
				return new Command(() => {
					var page = FreshPageModelResolver.ResolvePageModel<HubsListPageModel>();
					var basicNavContainer = new FreshNavigationContainer(page);
					Application.Current.MainPage = basicNavContainer;
				});
			}
		}

		public Command ChangeNameCommand =>
			new Command(() =>
			{
				Username = new Random().Next().ToString();
			});

	}
}
