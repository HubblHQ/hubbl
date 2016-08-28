using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;

namespace Hubbl.Mobile.PageModels
{
	[ImplementPropertyChanged]
	public class NicknamePageModel : FreshBasePageModel
	{
		public NicknamePageModel(){}

		public string ButtonText { get { return "Пойдёт"; } }

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
	}
}
