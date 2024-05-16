using DataServices.Services.Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Services.Implementation
{
    public class RoomAmenityService : IRoomAmenityService
    {
        private readonly IWorker _IWorker;
        public RoomAmenityService(IWorker iWorker)
        {

            _IWorker = iWorker;

        }
        public async Task<bool> CreateAsync(RoomAmenity objRoomAmenity)
        {
            try
            {
                var is_exists = await _IWorker.tbl_Rooms.GetAsync(fw => fw.RoomId == objRoomAmenity.RoomId);

                if (is_exists != null || objRoomAmenity == null)
                {
                    return false;
                }

                await _IWorker.tbl_RoomAmenity.CreateAync(objRoomAmenity);
                await _IWorker.tbl_RoomAmenity.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<RoomAmenity>> GetAllAsync(Expression<Func<RoomAmenity, bool>>? filter = null, string? IncludeProperties = null, bool isTracking = false, int pageSize = 0, int pageNumber = 1)
        {
            IEnumerable<RoomAmenity> objRoomAmenities = new List<RoomAmenity>();

            try
            {
                objRoomAmenities = await _IWorker.tbl_RoomAmenity.GetAllAsync(filter, IncludeProperties, isTracking, pageSize, pageNumber);

                if (objRoomAmenities != null)
                {
                    return objRoomAmenities;
                }
            }
            catch (Exception ex)
            {
                return null!;
            }
            return null!;
        }

        public async Task<RoomAmenity> GetAsync(int Id)
        {
            RoomAmenity objRoomAmenity  = new();

            try
            {
                objRoomAmenity = await _IWorker.tbl_RoomAmenity.GetAsync(fw => fw.RoomId == Id);

                if (objRoomAmenity != null)
                {
                    return objRoomAmenity;
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
            RoomAmenity objRoomAmenity = new();

            try
            {
                objRoomAmenity = await _IWorker.tbl_RoomAmenity.GetAsync(fw => fw.RoomId == Id);
                if (objRoomAmenity != null)
                {
                    await _IWorker.tbl_RoomAmenity.RemoveAsync(objRoomAmenity);
                    await _IWorker.tbl_RoomAmenity.SaveAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        public async Task<bool> UpdateAsync(RoomAmenity objRoomAmenity)
        {
            try
            {
                var is_exists = await _IWorker.tbl_RoomAmenity.GetAsync(fw => fw.RoomId == objRoomAmenity.RoomId);

                if (is_exists == null || objRoomAmenity == null)
                {
                    return false;
                }

                await _IWorker.tbl_RoomAmenity.UpdateAsync(objRoomAmenity);
                await _IWorker.tbl_RoomAmenity.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
