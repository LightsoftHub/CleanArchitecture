﻿using CleanArch.Infrastructure.Identity;
using CleanArch.Shared.Notifications;
using Light.EntityFrameworkCore.Extensions;
using Light.Specification;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.WebApi.SignalR.UseCases;

public class GetNotifications : NotificationLookup, IQuery<PagedResult<NotificationDto>>;

internal class GetNotificationsHandler(AppIdentityDbContext context) :
    IQueryHandler<GetNotifications, PagedResult<NotificationDto>>
{
    public async Task<PagedResult<NotificationDto>> Handle(GetNotifications request,
        CancellationToken cancellationToken)
    {
        return await context.Notifications
            .WhereIf(!string.IsNullOrEmpty(request.ToUser), x => x.ToUserId == request.ToUser)
            .AsNoTracking()
            .OrderByDescending(o => o.CreatedOn)
            .ProjectToType<NotificationDto>()
            .ToPagedResultAsync(request.Page, request.PageSize, cancellationToken);
    }
}