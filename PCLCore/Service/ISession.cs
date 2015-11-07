using Hubl.Core.Model;

namespace Hubl.Core.Service
{
    public interface ISession
    {
        User CurrentUser { get; }
    }
}
