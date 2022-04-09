using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using El3edda.Data;
using El3edda.Models;
using Microsoft.AspNetCore.Authorization;
using El3edda.Data.Services.MobileService;
using El3edda.Data.Services.ReviewService;
using Microsoft.AspNetCore.Identity;
using El3edda.Data.Enums;
using El3edda.Data.Static;

namespace El3edda.Controllers
{

    public class ReviewsController : Controller
    {
        private readonly IReviewService _service;

        private readonly IMobileService _serviceMob;

        private readonly UserManager<ApplicationUser> _userManager;
        
        public ReviewsController(IReviewService service, IMobileService serviceMob, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _serviceMob = serviceMob;
            _userManager = userManager;
        }

        // GET: Reviews
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Index()
        {
            var AllReviews = await _service.GetAllAsync(r => r.Mobile, r => r.User);
            
            return View(AllReviews);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, MobileRate Rate, string Feedback)
        {
            //ViewBag.Mobile = await _serviceMob.GetByIdAsync(id);

            var usr = await _userManager.GetUserAsync(HttpContext.User);

            if (usr == null)
            {
                return RedirectToAction("Login", "Account");
            }

            Review review = new();

            review.Rate = Rate;
            review.Feedback = Feedback;
            review.Date = DateTime.Now;
            review.UserId = usr.Id;
            review.MobileId = id;

            await _service.AddAsync(review);
            return RedirectToAction("Details", "Mobiles", new { area = "", id });
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var ReviewDetails = await _service.GetByIdAsync(id, r => r.Mobile, r => r.User);

            //ViewBag.Mobile = await _serviceMob.GetByIdAsync(ReviewDetails.MobileId);


            if (ReviewDetails == null)
                return NotFound();

            return View(ReviewDetails);
        }

        // POST: Reviews/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Review review)
        {
            review.Date = DateTime.Now;

            if (!ModelState.IsValid)
            {
                ViewBag.Mobile = await _serviceMob.GetByIdAsync(review.MobileId);
                return View(review);
            }

            await _service.UpdateAsync(id, review);
            return RedirectToAction("Details", "Mobiles", new { area = "", id = review.MobileId });
        }

        //GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var ReviewDetails = await _service.GetByIdAsync(id);

            var mobid = ReviewDetails.MobileId;

            if (ReviewDetails == null)
                return NotFound();

            await _service.DeleteAsync(id);

            return RedirectToAction("Details", "Mobiles", new { area = "", id = mobid });
        }

    }
}
