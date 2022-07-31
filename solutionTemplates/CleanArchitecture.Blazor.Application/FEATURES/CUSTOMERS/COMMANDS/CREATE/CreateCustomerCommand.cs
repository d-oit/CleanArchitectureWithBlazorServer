﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.$safeprojectname$.Features.Customers.DTOs;
using CleanArchitecture.Blazor.$safeprojectname$.Features.Customers.Caching;

namespace CleanArchitecture.Blazor.$safeprojectname$.Features.Customers.Commands.Create;

    public class CreateCustomerCommand: CustomerDto,IRequest<Result<int>>, ICacheInvalidator
    {
      public string CacheKey => CustomerCacheKey.GetAllCacheKey;
      public CancellationTokenSource? SharedExpiryTokenSource => CustomerCacheKey.SharedExpiryTokenSource();
    }
    
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreateCustomerCommand> _localizer;
        public CreateCustomerCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<CreateCustomerCommand> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
           //TODO:Implementing CreateCustomerCommandHandler method 
           var item = _mapper.Map<Customer>(request);
           // add create domain events if this entity implement the IHasDomainEvent interface
	       // item.AddDomainEvent(new CreatedEvent<Customer>(item));
           _context.Customers.Add(item);
           await _context.SaveChangesAsync(cancellationToken);
           return  Result<int>.Success(item.Id);
        }
    }

