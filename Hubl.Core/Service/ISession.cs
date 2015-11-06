using Hubl.Core.Model;

namespace Hubl.Core.Service
{
    public interface ISeesion
    {
        User CurrentUser { get; }
    }
}
