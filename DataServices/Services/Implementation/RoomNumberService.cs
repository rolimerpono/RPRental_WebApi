using DataServices.Services.Interface;
using Microsoft.AspNetCore.Components.Forms;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Services.Implementation
{
   
    public class RoomNumberService : IRoomNumberService
    {

        private readonly IWorker _IWorker;

        public RoomNumberService(IWorker IWorker)
        {
            _IWorker = IWorker;
        }
        public async Task<bool> CreateAsync(RoomNumber objRoomNo)
        {
            try
            {

                var is_exists = await _IWorker.tbl_RoomNumber.GetAsync(fw => fw.RoomNo == objRoomNo.RoomNo);

                if (is_exists != null || objRoomNo == null)
                {
                    return false;
                }

                await _IWorker.tbl_RoomNumber.CreateAync(objRoomNo);
                await _IWorker.tbl_RoomNumber.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<RoomNumber>> GetAllAsync(Expression<Func<RoomNumber, bool>>? filter = null, string? IncludeProperties = null, bool isTracking = false, int pageSize = 0, int pageNumber = 1)
        {
            IEnumerable<RoomNumber> objRoomNo;

            try
            {
                objRoomNo = await _IWorker.tbl_RoomNumber.GetAllAsync(filter, IncludeProperties, isTracking, pageSize, pageNumber);

                if (objRoomNo != null)
                {
                    return objRoomNo;
                }
            }
            catch (Exception ex)
            {
                return null!;
            }
            return null!;
          
        }

        public async Task<RoomNumber> GetAsync(int RoomNo)
        {
            RoomNumber objRoomNo;
            try
            {
                objRoomNo = await _IWorker.tbl_RoomNumber.GetAsync(fw => fw.RoomNo == RoomNo);

                if (objRoomNo != null)
                {
                    return objRoomNo;
                }

                return null!;

            }
            catch (Exception ex)
            {
                return null!;
            }
        }

        public async Task<bool> RemoveAsync(int RoomNo)
        {
            try
            {
                var objRoomNo = await _IWorker.tbl_RoomNumber.GetAsync(fw => fw.RoomNo == RoomNo);
                if (objRoomNo != null)
                {
                    await _IWorker.tbl_RoomNumber.RemoveAsync(objRoomNo);
                    await _IWorker.tbl_RoomNumber.SaveAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(RoomNumber objRoomNo)
        {
            try
            {
                var is_exists = await _IWorker.tbl_RoomNumber.GetAsync(fw => fw.RoomNo == objRoomNo.RoomNo);

                if (is_exists == null || objRoomNo == null)
                {
                    return false;
                }

                await _IWorker.tbl_RoomNumber.UpdateAsync(objRoomNo);
                await _IWorker.tbl_RoomNumber.SaveAsync();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
    
}
