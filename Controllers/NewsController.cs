using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsWeb.Models;
using NewsWeb.Services;
using NewsWeb.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsWeb.ViewModel;

namespace NewsWeb.Controllers
{
    public class NewsController : Controller
    {

        private INewsService newsService;

        public NewsController(INewsService newsService)
        {
            this.newsService = newsService;
        }

        public async Task<IActionResult> Index(int id = 0, int page = 1)
        {
            NewsResult newsResult = null;

            if (id == 0)
            {
                newsResult = await newsService.GetNewsAsync(page);
            }
            else
            {
                newsResult = await newsService.GetNewsAsync(id, page);
            }
            var resultCategories = await newsService.GetCategoriesAsync();
            var viewModel = new NewsIndexViewModel
            {
                News = newsResult.News,
                Categories = resultCategories,
                CurrentPage = page,
                TotalPages = (int)newsResult.TotalPages,
                CategoryId = id
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var list = await newsService.GetCategoriesAsync();
            var list2 = await newsService.GetTagsAsync();
            ViewBag.Categories = new SelectList(list, "Id", "Name");
            ViewBag.Tags = new MultiSelectList(list2, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(News news, IFormFile ImageFile, int[] Tags)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    var filepath = await FileUploadHelper.UploadAsync(ImageFile);
                    news.PosterUrl = filepath;
                }
                news.Date = DateTime.Now;
                await newsService.AddNewsAsync(news);
                await newsService.UpdateTagsInNews(news.Id, Tags);
                return RedirectToAction("Index");
            }
            else
            {
                var list = await newsService.GetCategoriesAsync();
                var list2 = await newsService.GetTagsAsync();
                ViewBag.Categories = new SelectList(list, "Id", "Name");
                ViewBag.Tags = new MultiSelectList(list2, "Id", "Name");
                return View(news);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            await newsService.DeleteNewsAsync(Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var news = await newsService.FindNewsByIdAsync(Id);
            var list = await newsService.GetCategoriesAsync();
            var list2 = await newsService.GetTagsAsync();
            ViewBag.Categories = new SelectList(list, "Id", "Name", news.CategoryId);
            ViewBag.Tags = new MultiSelectList(list2, "Id", "Name", news.NewsTags.Select(x => x.TagId));
            return View(news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(News news, IFormFile ImageFile, int[] Tags)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    var filepath = await FileUploadHelper.UploadAsync(ImageFile);
                    news.PosterUrl = filepath;
                }
                news.Date = DateTime.Now;
                await newsService.EditNewsAsync(news);
                await newsService.UpdateTagsInNews(news.Id, Tags);
                return RedirectToAction("Index");
            }
            else
            {
                var list = await newsService.GetCategoriesAsync();
                var list2 = await newsService.GetTagsAsync();
                ViewBag.Categories = new SelectList(list, "Id", "Name");
                ViewBag.Tags = new MultiSelectList(list2, "Id", "Name");
                return View(news);
            }
        }
    }
}
