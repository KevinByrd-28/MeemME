using MeemME.Data;
using MeemME.Models.SaveModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeemME.Services
{
    public class SaveService
    {
        private readonly Guid _userId;

        public SaveService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateSave(SaveCreate model)
        {

            var entity =
                new Save()
                {
                    OwnerId = _userId,
                    Title = model.Title,
                    URL = model.URL
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Saves.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<SaveListItem> GetSaves()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Saves
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new SaveListItem
                                {
                                    SaveID = e.SaveID,
                                    Title = e.Title,
                                    URL = e.URL
                                }
                        );


                return query.ToArray();
            }
        }

        public SaveDetail GetSaveById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Saves
                        .Single(e => e.SaveID == id && e.OwnerId == _userId);
                return
                    new SaveDetail
                    {
                        SaveID = entity.SaveID,
                        Title = entity.Title,
                        URL = entity.URL
                    };
            }
        }

        public bool UpdateSave(SaveEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Saves
                        .Single(e => e.SaveID == model.SaveID && e.OwnerId == _userId);

                entity.Title = model.Title;
                entity.URL = model.URL;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteSave(int saveId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Saves
                        .Single(e => e.SaveID == saveId && e.OwnerId == _userId);

                ctx.Saves.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
