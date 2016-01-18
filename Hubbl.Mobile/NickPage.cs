using System;
using Hubbl.Core.Model;
using Hubbl.Core.Service;
using Autofac;

using Xamarin.Forms;

namespace Hubbl.Mobile
{
	public class NickPage : ContentPage
	{
		Random rand;
		public NickPage ()
		{
			rand = new Random ();
			var image = new Image () {
				Source = new FileImageSource() {File = "applogowideblack.png"},
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Aspect = Aspect.AspectFit,
				HeightRequest = 200,
			};

			var about = new Label{
				Text = "Музыкальный плеер с удаленным управлениях",
				VerticalOptions = LayoutOptions.Center,
				XAlign = TextAlignment.Center
			};
			var nameLabel = new Label {
				Text = "Ваше имя в сети",
				VerticalOptions = LayoutOptions.End,
				XAlign = TextAlignment.Center
			};
			var nickLabel = new Label{ 
				Text = GenerateNewName (),
				XAlign = TextAlignment.Center,
				FontSize = 24.0,
				VerticalOptions = LayoutOptions.End
			};
			var box = new BoxView (){ MinimumHeightRequest = 40 };
			var refresh = new Button () {HeightRequest = 40, WidthRequest = 40};
			refresh.Image = new FileImageSource () { File = "reloadblue.png" };
			refresh.Clicked += (sender, e) => {
				var name = GenerateNewName();
				nickLabel.Text = name;
			};
			refresh.VerticalOptions = LayoutOptions.End;
			//refresh.Text = "Выбрать другое";

			var continueButton = new Button ();
			continueButton.Text = "Пойдет";
			continueButton.Clicked += (sender, e) => {				
				var session = App.Container.Resolve<ISession>();
				session.CurrentUser.Title = nickLabel.Text;
				Navigation.PushAsync(new HubsPage());
			};
			continueButton.VerticalOptions = LayoutOptions.End;

			Content = new StackLayout { 
				Padding = 5,
				Spacing = 15,
				Children = {
					image,
					about,
					box,
					nameLabel,
					nickLabel,
					refresh,
					continueButton,
				}
			};
		}

		string GenerateNewName()
		{ var prefixes = new string[] {
				"",
				"TheOnly",
				"uber",
				"TRU",
				""
			};
			var names = new string[]{
				"Em1nem",
				"Hendrixx",
				"Rihanna:3",
				"Im@ny",
				"Тимати",
				"BobMarl3y",
				"2pac",
				"МаксКорж",
				"Лагутенко",
				"KanyeW3st",
				"Pitbull",
				"LanaD3lRey",
				"KatyP",
				"Bieber"
			};
			var digits = rand.Next (100);
			return prefixes[rand.Next(prefixes.Length)]+ names [rand.Next (names.Length)] + digits;
		}
	}
}


