﻿using FleetTrackerSystem.Application.DTOS.User;
using FleetTrackerSystem.Infrastructure.Repositories.Repos;
using FleetTrackerSystem.Infrastructure.UnitOfWork;
using MediatR;

namespace FleetTrackerSystem.Application.CQRS.UserMangement.Queries
{
    public class GetUserByComanyId: IRequest<UserDto>
    {
        public int CompanyId { get; set; }
        public GetUserByComanyId(int companyId)
        {
            CompanyId = companyId;
        }
    }

    public class GetUserByComanyIdHandler : IRequestHandler<GetUserByComanyId, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetUserByComanyIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<UserDto> Handle(GetUserByComanyId request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.User.GetUserByCompanyId(request.CompanyId);
            return user.Map<UserDto>();
        }
    }
}
