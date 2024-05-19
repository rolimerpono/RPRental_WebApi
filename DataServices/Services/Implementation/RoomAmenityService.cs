using DataServices.Common.DTO;
using DataServices.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DataServices.Services.Implementation
{
    public class RoomAmenityService : IRoomAmenityService
    {
        private readonly IWorker _IWorker;
        private readonly APIResponse _APIResponse;
        public RoomAmenityService(IWorker iWorker)
        {

            _IWorker = iWorker;
            _APIResponse = new();

        }

        [HttpGet]
        public async Task<RoomAmenity> GetAsync(int RoomId)
        {
            RoomAmenity objRoomAmenity = new();

            try
            {

                objRoomAmenity = await _IWorker.tbl_RoomAmenity.GetAsync(fw => fw.RoomId == RoomId,IncludeProperties:"Room , Amenity");

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

        [HttpGet]
        public async Task<IEnumerable<RoomAmenity>> GetAllAsync(bool isTracking = false, int pageSize = 0, int pageNumber = 1)
        {
            IEnumerable<RoomAmenity> objRoomAmenties = new List<RoomAmenity>();

            try
            {
                objRoomAmenties = await _IWorker.tbl_RoomAmenity.GetAllAsync(null,IncludeProperties:"Room, Amenity", isTracking, pageSize,pageNumber);

                if (objRoomAmenties != null)
                {
                    return objRoomAmenties;
                }
            }
            catch (Exception ex)
            {
                return null!;
            }
            return null!;
        }



        [HttpPost]
        public async Task<APIResponse> CreateAsync(RoomAmenity objRoomAmenity)
        {

            try
            {
               
                await _IWorker.tbl_RoomAmenity.CreateAync(objRoomAmenity);
                await _IWorker.tbl_RoomAmenity.SaveAsync();
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
        public async Task<APIResponse> UpdateAsync(RoomAmenity objRoomAmenity)
        {
            try
            {
                var objData = await _IWorker.tbl_RoomAmenity.GetAsync(fw => fw.RoomId == objRoomAmenity.RoomId);

                if (objData == null || objRoomAmenity == null)
                {
                    _APIResponse.IsSuccess = false;
                    _APIResponse.Message = SD.CrudTransactionsMessage.RecordNotFound;
                    return _APIResponse;
                }

                await _IWorker.tbl_RoomAmenity.UpdateAsync(objRoomAmenity);
                await _IWorker.tbl_RoomAmenity.SaveAsync();

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
        public async Task<APIResponse> RemoveAsync(int RoomId)
        {
            RoomAmenity objRoomAmenity = new();

            try
            {
                objRoomAmenity = await _IWorker.tbl_RoomAmenity.GetAsync(fw => fw.RoomId == RoomId);

                if (objRoomAmenity != null)
                {
                    await _IWorker.tbl_RoomAmenity.RemoveAsync(objRoomAmenity);
                    await _IWorker.tbl_RoomAmenity.SaveAsync();

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
