using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    internal interface IPhotosFromEventRepository : IRepository<PhotosFromEvent>
    {
        List<PhotosFromEvent> GetPhotosByGuestId(string guestId);
        List<PhotosFromEvent> GetPhotosByEventId(string eventId);
        bool AreTherePhotosFromEvent(string eventId);
        List<PhotosFromEvent> GetPhotosPaged(int skip, int take);
        List<PhotosFromEvent> GetPhotosSortedByNewest();
        List<PhotosFromEvent> GetPhotosByEventSortedByGuestName(string eventId);
        List<PhotosFromEvent> GetPhotosByBlessing(string blessingText);
        List<PhotosFromEvent> GetPhotosWithGuestAndEvent();
        List<PhotosFromEvent> GetPhotosByGuestAndEvent(string guestId, string eventId);


    }
}
