using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class BlogPostLikeRepository :IBlogPostLikeRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogPostLikeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
            return await _context.BlogPostsLike
                .CountAsync(x => x.BlogPostId == blogPostId);
        }

        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            await _context.BlogPostsLike.AddAsync(blogPostLike);
            await _context.SaveChangesAsync();
            return blogPostLike;
        }

        public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
        {
            return await _context.BlogPostsLike.Where(x => x.BlogPostId == blogPostId)
                .ToListAsync();
        }
    }
}
