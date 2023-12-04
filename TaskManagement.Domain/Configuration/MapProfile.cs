using TaskManagement.Domain.DbModel;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.ViewModel;
using AutoMapper;
using TaskManagement.Domain.ViewModel.Report;

namespace TaskManagement.Domain.Configuration
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            #region Task
            CreateMap<TaskDbModel, Entities.Task>().ReverseMap();
            CreateMap<Entities.Task, TaskGetViewModel>().ReverseMap();
            CreateMap<Entities.Task, TaskCreateViewModel>().ReverseMap();
            CreateMap<Entities.Task, TaskUpdateViewModel>().ReverseMap();
            #endregion

            #region Project
            CreateMap<ProjectDbModel, Project>().ReverseMap();
            CreateMap<Project, ProjectCreateViewModel>().ReverseMap();
            CreateMap<Project, ProjectGetViewModel>().ReverseMap();
            #endregion

            #region Comment
            CreateMap<CommentDbModel, Comment>().ReverseMap();
            CreateMap<Comment, CommentCreateViewModel>().ReverseMap();
            CreateMap<Comment, CommentUpdateViewModel>().ReverseMap();
            CreateMap<Comment, CommentGetViewModel>().ReverseMap();
            #endregion

            #region Report
            CreateMap<Report, ReportGetViewModel>().ReverseMap();
            #endregion
        }
    }
}