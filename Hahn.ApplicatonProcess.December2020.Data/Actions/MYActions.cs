
using Hahn.ApplicatonProcess.December2020.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data.Actions
{
    public class MYActions 
    {
        private readonly Database db = new Database();
        public async Task<bool> Create(ModelDB newuser)
        {
            var tf = false;
            if (newuser.Id != 0)
                newuser.Id = 0;
            db.DBCON.Add(newuser);
            var numberOfApplicantsCreated = await db.SaveChangesAsync();
            if (numberOfApplicantsCreated == 1)
                tf = true;
            return tf;
        }

        public async Task<bool> Update(ModelDB newuser)
        {
            var tf = false;

            var existingApplicant = Get(newuser.Id);

            if (existingApplicant != null)
            {
                existingApplicant.Name = newuser.Name;
                existingApplicant.FamilyName = newuser.FamilyName;
                existingApplicant.Address = newuser.Address;
                existingApplicant.CountryOfOrigin = newuser.CountryOfOrigin;
                existingApplicant.EmailAddress = newuser.EmailAddress;
                existingApplicant.Age = newuser.Age;
                existingApplicant.Hired = newuser.Hired;
                db.DBCON.Attach(existingApplicant);
                var numberOfApplicantsCreated = await db.SaveChangesAsync();
                if (numberOfApplicantsCreated == 1)
                    tf = true;
            }
            return tf;
        }

        public IOrderedQueryable<ModelDB> GetAll()
        {
            var result = db.DBCON.OrderByDescending(x => x.Id);
            return result;
        }

        public ModelDB Get(int UserID)
        {
            var result = db.DBCON.Where(x => x.Id == UserID).FirstOrDefault();
            return result;
        }

        public async Task<bool> Delete(int euser)
        {
            var success = false;
            var existingApplicant = Get(euser);
            if (existingApplicant != null)
            {
                db.DBCON.Remove(existingApplicant);
                var numberOfApplicantsDeleted = await db.SaveChangesAsync();
                if (numberOfApplicantsDeleted == 1)
                    success = true;
            }
            return success;
        }

    }
}
