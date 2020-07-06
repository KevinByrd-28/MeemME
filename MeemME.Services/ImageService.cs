using MeemME.Data;
using MeemME.Models.ImageModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MeemME.Services
{
    public class ImageService
    {
        private readonly Guid _userId;

        public ImageService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateImage(ImageCreate model)
        {

            var entity =
                new Image()
                {
                    OwnerId = _userId,
                    Title = model.Title,
                    ImagePath = model.ImagePath,
                    ImageFile = model.ImageFile,
                    TopText = model.TopText,
                    BottomText = model.BottomText,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Images.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ImageListItem> GetImages()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Images
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new ImageListItem
                                {
                                    ImageID = e.ImageID,
                                    Title = e.Title,
                                    ImagePath = e.ImagePath,
                                    //ImageFile = e.ImageFile,
                                    TopText = e.TopText,
                                    BottomText = e.BottomText,
                                }
                        ) ;

             
                    return query.ToArray();
            }
        }

        public ImageDetail GetImageById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Images
                        .Single(e => e.ImageID == id && e.OwnerId == _userId);
                return
                    new ImageDetail
                    {
                        ImageID = entity.ImageID,
                        Title = entity.Title,
                        ImagePath = entity.ImagePath,
                        ImageFile = entity.ImageFile,
                        TopText = entity.TopText,
                        BottomText = entity.BottomText,
                    };
            }
        }

        public bool UpdateImage(ImageEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Images
                        .Single(e => e.ImageID == model.ImageID && e.OwnerId == _userId);

                entity.Title = model.Title;
                entity.ImagePath = model.ImagePath;
                entity.ImageFile = model.ImageFile;
                entity.TopText = model.TopText;
                entity.BottomText = model.BottomText;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteImage(int imageId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Images
                        .Single(e => e.ImageID == imageId && e.OwnerId == _userId);

                ctx.Images.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
       

    }
}
