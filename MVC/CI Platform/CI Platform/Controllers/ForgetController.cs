using CI_Platform.DataModels;
using CI_Platform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

namespace CI_Platform.Controllers
{
    public class ForgetController : Controller
    {
        private readonly CIdbcontext _cidbcontext;
        public ForgetController(CIdbcontext cidbcontext)
        {
            _cidbcontext = cidbcontext;
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Forget(ForgetModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _cidbcontext.Users.FirstOrDefault(u => u.Email == model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("Email", "Email is not valid");
                    return View(model);
                }

                // Generate a password reset token for the user
                var token = Guid.NewGuid().ToString();

                // Store the token in the password resets table with the user's email
                var passwordReset = new PasswordReset
                {
                    Email = model.Email,
                    Token = token
                };
                _cidbcontext.PasswordResets.Add(passwordReset);
                _cidbcontext.SaveChanges();

                // Send an email with the password reset link to the user's email address
                var resetLink = Url.Action("Reset", "Home", new { email = model.Email, token }, Request.Scheme);
                // Send email to user with reset password link
                // ...
                var fromAddress = new MailAddress("gajeravirajpareshbhai@gmail.com", "Sender Name");
                var toAddress = new MailAddress(model.Email);
                var subject = "Password reset request";
                var body = $"Hi,<br /><br />Please click on the following link to reset your password:<br /><br /><a href='{resetLink}'>{resetLink}</a>";
                var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                var smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("gajeravirajpareshbhai@gmail.com", "drbwjzfrmubtveud"),
                    EnableSsl = true
                };
                smtpClient.Send(message);

                return RedirectToAction("Landing", "Home");
            }

            return View();
        }











        // GET: ForgetController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ForgetController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ForgetController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ForgetController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ForgetController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ForgetController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ForgetController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ForgetController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
