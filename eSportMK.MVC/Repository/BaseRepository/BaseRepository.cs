using eSportMK.Repository.ResponseObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eSportMK.Repository.BaseRepository
{
    public class BaseRepository<TContext> : IRepository where TContext : DbContext
    {
        protected readonly TContext context;

        public BaseRepository(TContext context)
        {
            this.context = context;
        }

        protected virtual IQueryable<TEntity> GetQueryable<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class
        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<TEntity> query = context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        public virtual Response<IEnumerable<TEntity>> GetAll<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class
        {
            var response = new Response<IEnumerable<TEntity>> { Data = new List<TEntity>() };
            try
            {
                var entities = GetQueryable<TEntity>(null, orderBy, includeProperties, skip, take).ToList();
                if (entities == null)
                {
                    response.ErrorMessage = String.Format("No entries of type {0} found", typeof(TEntity).Name);
                    response.Success = false;
                    return response;
                }
                response.Success = true;
                response.Data = entities;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                return response;
            }
        }

        public virtual async Task<Response<IEnumerable<TEntity>>> GetAllAsync<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class
        {
            var response = new Response<IEnumerable<TEntity>> { Data = new List<TEntity>() };
            try
            {
                var entities = await GetQueryable<TEntity>(null, orderBy, includeProperties, skip, take).ToListAsync();
                if (entities == null)
                {
                    response.ErrorMessage = String.Format("No entries of type {0} found", typeof(TEntity).Name);
                    response.Success = false;
                    return response;
                }
                response.Success = true;
                response.Data = entities;
                return response;
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                return response;
            }
        }

        public virtual Response<IEnumerable<TEntity>> Get<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class
        {
            var response = new Response<IEnumerable<TEntity>> { Data = new List<TEntity>() };
            try
            {
                var entities = GetQueryable<TEntity>(null, orderBy, includeProperties, skip, take).ToList();
                if (entities == null)
                {
                    response.ErrorMessage = String.Format("No entries of type {0} found", typeof(TEntity).Name);
                    response.Success = false;
                    return response;
                }
                response.Success = true;
                response.Data = entities;
                return response;
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                return response;
            }
        }

        public virtual async Task<Response<IEnumerable<TEntity>>> GetAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class
        {
            var response = new Response<IEnumerable<TEntity>> { Data = new List<TEntity>() };
            try
            {
                var entities = await GetQueryable<TEntity>(null, orderBy, includeProperties, skip, take).ToListAsync();
                if (entities == null)
                {
                    response.ErrorMessage = String.Format("No entries of type {0} found", typeof(TEntity).Name);
                    response.Success = false;
                    return response;
                }
                response.Success = true;
                response.Data = entities;
                return response;
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                return response;
            }
        }

        public virtual Response<TEntity> GetOne<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "")
            where TEntity : class
        {
            var response = new Response<TEntity> { Data = null };
            try
            {
                var entity = GetQueryable<TEntity>(filter, null, includeProperties, null, null).SingleOrDefault();
                if (entity == null)
                {
                    response.ErrorMessage = String.Format("No such entry of type {0} found", typeof(TEntity).Name);
                    response.Success = false;
                    return response;
                }
                response.Success = true;
                response.Data = entity;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                return response;
            }
        }

        public virtual async Task<Response<TEntity>> GetOneAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null)
            where TEntity : class
        {
            var response = new Response<TEntity> { Data = null };
            try
            {
                var entity = await GetQueryable<TEntity>(filter, null, includeProperties, null, null).SingleOrDefaultAsync();
                if (entity == null)
                {
                    response.ErrorMessage = String.Format("No such entry of type {0} found", typeof(TEntity).Name);
                    response.Success = false;
                    return response;
                }
                response.Success = true;
                response.Data = entity;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                return response;
            }
        }

        public virtual Response<TEntity> GetFirst<TEntity>(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "")
           where TEntity : class
        {
            var response = new Response<TEntity> { Data = null };
            try
            {
                var entity = GetQueryable<TEntity>(filter, null, includeProperties, null, null).SingleOrDefault();
                if (entity == null)
                {
                    response.ErrorMessage = String.Format("No such entry of type {0} found", typeof(TEntity).Name);
                    response.Success = false;
                    return response;
                }
                response.Success = true;
                response.Data = entity;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                return response;
            }
        }

        public virtual async Task<Response<TEntity>> GetFirstAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null)
            where TEntity : class
        {
            var response = new Response<TEntity> { Data = null };
            try
            {
                var entity = await GetQueryable<TEntity>(filter, null, includeProperties, null, null).SingleOrDefaultAsync();
                if (entity == null)
                {
                    response.ErrorMessage = String.Format("No such entry of type {0} found", typeof(TEntity).Name);
                    response.Success = false;
                    return response;
                }
                response.Success = true;
                response.Data = entity;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                return response;
            }
        }

        public virtual Response<TEntity> GetById<TEntity>(object id)
            where TEntity : class
        {
            var response = new Response<TEntity> { Data = null };
            try
            {
                var entity = context.Set<TEntity>().Find(id);
                if (entity == null)
                {
                    response.ErrorMessage = String.Format("No such entry of type {0} found. Key used {1}", typeof(TEntity).Name, id);
                    response.Success = false;
                    return response;
                }
                response.Success = true;
                response.Data = entity;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                return response;
            }   
        }

        public virtual async Task<Response<TEntity>> GetByIdAsync<TEntity>(object id)
            where TEntity : class
        {
            var response = new Response<TEntity> { Data = null };
            try
            {
                var entity = await context.Set<TEntity>().FindAsync(id);
                if (entity == null)
                {
                    response.ErrorMessage = String.Format("No such entry of type {0} found. Key used {1}", typeof(TEntity).Name, id);
                    response.Success = false;
                    return response;
                }
                response.Success = true;
                response.Data = entity;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                return response;
            }
        }

        public virtual Response<bool> GetExists<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class
        {
            var response = new Response<bool> { Data = false };
            try
            {
                var entity = GetQueryable<TEntity>(filter).Any();
                if (!entity)
                {
                    response.ErrorMessage = String.Format("No such entry of type {0} found", typeof(TEntity).Name);
                    response.Success = false;
                    return response;
                }
                response.Success = true;
                response.Data = entity;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                return response;
            }
        }

        public virtual async Task<Response<bool>> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class
        {
            var response = new Response<bool> { Data = false };
            try
            {
                var entity = await GetQueryable<TEntity>(filter).AnyAsync();
                if (!entity)
                {
                    response.ErrorMessage = String.Format("No such entry of type {0} found", typeof(TEntity).Name);
                    response.Success = false;
                    return response;
                }
                response.Success = true;
                response.Data = entity;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                return response;
            }
        }

        public virtual Response<bool> Create<TEntity>(TEntity entity, string createdBy = null)
       where TEntity : class
        {
            var response = new Response<bool> { Data = false };
            try
            {
                context.Set<TEntity>().Add(entity);
                response.Success = true;
                response.Data = true;
                Save();
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                return response;
            }

        }

        public virtual Response<bool> Update<TEntity>(TEntity entity, string modifiedBy = null)
            where TEntity : class
        { 
            var response = new Response<bool> { Data = false };
            try
            {
                context.Set<TEntity>().Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
                Save();
                response.Success = true;
                response.Data = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                return response;
            }
        }

        public virtual Response<bool> Delete<TEntity>(object id)
            where TEntity : class
        {
            var response = new Response<bool> { Data = false };
            try
            {
                var entity = context.Set<TEntity>().Find(id);
                if(entity == null)
                {
                    response.Success = false;
                    response.ErrorMessage = String.Format("No such entry of type {0} found", typeof(TEntity).Name);
                    return response;
                }
                var deletion = Delete(entity);
                if (!deletion.Success)
                {
                    response.Success = false;
                    response.ErrorMessage = String.Format("Deletion of type {0} succeeded", typeof(TEntity).Name);
                    return response;
                }
                Save();
                response.Success = true;
                response.Data = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                return response;
            }
            
        }

        public virtual Response<bool> Delete<TEntity>(TEntity entity)
            where TEntity : class
        {
            var response = new Response<bool> { Data = false };
            try
            {
                var dbSet = context.Set<TEntity>();
                if (context.Entry(entity).State == EntityState.Detached)
                {
                    dbSet.Attach(entity);
                }
                dbSet.Remove(entity);
                Save();
                response.Data = true;
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                return response;
            }
        }

        public virtual void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                ThrowEnhancedValidationException(e);
            }
        }

        public virtual Task SaveAsync()
        {
            try
            {
                return context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                ThrowEnhancedValidationException(e);
            }

            return Task.FromResult(0);
        }

        protected virtual void ThrowEnhancedValidationException(Exception e)
        {

        }
    }
}
