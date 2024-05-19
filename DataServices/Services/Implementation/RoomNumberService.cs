using DataServices.Common.DTO;
using DataServices.Services.Interface;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DataServices.Services.Implementation
{
   
    public class RoomNumberService : IRoomNumberService
    {

        private readonly IWorker _IWorker;
        private readonly APIResponse _APIResponse;

        public RoomNumberService(IWorker IWorker)
        {
            _IWorker = IWorker;
            _APIResponse = new();
        }

        public async Task<APIResponse> IsUniqueRoomNumber(int RoomNo)
        {

            try
            {
                var objRoom = await _IWorker.tbl_RoomNumber.GetAsync(fw => fw.RoomNo == RoomNo);

                if (objRoom != null)
                {
                    _APIResponse.IsSuccess = false;
                    _APIResponse.Message = SD.CrudTransactionsMessage.RecordExists;
                    return _APIResponse;
                }

                _APIResponse.IsSuccess = true;
                return _APIResponse;
            }
            catch (Exception ex)
            {
                _APIResponse.IsSuccess = false;
                _APIResponse.Message = ex.Message + " " + SD.SystemMessage.ContactAdmin;
                return _APIResponse;
            }
        }

        public async Task<RoomNumber> GetAsync(int RoomNo)
        {
            RoomNumber objRoomNo = new();

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

        public async Task<IEnumerable<RoomNumber>> GetAllAsync(Expression<Func<RoomNumber, bool>>? filter = null, string? IncludeProperties = null, bool isTracking = false, int pageSize = 0, int pageNumber = 1)
        {
            IEnumerable<RoomNumber> objRoomNo = new List<RoomNumber>();

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

        [HttpPost]
        public async Task<APIResponse> CreateAsync(RoomNumber objRoomNumber)
        {

            try
            {

                var response = await IsUniqueRoomNumber(objRoomNumber.RoomNo);

                if (response.IsSuccess == false)
                {
                    _APIResponse.IsSuccess = false;
                    _APIResponse.Message = SD.CrudTransactionsMessage.RecordExists;
                    return _APIResponse;
                }

                await _IWorker.tbl_RoomNumber.CreateAync(objRoomNumber);
                await _IWorker.tbl_Rooms.SaveAsync();
                _APIResponse.IsSuccess = true;
                _APIResponse.Message = SD.CrudTransactionsMessage.Save;

                return _APIResponse;
            }
            catch (Exception ex)
            {

                _APIResponse.IsSuccess = false;
                _APIResponse.Message = ex.Message + " " + SD.SystemMessage.ContactAdmin;
                return _APIResponse;
            }

        }


        [HttpPost]
        public async Task<APIResponse> UpdateAsync(RoomNumber objRoomNumber)
        {
            try
            {
                var objData = await _IWorker.tbl_RoomNumber.GetAsync(fw => fw.RoomNo == objRoomNumber.RoomNo);

                if (objData == null || objRoomNumber == null)
                {
                    _APIResponse.IsSuccess = false;
                    _APIResponse.Message = SD.CrudTransactionsMessage.RecordNotFound;
                    return _APIResponse;
                }

                await _IWorker.tbl_RoomNumber.UpdateAsync(objRoomNumber);
                await _IWorker.tbl_RoomNumber.SaveAsync();

                _APIResponse.IsSuccess = true;
                _APIResponse.Message = SD.CrudTransactionsMessage.Save;
                return _APIResponse;
            }
            catch (Exception ex)
            {
                _APIResponse.IsSuccess = false;
                _APIResponse.Message = ex.Message + " " + SD.SystemMessage.ContactAdmin;
                return _APIResponse;
            }

        }



        [HttpPost]
        public async Task<APIResponse> RemoveAsync(int RoomNo)
        {
            Room objRoom = new();

            try
            {
                objRoom = await _IWorker.tbl_Rooms.GetAsync(fw => fw.RoomId == RoomNo);

                if (objRoom != null)
                {
                    await _IWorker.tbl_Rooms.RemoveAsync(objRoom);
                    await _IWorker.tbl_Rooms.SaveAsync();

                    _APIResponse.IsSuccess = true;
                    _APIResponse.Message = SD.CrudTransactionsMessage.Delete;
                    return _APIResponse;
                }

                _APIResponse.IsSuccess = false;
                _APIResponse.Message = SD.CrudTransactionsMessage.RecordFound;
                return _APIResponse;
            }
            catch (Exception ex)
            {
                _APIResponse.IsSuccess = false;
                _APIResponse.Message = ex.Message + " " + SD.SystemMessage.ContactAdmin;
                return _APIResponse;
            }
        }



    }
    
}
