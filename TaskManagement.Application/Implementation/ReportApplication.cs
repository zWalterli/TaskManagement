using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.DbModel;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interface.Application;
using TaskManagement.Domain.Interface.Repository;
using TaskManagement.Domain.ViewModel.Report;
using TaskManagement.Domain.ViewModel.Result;
using TaskManagement.Repository.Implementation;

namespace TaskManagement.Application.Implementation
{
    public class ReportApplication : IReportApplication
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        public ReportApplication(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }
        
        public async Task<Result<IList<ReportGetViewModel>>> GetAmountTasksFinishedPerUserInAMounth()
        {
            var finished = await _taskRepository.GetFinishedByUser();

            var response = finished
                .GroupBy(x => x.UserId)
                .Select(item => new Report { UserId = item.Key, AmountDoneTasks = item.Count(), Tasks = _mapper.Map<List<Domain.Entities.Task>>(item) })
                .ToList();

            var result = _mapper.Map<List<ReportGetViewModel>>(response);
            return Result<IList<ReportGetViewModel>>.Ok(result);
        }
    }
}
