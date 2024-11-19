using System.Net.Mime;
using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class BlogPostCommentRepository:IBlogPostCommentRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogPostCommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
        {
            await _context.BlogPostsComment.AddAsync(blogPostComment);
            await _context.SaveChangesAsync();
            return blogPostComment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIdAsync(Guid blogPostId)
        {
            return await _context.BlogPostsComment
                .Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }
    }
}
