using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class GroupRepository:IRepository<Group>
    {
        private readonly IContext context;
        public GroupRepository(IContext context)
        {
            this.context = context;
        }

        public Group Add(Group item)
        {
            context.Groups.Add(item);
            context.save();
            return item;
        }

        public void Delete(string id)
        {
            context.Groups.Remove(Get(id));
            context.save();
        }

        public Group Get(string id)
        {
            return context.Groups.FirstOrDefault(x => x.id == id);
        }

        public List<Group> GetAll()
        {
            return context.Groups.ToList();
        }

        public Group Update(string id, Group item)
        {
            var existingGroup = context.Groups.FirstOrDefault(x => x.id == id);

            existingGroup.name = item.name;
            existingGroup.organizerId = item.organizerId;
            existingGroup.guestId = item.guestId;

            if (item.organizer != null)
            {
                existingGroup.organizer = item.organizer;
            }

            if (item.guest != null)
            {
                existingGroup.guest = item.guest;
            }

            context.save();

            return existingGroup;
        }
        //חיפוש קבוצות לפי מזהה מארגן(organizerId)
        public List<Group> GetGroupsByOrganizerId(string organizerId)
        {
            return context.Groups
                .Where(g => g.organizerId == organizerId)  // מסנן לפי מזהה המארגן
                .ToList();
        }
        //חיפוש קבוצות לפי שם הקבוצה(name)
        public List<Group> GetGroupsByName(string name)
        {
            return context.Groups
                .Where(g => g.name.Contains(name))  // מסנן לפי שם הקבוצה
                .ToList();
        }
        //חיפוש קבוצות לפי מזהה אורח(guestId)
        public List<Group> GetGroupsByGuestId(string guestId)
        {
            return context.Groups
                .Where(g => g.guestId == guestId)  // מסנן לפי מזהה האורח
                .ToList();
        }
        // חיפוש קבוצות לפי מספר מוגבל של קבוצות (לדוגמה 5 קבוצות ראשונות)
        public List<Group> GetTopGroups(int count)
        {
            return context.Groups
                .Take(count)  // לוקח את כמות הקבוצות המבוקשת
                .ToList();
        }
    }
}
