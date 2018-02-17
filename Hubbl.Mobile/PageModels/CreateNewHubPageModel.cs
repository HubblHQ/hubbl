using FreshMvvm;
using Hubbl.Mobile.PageModels.Vkontakte;
using PropertyChanged;
using Xamarin.Forms;

namespace Hubbl.Mobile.PageModels
{
    internal class CreateNewHubPageModel : HubblViewModel
    {
        public Command ShowVkSongs =>
            new Command(() 
                => CoreMethods.PushPageModel<VkSongsSourcePageModel>());
    }
}
