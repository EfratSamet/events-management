using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class PhotosFromEventRepository : IRepository<PhotosFromEvent>
    {
        private readonly IContext context;
        public PhotosFromEventRepository(IContext context)
        {
            this.context = context;
        }

        public PhotosFromEvent Add(PhotosFromEvent item)
        {
            context.PhotosFromEvents.Add(item);
            context.save();
            return item;
        }

        public void Delete(int id)
        {
            context.PhotosFromEvents.Remove(Get(id));
            context.save();
        }

        public PhotosFromEvent Get(int id)
        {
            return context.PhotosFromEvents.FirstOrDefault(x => x.id == id);
        }

        public List<PhotosFromEvent> GetAll()
        {
            return context.PhotosFromEvents.ToList();
        }

        public PhotosFromEvent Update(int id, PhotosFromEvent item)
        {
            PhotosFromEvent p = Get(id);
            p.eventId = item.eventId;
            p.guestId = item.guestId;
            p.blessing = item.blessing;
            p.imageUrl = item.imageUrl;
            context.save();
            return p;
        }
       
    }
}

