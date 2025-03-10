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
        // חיפוש תמונות לפי מזהה אורח (guestId)
        public List<PhotosFromEvent> GetPhotosByGuestId(int guestId)
        {
            return context.PhotosFromEvents.Where(p => p.guestId == guestId).ToList();
        }

        // חיפוש תמונות לפי מזהה אירוע (eventId)
        public List<PhotosFromEvent> GetPhotosByEventId(int eventId)
        {
            return context.PhotosFromEvents.Where(p => p.eventId == eventId).ToList();
        }

        // חיפוש תמונות של אורח ספציפי מתוך אירוע מסוים
        public List<PhotosFromEvent> GetPhotosByGuestAndEvent(int guestId, int eventId)
        {
            return context.PhotosFromEvents
                           .Where(p => p.guestId == guestId && p.eventId == eventId)
                           .ToList();
        }

        // חיפוש תמונות עם טעינת נתוני האורח והאירוע
        public List<PhotosFromEvent> GetPhotosWithGuestAndEvent()
        {
            return context.PhotosFromEvents
                           .Include(p => p.guest)
                           .Include(p => p.event_)
                           .ToList();
        }

        // חיפוש תמונות עם ברכה מסוימת (תומך בחיפוש חלקי)
        public List<PhotosFromEvent> GetPhotosByBlessing(string blessingText)
        {
            return context.PhotosFromEvents
                           .Where(p => p.blessing.Contains(blessingText))
                           .ToList();
        }

        // חיפוש תמונות עם סינון לפי אירוע ומיון לפי שם האורח
        public List<PhotosFromEvent> GetPhotosByEventSortedByGuestName(int eventId)
        {
            return context.PhotosFromEvents
                           .Where(p => p.eventId == eventId)
                           .Include(p => p.guest)
                           .OrderBy(p => p.guest.name)
                           .ToList();
        }

        // חיפוש תמונות עם מיון יורד לפי תאריך הוספה (אם נוסיף תכונה של תאריך בעתיד)
        public List<PhotosFromEvent> GetPhotosSortedByNewest()
        {
            return context.PhotosFromEvents
                           .OrderByDescending(p => p.id) // אם נוסיף תאריך יצירה, נחליף ב- p.CreatedAt
                           .ToList();
        }

        // חיפוש עם דילוג והגבלה (Pagination)
        public List<PhotosFromEvent> GetPhotosPaged(int skip, int take)
        {
            return context.PhotosFromEvents.Skip(skip).Take(take).ToList();
        }

        // בדיקה אם קיימות תמונות מאירוע מסוים
        public bool AreTherePhotosFromEvent(int eventId)
        {
            return context.PhotosFromEvents.Any(p => p.eventId == eventId);


        }
    }
}

