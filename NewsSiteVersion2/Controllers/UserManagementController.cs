using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsSiteVersion2.Entities;
using NewsSiteVersion2.Models;
using NewsSiteVersion2.Models.UserManagement;
using X.PagedList;

namespace NewsSiteVersion2.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UserManagementController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<CustomRole> _roleManager;
        private CustomIdentityDbContext _dbContext;
        public UserManagementController(UserManager<ApplicationUser> userManager,RoleManager<CustomRole> roleManager
                                                                       ,CustomIdentityDbContext dbContext)
        {
           _userManager = userManager;
           _roleManager = roleManager;
            _dbContext = dbContext;

        }
        [HttpPost,HttpGet]
        public  async Task<ActionResult> Index(string searchTerm,int pageNumber=1)
        {

            var indexUserModel = new IndexUserModel();
            indexUserModel.Users = new List<UserWithRole>();
            var result = _userManager.Users.AsQueryable();
            if (!String.IsNullOrEmpty(searchTerm))
                result = result.Where(x => x.Email.Contains(searchTerm));
            var applicationUsers =result.ToPagedList(pageNumber, 3);


            foreach (var user in applicationUsers.ToList())
            {
                var temp =await _userManager.GetRolesAsync(user);
                var userwithModel = new UserWithRole();
                userwithModel.User = user;

                userwithModel.RoleName = temp.FirstOrDefault();
                indexUserModel.Users.Add(userwithModel);
            }
            indexUserModel.SearchTerm = searchTerm;
            indexUserModel.tempUser = applicationUsers;

            
            
                
            
            return View(indexUserModel);
        }

        // GET: UserManagement/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var tempUser = _userManager.FindByIdAsync(id).Result;
            var tempRole = await _userManager.GetRolesAsync(tempUser);

            var editUserModel = new DetailsUserModel()
            {
                User = tempUser,
                RoleName = tempRole.FirstOrDefault()
            };

            return View(editUserModel);
        }

      

        // GET: UserManagement/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var tempUser = _userManager.FindByIdAsync(id).Result;
            var tempRole = await _userManager.GetRolesAsync(tempUser);

            var editUserModel = new EditUserModel()
            {
                User = tempUser,
                Roles = new SelectList(_roleManager.Roles, "Name", "Name", "Name")
            };

            return View(editUserModel);
        }

        // POST: UserManagement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EditUserModel userModel)
        {
            var User = _userManager.FindByIdAsync(userModel.User.Id).Result;
            var rolesBeforeUpdate = await _userManager.GetRolesAsync(User);
            
                await _userManager.RemoveFromRolesAsync(User, rolesBeforeUpdate);
                await _userManager.AddToRoleAsync(User,userModel.Role.Name);
                User.Name = userModel.User.Name;
                User.Surname = userModel.User.Surname;
                User.Email = userModel.User.Email;
                IdentityResult result = await _userManager.UpdateAsync(User);

                return RedirectToAction("Index","UserManagement");
            
            
                //return View();
            
        }

        // GET: UserManagement/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                var result = await _userManager.DeleteAsync(user);
              

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
            

        }

        // POST: UserManagement/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}