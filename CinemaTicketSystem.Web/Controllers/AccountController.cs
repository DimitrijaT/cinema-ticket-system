﻿using CinemaTicketSystem.Web.Models.Domain;
using CinemaTicketSystem.Web.Models.Identity;
using CinemaTicketSystem.Web.Models.Dto;
//using EShop.Web.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EShop.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<CinemaTicketSystemUser> userManager;
        private readonly SignInManager<CinemaTicketSystemUser> signInManager;
        public AccountController(UserManager<CinemaTicketSystemUser> userManager,
            SignInManager<CinemaTicketSystemUser> signInManager)
        {

            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Register()
        {
            UserRegistrationDto model = new UserRegistrationDto();
            return View(model);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(UserRegistrationDto request)
        {
            if (ModelState.IsValid)
            {
                var userCheck = await userManager.FindByEmailAsync(request.Email);
                if (userCheck == null)
                {
                    var user = new CinemaTicketSystemUser
                    {
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Address = request.Address,
                        UserName = request.Email,
                        NormalizedUserName = request.Email,
                        Email = request.Email,
                        PhoneNumber = request.PhoneNumber,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        //UserCart = new Models.Domain.ShoppingCart()
                        UserCart = new CinemaTicketSystem.Web.Models.Domain.ShoppingCart()
                    };
                    var result = await userManager.CreateAsync(user, request.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        if (result.Errors.Count() > 0)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("message", error.Description);
                            }
                        }
                        return View(request);
                    }
                }
                else
                {
                    ModelState.AddModelError("message", "Email already exists.");
                    return View(request);
                }
            }
            return View(request);

        }


        [HttpGet, AllowAnonymous]
        public IActionResult Login()
        {
            UserLoginDto model = new UserLoginDto();
            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null && !user.EmailConfirmed)
                {
                    ModelState.AddModelError("message", "Email not confirmed yet");
                    return View(model);

                }
                if (await userManager.CheckPasswordAsync(user, model.Password) == false)
                {
                    ModelState.AddModelError("message", "Invalid credentials");
                    return View(model);

                }

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, true, true);

                if (result.Succeeded)
                {
                    await userManager.AddClaimAsync(user, new Claim("UserRole", "Admin"));
                    return RedirectToAction("Index", "Tickets");
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid login attempt");
                    return View(model);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("login", "account");
        }
    }
}