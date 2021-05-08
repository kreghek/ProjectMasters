using System.Threading.Tasks;

namespace ProjectMasters.Web.Hubs
{
    public interface IGame
    {
        Task AssignPersonAsync(object obj);
    }
}