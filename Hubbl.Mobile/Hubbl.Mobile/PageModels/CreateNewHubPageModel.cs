using FreshMvvm;
using Hubbl.Mobile.PageModels.Vkontakte;
using PropertyChanged;
using Xamarin.Forms;

namespace Hubbl.Mobile.PageModels
{
    [ImplementPropertyChanged]
    internal class CreateNewHubPageModel : FreshBasePageModel
    {
        public Command ShowVkSongs => 
            new Command(() 
                => CoreMethods.PushPageModel<VkSongsSourcePageModel>());
    }
}
