using System;
using Hubl.Core.Model;
using Hubl.Core.Service;
using Autofac;

using Xamarin.Forms;

namespace Hubl.Mobile
{
	public class NickPage : ContentPage
	{
		Random rand;
		public NickPage ()
		{
			rand = new Random ();
			var nickLabel = new Label{ 
				Text = GenerateNewName (),
				XAlign = TextAlignment.Center,
				FontSize = 24.0,
			};

			var refresh = new Button ();
			refresh.Text = "Обновить";
			refresh.Clicked += (sender, e) => {
				var name = GenerateNewName();
				nickLabel.Text = name;
			};

			var continueButton = new Button ();
			continueButton.Text = "Продолжить";
			continueButton.Clicked += (sender, e) => {				
				var session = App.Container.Resolve<ISession>();
				session.CurrentUser.Title = nickLabel.Text;
				Navigation.PushAsync(new HubsPage());
			};

			Content = new StackLayout { 				
				VerticalOptions = LayoutOptions.End,
				Children = {
					nickLabel,
					refresh,
					continueButton,
				}
			};
		}

		string GenerateNewName()
		{
			var names = new string[]{
				"Eminem",
				"Hendrix",
				"Kanye West",
				"Rihanna",
				"Imany",
				"Тимати",
			};
			var digits = rand.Next (100);
			return names [rand.Next (names.Length)] + digits;
		}
	}
}


