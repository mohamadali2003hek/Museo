using Museo.ViewModels;
using System.Security.Claims;

namespace Museo.Services
{
    public interface IItemsService
    {

        Task<List<ItemViewModel>?> getItem(ClaimsPrincipal user);

        CreatItemVeiwModel GetCreatItem();

        Task<bool> ceratItem(ClaimsPrincipal user, CreatItemVeiwModel model);

        Task<EditItemVeiwModel> GetEditItem(ClaimsPrincipal user, int id);

        Task<bool> EditItem(ClaimsPrincipal user, EditItemVeiwModel model, int id);

        Task<bool> DeletItem(ClaimsPrincipal user, int id);

        Task<bool> MoveItem(ClaimsPrincipal user, MoveItemViewModel model);

        Task<ItemDetailsViewModel?> GetItemDetails(int id);

        Task<bool> ToggleLike(int id, ClaimsPrincipal user);

    }
}
