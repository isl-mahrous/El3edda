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

namespace El3edda.Controllers
{

    public class ReviewsController : Controller
    {
        private readonly IReviewService _service;

        private readonly IMobileService _serviceMob;


        public ReviewsController(IReviewService service, IMobileService serviceMob)
        {
            _service = service;
            _serviceMob = serviceMob;
        }

        // GET: Reviews
        [Authorize]

        public IActionResult Index()
        {
            var AllReviews = _service.GetAllAsync(r => r.Mobile).Result;
            
            return View(AllReviews);

        }

        public async Task<IActionResult> Create(int id)
        {
            ViewBag.Mobile = await _serviceMob.GetByIdAsync(id);

            return View(new Review());
        }

        // POST: Reviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Rate", "CustomerName", "Feedback", "MobileId")]Review review)
        {
            review.Date = DateTime.Now;

            if (ModelState.IsValid)
            {
                await _service.AddAsync(review);
                return RedirectToAction("Details", "Mobiles", new { area = "", id = review.MobileId});
            }

            ViewBag.Mobile = await _serviceMob.GetByIdAsync(review.MobileId);

            return View(review);
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var ReviewDetails = await _service.GetByIdAsync(id, r => r.Mobile);

            ViewBag.Mobile = await _serviceMob.GetByIdAsync(ReviewDetails.MobileId);


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

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var ReviewDetails = await _service.GetByIdAsync(id);
            if (ReviewDetails == null)
                return NotFound();

            return View(ReviewDetails);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ReviewDetails = await _service.GetByIdAsync(id);
            if (ReviewDetails == null)
                return NotFound();

            await _service.DeleteAsync(id);
            return RedirectToAction("Index", "Reviews", new { area = "" });
        }
    }
}
