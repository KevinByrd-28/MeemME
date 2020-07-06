using MeemME.Data;
using MeemME.Models.FolderModels;
using MeemME.Models.ImageModels;
using MeemME.Models.ImagesInFolderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeemME.Services
{
    public class FolderService
    {
        private readonly Guid _userId;

        public FolderService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateFolder(FolderCreate model)
        {


            try
            {
                bool allWentWell = false;
                var entity =
                    new Folder()
                    {
                        Name = model.Name,
                    };

                using (var ctx = new ApplicationDbContext())
                {
                    ctx.Folders.Add(entity);
                    var success = ctx.SaveChanges() == 1;

                    foreach (var imageInFolder in model.ImagesInFolder)
                    {
                        if (imageInFolder.HasValue)
                        {
                            var image =
                            new ImagesInFolderCreate()
                            {
                                FolderID = entity.FolderID,
                                ImageID = Convert.ToInt32(imageInFolder)
                            };
                            var succeeded = CreateImagesInFolder(image);
                            //break the code or decide what you'll do if succeeded == false;
                            // if succeeded is false, return allWentWell
                        }


                    }
                    allWentWell = true;
                }
                return allWentWell;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool CreateImagesInFolder(ImagesInFolderCreate imageModel)
        {
            var entity =
                new ImagesInFolder()
                {
                    FolderID = imageModel.FolderID,
                    ImageID = imageModel.ImageID,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.ImagesInFolder.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public bool UpdateImageInFolder(ImagesInFolderEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .ImagesInFolder
                        .Single(e => e.FolderID == model.FolderID);

                entity.ImagesInFolderID = model.ImagesInFolderID;
                entity.FolderID = model.FolderID;
                entity.ImageID = model.ImageID;

                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<FolderListItem> GetFolders()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Folders
                        .Select(
                            e =>
                                new FolderListItem
                                {
                                    FolderID = e.FolderID,
                                    Name = e.Name,
                                }
                        );

                return query.ToArray();
            }
        }
        public FolderDetail GetFolderById(int id)
        {


            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Folders
                        .Single(e => e.FolderID == id);

                List<ImageListItem> imageListItems = new List<ImageListItem>();
                foreach (var image in entity.ImagesInFolder)
                {
                    var meme = new ImageListItem()
                    {
                        ImageID = image.ImageID,
                        Title = image.Image.Title,
                        ImagePath = image.Image.ImagePath,
                        ImageFile = image.Image.ImageFile,
                        TopText = image.Image.TopText,
                        BottomText = image.Image.BottomText
                    };
                    imageListItems.Add(meme);
                }

                var image1 = new FolderDetail
                {
                    FolderID = entity.FolderID,
                    Name = entity.Name,
                    ImagesInFolder = imageListItems,
                };
                return image1;
            }
        }
        public bool UpdateFolder(FolderEdit model) //Purge folder and replace with new images OR just add and not remove
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity =
                      ctx
                          .Folders
                          .Single(e => e.FolderID == model.FolderID);
                    if (entity == null)
                        return false;


                    var iidEntity =
                        ctx
                            .ImagesInFolder
                            .Select(e => e.FolderID == model.FolderID);

                    //foreach (var oldImages in ctx.ImagesInFolder) //Deletes Existing in Folder
                    //{
                    //    var happy = ctx.ImagesInFolder.Remove(oldImages);
                    //}
                    //ctx.SaveChanges();
                    //var succeed = ctx.ImagesInFolder.Count() == 0;

                    entity.Name = model.Name;

                    foreach (var newImages in model.ImagesInFolder) //may break if not purging system
                    {
                        var image =
                            new ImagesInFolderCreate()
                            {
                                FolderID = entity.FolderID,
                                ImageID = newImages.Value
                            };
                        var succeeded = CreateImagesInFolder(image);
                    }
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return true;
        }

        public bool DeleteFolder(int folderId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Folders
                        .Single(e => e.FolderID == folderId);

                ctx.Folders.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }


    }
}
