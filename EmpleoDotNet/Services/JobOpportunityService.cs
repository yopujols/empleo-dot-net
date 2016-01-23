﻿using System.Collections.Generic;
using System.Linq;
using EmpleoDotNet.Core.Domain;
using EmpleoDotNet.Core.Dto;
using EmpleoDotNet.Data;
using EmpleoDotNet.Repository;
using EmpleoDotNet.ViewModel;
using PagedList;

namespace EmpleoDotNet.Services
{
    public class JobOpportunityService
    {
        private readonly JobOpportunityRepository _jobOpportunityRepository;

        public JobOpportunityService()
        {
            _jobOpportunityRepository = new JobOpportunityRepository(new EmpleadoContext());
        }

        public void CreateNewJobOpportunity(JobOpportunity jobOpportunity)
        {
            _jobOpportunityRepository.Add(jobOpportunity);
            _jobOpportunityRepository.SaveChanges();
        }

        public List<RelatedJobDto> GetCompanyRelatedJobs(int id, string name, string email, string url)
        {
            var result = _jobOpportunityRepository.GetAllJobOpportunities()
                .Where(
                    x =>
                        x.Id != id &&
                        (x.CompanyName == name && x.CompanyEmail == email &&
                         x.CompanyUrl == url))
                .Select(jobOpportunity => new RelatedJobDto
                {
                    Title = jobOpportunity.Title,
                    Url = "/JobOpportunity/Detail/" + jobOpportunity.Id
                }).ToList();

            return result;
        }

        public IPagedList<JobOpportunity> GetAllJobOpportunitiesPagedByFilters(JobOpportunityPagingParameter parameter)
        {
          return _jobOpportunityRepository.GetAllJobOpportunitiesPagedByFilters(parameter);
        }

        public JobOpportunity GetJobOpportunityById(int? id)
        {
            return _jobOpportunityRepository.GetJobOpportunityById(id);
        }
    }
}