﻿namespace Booking.Application.Abstractions.Persistence
{
    public interface IContextTransactionCreator
    {
        Task<IContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
    }
}
