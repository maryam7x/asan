using Asan.Contexts;
using Asan.Entities;
using Asan.ResourceParams;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Asan.Services
{
    public class SubscriberRepository : RepositoryBase<Subscriber> , ISubscriberRepository
    {
        private readonly AsanContext _context;

        public SubscriberRepository(AsanContext context): base(context)
        {
            _context = context;
        }   
             
        public bool EntityExists(int entityId)
        {
            return _context.Subscribers.Any(s => s.Id == entityId);
        }
                  
        public Subscriber GetById(int entityId, bool includeAddresses = false)
        {
            if (includeAddresses)
            {
                return _context.Subscribers.Where(s => s.Id == entityId).Include(s => s.AddressList).FirstOrDefault();
            }
            else
            {
                return _context.Subscribers.FirstOrDefault(s => s.Id == entityId);
            }
        }
             
        public ListResponse<Subscriber> GetAll(ListRequest request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var subscribers = _context.Subscribers as IQueryable<Subscriber>;

            if (!string.IsNullOrWhiteSpace(request.ContainsText))
            {
                //due to limited properties the expression have been hard coded but in real world should use 'Expression Tree' to generate this expression dynamically
                subscribers = subscribers.Where(s => s.FirstName.Contains(request.ContainsText) || s.LastName.Contains(request.ContainsText));
            }

            if (request.Filters != null && request.Filters.Count() > 0)
            {
                foreach (var filter in request.Filters)
                {
                    Type t = typeof(Subscriber);
                    PropertyInfo prop = t.GetProperty(filter.Field);
                    switch (filter.Operator)
                    {
                        case QueryOperator.Equal:
                            {
                                subscribers = subscribers.Where(a => prop.GetValue(a).Equals(filter.Value));
                                break;
                            }
                        case QueryOperator.NotEqual:
                            {
                                subscribers = subscribers.Where(a => !prop.GetValue(a).Equals(filter.Value));
                                break;
                            }
                        case QueryOperator.Contains:
                            {
                                subscribers = subscribers.Where(a => ((string)prop.GetValue(a)).Contains((string)filter.Value));
                                break;
                            }
                        default:
                            break;
                    }
                }
            }

            if (request.Sorts != null && request.Sorts.Count() > 0)
            {
                foreach (var sort in request.Sorts)
                {
                    Type t = typeof(Subscriber);
                    PropertyInfo prop = t.GetProperty(sort.Field);

                    subscribers = sort.Order == SortOrder.Asc ? subscribers.OrderBy(a => prop.GetValue(a)) : subscribers.OrderByDescending(a => prop.GetValue(a));
                }
            }

            return ListResponse<Subscriber>.Generate(subscribers.Count(), subscribers.Skip(request.PageSize * (request.PageNumber - 1)).Take(request.PageSize).ToList(), request.PageNumber, request.PageSize);
        }      
    }
}
