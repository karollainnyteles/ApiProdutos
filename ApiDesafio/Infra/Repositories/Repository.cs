using ApiDesafio.Business.Core.Data;
using ApiDesafio.Business.Core.Models;
using ApiDesafio.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDesafio.Infra.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly MeuDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(MeuDbContext meuDbContext)
        {
            Db = meuDbContext;
            DbSet = Db.Set<TEntity>();
        }

        public async Task<bool> Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            return await SaveChanges();
        }

        public async Task<bool> Atualizar(TEntity entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
            return await SaveChanges();
        }

        public async Task<TEntity> ObterPorId(int id)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(a=> a.Id == id);
        }

        public async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public async Task<bool> Remover(int id)
        {
            Db.Remove(new TEntity { Id = id });
            return await SaveChanges();
        }

        public async Task<bool> SaveChanges()
        {
            return await Db.SaveChangesAsync()>0;
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
