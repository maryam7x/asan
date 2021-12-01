using Asan.Contexts;
using Asan.Entities;
using Asan.ResourceParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Asan.Services
{
    public class AddressRepository : RepositoryBase<Address> , IAddressRepository
    {
        private readonly AsanContext _context;

        public AddressRepository(AsanContext context) : base(context)
        {
            _context = context;
        }

        public bool EntityExists(int entityId)
        {
            return _context.Addresses.Any(s => s.Id == entityId);
        }

        public Address GetSubscriberAddress(int subscriberId, int addressId)
        {
            return _context.Addresses.FirstOrDefault(a => a.SubscriberId == subscriberId && a.Id == addressId);
        }

        public Address GetById(int entityId)
        {
            return _context.Addresses.FirstOrDefault(s => s.Id == entityId);
        }

        public ListResponse<Address> GetSubscriberAddresses(AddressListRequest request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var addresses = _context.Addresses.Where(a => a.SubscriberId == request.SubescriberId) as IQueryable<Address>;

            if (!string.IsNullOrWhiteSpace(request.ContainsText))
            {
                //due to limited properties the expression have been hard coded but in real world should use 'Expression Tree' to generate this expression dynamically
                addresses = addresses.Where(a => a.Country.Contains(request.ContainsText) || a.City.Contains(request.ContainsText) || a.AddressDetail.Contains(request.ContainsText));
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
                                addresses = addresses.Where(a => prop.GetValue(a).Equals(filter.Value));
                                break;
                            }
                        case QueryOperator.NotEqual:
                            {
                                addresses = addresses.Where(a => !prop.GetValue(a).Equals(filter.Value));
                                break;
                            }
                        case QueryOperator.Contains:
                            {
                                addresses = addresses.Where(a => ((string)prop.GetValue(a)).Contains((string)filter.Value));
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

                    addresses = sort.Order == SortOrder.Asc ? addresses.OrderBy(a => prop.GetValue(a)) : addresses.OrderByDescending(a => prop.GetValue(a));
                }
            }

            return ListResponse<Address>.Generate(addresses.Count(), addresses.Skip(request.PageSize * (request.PageNumber - 1)).Take(request.PageSize).ToList(), request.PageNumber, request.PageSize);
        }
    }
}
