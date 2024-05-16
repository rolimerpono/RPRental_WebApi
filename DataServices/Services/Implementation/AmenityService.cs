using DataServices.Services.Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Services.Implementation
{
    public class AmenityService : IAmenityService
    {
        private readonly IWorker _IWorker;
        public AmenityService(IWorker iWorker)
        {

            _IWorker = iWorker;

        }
        public async Task<bool> CreateAsync(Amenity objAmenity)
        {
            try
            {
                var is_exists = await _IWorker.tbl_Amenity.GetAsync(fw => fw.AmenityName.ToLower() == objAmenity.AmenityName.ToLower());

                if (is_exists != null || objAmenity == null)
                {
                    return false;
                }

                await _IWorker.tbl_Amenity.CreateAync(objAmenity);
                await _IWorker.tbl_Amenity.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<IEnumerable<Amenity>> GetAllAsync(Expression<Func<Amenity, bool>>? filter = null, string? IncludeProperties = null, bool isTracking = false, int pageSize = 0, int pageNumber = 1)
        {
            IEnumerable<Amenity> objAmenities;

            try
            {
                objAmenities = await _IWorker.tbl_Amenity.GetAllAsync(filter, IncludeProperties, isTracking, pageSize, pageNumber);

                if (objAmenities != null)
                {
                    return objAmenities;
                }
            }
            catch (Exception ex)
            {
                return null!;
            }
            return null!;
        }

        public async Task<Amenity> GetAsync(int Id)
        {
            Amenity objAmenity;
            try
            {

                objAmenity = await _IWorker.tbl_Amenity.GetAsync(fw => fw.AmenityId == Id);

                if (objAmenity != null)
                {
                    return objAmenity;
                }

                return null!;

            }
            catch (Exception ex)
            {
                return null!;
            }
        }

        public async Task<bool> RemoveAsync(int Id)
        {
            try
            {
                var objAmenity = await _IWorker.tbl_Amenity.GetAsync(fw => fw.AmenityId == Id);
                if (objAmenity != null)
                {
                    await _IWorker.tbl_Amenity.RemoveAsync(objAmenity);
                    await _IWorker.tbl_Amenity.SaveAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        public async Task<bool> UpdateAsync(Amenity objAmenity)
        {
            try
            {
                var is_exists = await _IWorker.tbl_Amenity.GetAsync(fw => fw.AmenityId == objAmenity.AmenityId);

                if (is_exists == null || objAmenity == null)
                {
                    return false;
                }

                await _IWorker.tbl_Amenity.UpdateAsync(objAmenity);
                await _IWorker.tbl_Amenity.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
