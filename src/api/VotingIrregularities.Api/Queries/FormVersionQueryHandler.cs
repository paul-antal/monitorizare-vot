﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VotingIrregularities.Api.Models;
using VotingIrregularities.Api.Services;
using VotingIrregularities.Domain.Models;

namespace VotingIrregularities.Api.Queries
{
    public class FormVersionQueryHandler : AsyncRequestHandler<FormVersionQuery, List<FormVersion>>
    {

        private readonly VotingContext _context;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public FormVersionQueryHandler(VotingContext context, IMapper mapper, ICacheService cacheService)
        {
            _context = context;
            _mapper = mapper;
            _cacheService = cacheService;
        }
      
        protected override async Task<List<FormVersion>> HandleCore(FormVersionQuery request)
        {
            var result = await _context.FormVersions
                .AsNoTracking()
                .ToListAsync();

            return result;
        }
    }
}
