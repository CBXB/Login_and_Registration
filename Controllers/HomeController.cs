using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Login_and_Registration.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Login_and_Registration.Controllers
{
    public class HomeController : Controller
    {
        private UserContext _context { get; set; }
        public HomeController(UserContext context)
        {
            _context = context;
        }


        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(User data)
        {
            if(ModelState.IsValid)
            {
                User EmailQuery = _context.users.SingleOrDefault( a=>a.Email == @data.Email );
                if(EmailQuery != null)
                {
                    ViewBag.EmailError = "Email has already been registered.";
                    return View("Index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                data.Password = Hasher.HashPassword(data, data.Password);
                //Save your user object to the database
                User NewReview = new User
                {
                    FirstName = @data.FirstName,
                    LastName = @data.LastName,
                    Email = @data.Email,
                    Password = data.Password,
                };
                _context.Add(NewReview);
                // OR _context.Users.Add(NewPerson);
                _context.SaveChanges();
                //List<User> AllTheReviews =   _context.reviews.OrderBy(p => p.ReviewDate).ToList();
                return View("Contact");
            }
            return View("Index");

        }

        [HttpPost("~/processlogin")]
        public IActionResult ProcessLogin(User data)
        {
            // Attempt to retrieve a user from your database based on the Email submitted
            User theUser = _context.users.SingleOrDefault( a=>a.Email == @data.LoginEmail ); 
            if(theUser != null && @data.LoginPassword != null)
            {
                var Hasher = new PasswordHasher<User>();
                // Pass the user object, the hashed password, and the PasswordToCheck
                if(0 != Hasher.VerifyHashedPassword(theUser, theUser.Password, @data.LoginPassword))
                {
                    //Handle success
                    return View("Contact");
                }
            }
            //Handle failure
            return View("Index");
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }


// ================================================================================================================
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
