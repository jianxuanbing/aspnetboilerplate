﻿using System.Linq;
using Abp.Application.Editions;
using BeiDreamAbp.Domain.Editions;
using BeiDreamAbp.Infrastructure.Ef.EntityFramework;

namespace BeiDreamAbp.Infrastructure.Ef.Migrations.SeedData
{
    public class DefaultEditionCreator
    {
        private readonly BeiDreamAbpDbContext _context;

        public DefaultEditionCreator(BeiDreamAbpDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateEditions();
        }

        private void CreateEditions()
        {
            var defaultEdition = _context.Editions.FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName);
            if (defaultEdition == null)
            {
                defaultEdition = new Edition { Name = EditionManager.DefaultEditionName, DisplayName = EditionManager.DefaultEditionName };
                _context.Editions.Add(defaultEdition);
                _context.SaveChanges();

                //TODO: Add desired features to the standard edition, if wanted!
            }
        }
    }
}