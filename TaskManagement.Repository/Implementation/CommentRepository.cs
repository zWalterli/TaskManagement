using TaskManagement.Domain.DbModel;
using TaskManagement.Domain.Interface.Repository;
using TaskManagement.Repository.Context;

namespace TaskManagement.Repository.Implementation
{
    public class CommentRepository : BaseRepository<CommentDbModel>, ICommentRepository
    {
        public CommentRepository(APIContext context) : base(context)
        {

        }
    }
}