using System;
using System.Collections.Generic;
using System.Linq;
using Data.Domain;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DbContext _context;
        private DbSet<T> _entities;
        public EFRepository(DbContext context)
        {
            _context = context;
        }


        public virtual IQueryable<T> Table
        {
            get
            {
                return Entities;
            }
        }

        public virtual IQueryable<T> TableNoTracking
        {
            get
            {
                return Entities.AsNoTracking();
            }
        }

        void IRepository<T>.Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                   throw new ArgumentNullException(nameof(entity));                    
                }
                Entities.Remove(entity);

                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {  
                //logger
            }
        }

        void IRepository<T>.Delete(IEnumerable<T> entities)
        {
              try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                foreach (var entity in entities)
                    Entities.Remove(entity);

                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                //logger
            }
        }

        T IRepository<T>.GetById(object id)
        {
            return Entities.Find(id);
        }

        void IRepository<T>.Insert(T entity)
        {
            try
            {
               if (entity == null)
               {
                   throw new ArgumentNullException(nameof(entity));
               }

               Entities.Add(entity);
               _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                //logger
            }
        }

        void IRepository<T>.Insert(IEnumerable<T> entities)
        {
           try
           {
               if (entities == null)
               {
                   throw new ArgumentNullException(nameof(entities));
               }

               foreach(var entity in entities)
               {
                   Entities.Add(entity);
                   _context.SaveChanges();
               }
           }
           catch (DbUpdateException ex)
           {
               //logger
           }
        }

        void IRepository<T>.Update(T entity)
        {
            try
            {
                if (entity == null)
            {
                   throw new ArgumentNullException(nameof(entity));                
            }

            _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {        
                 //logger
            }
            
        }

        void IRepository<T>.Update(IEnumerable<T> entities)
        {
             try
            {
                if (entities == null)
            {
                   throw new ArgumentNullException(nameof(entities));                
            }

            _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {        
                 //logger
            }
        }

        protected virtual DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = _context.Set<T>();
                }
                return _entities;
            }
        }
    }
}