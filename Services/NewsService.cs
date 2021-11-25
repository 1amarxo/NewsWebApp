using Microsoft.EntityFrameworkCore;
using NewsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsWeb.Services
{
    public class NewsService : INewsService
    {
        private readonly NewsDbContext context;

        public NewsService(NewsDbContext context)
        {
            this.context = context;
        }

        public async Task AddCategoryAsync(Category category)
        {
            context.Categories.Add(category);
            await context.SaveChangesAsync();
        }

        public async Task AddNewsAsync(News news)
        {
            context.News.Add(news);
            await context.SaveChangesAsync();
        }

        public async Task DeleteNewsAsync(int Id)
        {
            var news = context.News.Where(x => x.Id == Id).FirstOrDefault();
            context.News.Remove(news);
            await context.SaveChangesAsync();
        }

        public async Task EditNewsAsync(News news)
        {
            context.News.Update(news);
            await context.SaveChangesAsync();
        }

        public async Task<News> FindNewsByIdAsync(int id)
        {
            return await context.News.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<NewsResult> GetNewsAsync(int page = 1)
        {
            int skip = 5 * (page - 1);
            int take = 5;
            var news = await context.News.ToListAsync();
            double totalPages = Math.Ceiling(news.Count() / 5.0);
            return new NewsResult { News = news.Skip(skip).Take(take), TotalPages = totalPages };
        }

        public async Task<NewsResult> GetNewsAsync(int CategoryId, int page = 1)
        {
            int skip = 5 * (page - 1);
            int take = 5;
            var news = await context.News.Where(x => x.CategoryId == CategoryId).ToListAsync();
            double totalPages = Math.Ceiling(news.Count() / 5.0);
            return new NewsResult { News = news.Skip(skip).Take(take), TotalPages = totalPages };
        }

        public async Task<IEnumerable<Tag>> GetTagsAsync()
        {
            return await context.Tags.ToListAsync();
        }

        public async Task UpdateTagsInNews(int newsId, int[] tags)
        {
            var list = await context.NewsTags.Where(x => x.NewsId == newsId).ToListAsync();
            context.NewsTags.RemoveRange(list);

            //foreach (int item in tags)
            //{
            //    context.NewsTags.Add(new NewsTag { NewsId = newsId, TagId = item });
            //}

            context.NewsTags.AddRange(tags.Select(x => new NewsTag { NewsId = newsId, TagId = x }));

            await context.SaveChangesAsync();
        }
    }
}
