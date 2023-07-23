using AdvertisementApp.DataAccess.Configuraitons;
using AdvertisementApp.DataAccess.Contexts;
using AdvertisementApp.DataAccess.Interfaces;
using AdvertisementApp.Entities;
using AdvertisimentApp.Common.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AdvetisementContext _context;

        public Repository(AdvetisementContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GelAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter) // burada bütün kayıtlarda filtre uygulayıp getirmemize yarıyor
        {
            return await _context.Set<T>().AsNoTracking().Where(filter).ToListAsync();
        }

        public async Task<List<T>> GetAllAsync<TKey>(Expression<Func<T, TKey>> selector, OrderByType orderByType = OrderByType.DESC) // burada da veriyi belirli bir sırada getirmeye yaramaktadır.orderby descending gibi farklı olarak TKey kullanamktayız
        {
            return orderByType == OrderByType.ASC ? await _context.Set<T>().AsNoTracking().OrderBy(selector).ToListAsync() : await _context.Set<T>().AsNoTracking().OrderByDescending(selector).ToListAsync();
        }

        public async Task<List<T>> GetAllAsync<TKey>(Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> selector, OrderByType orderByType = OrderByType.DESC) // hem filtre atayarak hemde veri dizelre bütün verileri getiren metot
        {
            return orderByType == OrderByType.ASC ? await _context.Set<T>().AsNoTracking().Where(filter).OrderBy(selector).ToListAsync() : await _context.Set<T>().AsNoTracking().Where(filter).OrderByDescending(selector).ToListAsync();
        }


        public async Task<T> FindAsync(object id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByFilterAsync(Expression<Func<T,bool>> filter , bool asNoTracking = false)
        {

            return !asNoTracking? await _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(filter) : await _context.Set<T>().SingleOrDefaultAsync(filter);
        }

        public IQueryable<T> GetQuery()
        {

            return _context.Set<T>().AsQueryable();
        }

        public void Remove (T entity)
        {
            _context.Set<T>().Remove(entity);

        }

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Update( T entity, T unchanged)
        {
            _context.Entry(unchanged).CurrentValues.SetValues(unchanged);
        } 
        

    }
}
