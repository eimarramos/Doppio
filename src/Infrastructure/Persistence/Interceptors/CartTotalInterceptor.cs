﻿using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence.Interceptors
{
    public class CartTotalInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateTotal(eventData.Context);

            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateTotal(eventData.Context);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public void UpdateTotal(DbContext? context)
        {
            if (context == null) return;

            /**
            foreach (var entry in context.ChangeTracker.Entries<CartEntity>())
            {
                if (entry.State is EntityState.Added or EntityState.Modified || entry.HasChangedOwnedEntities())
                {
                    CartEntity cart = entry.Entity;
                    cart.Total = cart.CartDetails.Sum(item => item.Quantity * item.Coffee!.Price);
                }
            }
            **/
            foreach (var entry in context.ChangeTracker.Entries<CartDetailEntity>())
            {
                if (entry.State is EntityState.Added or EntityState.Modified || entry.State == EntityState.Deleted)
                {
                    var cart = entry.Entity.Cart;
                    if (cart != null)
                    {
                        cart.Total = cart.CartDetails.Sum(item => item.Quantity * item.Coffee!.Price);
                    }
                }
            }
        }
    }
    /**
     * 
     * Probably need entity config relationship to use it
     * 
    public static class Extensions
    {
        public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
            entry.References.Any(r =>
                r.TargetEntry != null &&
                r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added ||
             r.TargetEntry.State == EntityState.Modified ||
             r.TargetEntry.State == EntityState.Deleted));
    }
    **/
}
