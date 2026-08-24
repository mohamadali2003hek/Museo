using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Museo.Models;
using Museo.Museo_Database_Context;
using Museo.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Museo.Services
{
    public class ItemsService : IItemsService
    {

        private readonly UserManager<Users> _userManager;
        private readonly MuseoDbContext _context;

        public ItemsService (UserManager<Users> UserManager , MuseoDbContext museoDb)
        {
            _userManager = UserManager;
            _context = museoDb;
        }
        public async Task<List<ItemViewModel>?> getItem(ClaimsPrincipal  user)
        {
            var u = await _userManager.GetUserAsync(user);
            if (u == null) return null;

            var items = await  _context.Items.Where(i => i.Profile.UserId == u.Id).Select(i => new ItemViewModel
            {
                Id = i.Id,
                Name = i.Title,
                Preview_Image =i.Preview_Image,
                Source =i.Source,
                Note =i.Note,
                CreatedAt = i.Created_at,
                Pos_x = i.Pos_x,
                Pos_y = i.Pos_y
            }).ToListAsync();

            return items;
        }

        public CreatItemVeiwModel GetCreatItem()
        {

            return new CreatItemVeiwModel();
        }

        public async Task<bool> ceratItem(ClaimsPrincipal user, CreatItemVeiwModel model )
        {
            var u = await _userManager.GetUserAsync(user);
            if (u == null) return false;
            Profiles? profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == u.Id);

            if (profile == null)
                return false;

            var item = new Items
            {
                Url = model.Url,
                Title = model.Title,
                Preview_Image = model.Preview_Image,
                Source = model.Source,
                Note = model.Note,
                Pos_x = 0,
                Pos_y = 0,
                ProfileId = profile.Id
            };

            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<EditItemVeiwModel> GetEditItem(ClaimsPrincipal user, int id)
        {
            var u = await _userManager.GetUserAsync(user);
            if (u == null) return null;
            Profiles? profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == u.Id);

            if (profile == null)
                return null;

            var item = await _context.Items
            .FirstOrDefaultAsync(i => i.Id == id && i.ProfileId == profile.Id);
            if (item == null)
                return null;
        
            return new EditItemVeiwModel {
                Preview_Image =item.Preview_Image,
                Note=item.Note
            };

        }

        public async Task<bool> EditItem(ClaimsPrincipal user, EditItemVeiwModel model, int id)
        {
            var u = await _userManager.GetUserAsync(user);
            if (u == null) return false;
            Profiles? profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == u.Id);

            if (profile == null)
                return false;

            var item = await _context.Items
            .FirstOrDefaultAsync(i => i.Id == id && i.ProfileId == profile.Id);
            if (item == null)
                return false;
            if (model.Preview_Image != null) { item.Preview_Image = model.Preview_Image; }
            if (model.Note != null) { item.Note = model.Note; }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletItem(ClaimsPrincipal user, int id)
        {
            var u = await _userManager.GetUserAsync(user);
            if (u == null) return false;
            Profiles? profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == u.Id);

            if (profile == null)
                return false;

            var item = await _context.Items
            .FirstOrDefaultAsync(i => i.Id == id && i.ProfileId == profile.Id);
            if (item == null)
                return false;

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return true;
        }



        public async Task<bool> MoveItem(
    ClaimsPrincipal user,
    MoveItemViewModel model)
        {
            var u = await _userManager.GetUserAsync(user);

            if (u == null)
                return false;

            var item = await _context.Items
                .Include(i => i.Profile)
                .FirstOrDefaultAsync(i =>
                    i.Id == model.Id &&
                    i.Profile.UserId == u.Id);

            if (item == null)
                return false;

            item.Pos_x = model.PosX;
            item.Pos_y = model.PosY;

            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<ItemDetailsViewModel?> GetItemDetails(int id)
        {
            Items? Items = await  _context.Items.Include(i => i.Comments)
            .Include(i => i.Likes).FirstOrDefaultAsync(i =>i.Id == id);

            if (Items == null) return null; 
            return new  ItemDetailsViewModel{
                Id = Items.Id,
                Title = Items.Title,
                Url = Items.Url ,
                Note = Items.Note ,
                Source = Items.Source,
                Created_at = Items.Created_at,
                Comments = Items.Comments,
                Likes = Items.Likes

            }
            ;
        }


        public async Task<bool> ToggleLike(int id , ClaimsPrincipal user)
        {
            var u = await _userManager.GetUserAsync(user);
            if (u == null) return false;
            Items? item = await _context.Items.FirstOrDefaultAsync(i => i.Id == id );
            if (item == null) return false;

            Likes? like = await _context.Likes
            .FirstOrDefaultAsync(l => l.UserId == u.Id && l.ItemId == item.Id);

            if (like== null) {
                var l = new Likes
                {
                    UserId = u.Id,
                    ItemId = item.Id
                };
                await _context.Likes.AddAsync(l);
            }
            else {
                _context.Likes.Remove(like);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddComment( ClaimsPrincipal user,int itemId,CommentViewModel model)
        {
            var u = await _userManager.GetUserAsync(user);
            if (u == null) return false;
            var comment = new Comments
            {
                UserId = u.Id,
                ItemId = itemId,
                Content = model.Content
            };

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
