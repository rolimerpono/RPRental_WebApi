using DataServices.Common.DTO;
using DataServices.Services.Interface;
using Microsoft.AspNetCore.Hosting;
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
    public class RoomService : IRoomService
    {

        private readonly IWorker _IWorker;
        private readonly IWebHostEnvironment _Webhost;
        private readonly APIResponse _APIResponse;

        public RoomService(IWorker IWorker, IWebHostEnvironment webhost)
        {
            _IWorker = IWorker;
            _Webhost = webhost;
            _APIResponse  = new();
        }

        [HttpPost]
        public async Task<APIResponse> CreateAsync(Room objRoom)
        {
           
            try
            {           

                var response = await IsUniqueRoom(objRoom.RoomName);

                if (response.IsSuccess == false)
                {
                    _APIResponse.IsSuccess = false;
                    _APIResponse.Message = SD.CrudTransactionsMessage.RecordExists;
                    return _APIResponse;
                }

                await _IWorker.tbl_Rooms.CreateAync(objRoom);
                await _IWorker.tbl_Rooms.SaveAsync();
                _APIResponse.IsSuccess = true;
                _APIResponse.Message = SD.CrudTransactionsMessage.Save;

                return _APIResponse;
            }
            catch (Exception ex) {

                _APIResponse.IsSuccess = false;
                _APIResponse.Message = ex.Message + " " + SD.SystemMessage.ContactAdmin;
                return _APIResponse;
            }

        }

        [HttpGet]
        public async Task<IEnumerable<Room>> GetAllAsync(Expression<Func<Room, bool>>? filter = null, string? IncludeProperties = null, bool isTracking = false, int pageSize = 0, int pageNumber = 1)
        {
            IEnumerable<Room> objRooms = new List<Room>();

            try
            {
                objRooms = await _IWorker.tbl_Rooms.GetAllAsync(filter,IncludeProperties,isTracking,pageSize,pageNumber);

                if (objRooms != null)
                {
                    return objRooms;
                }
            }
            catch (Exception ex)
            {
                return null!;
            }
            return null!;
        }

        [HttpGet]
        public async Task<Room> GetAsync(int Id)
        {
            Room objRoom = new();

            try
            {

                objRoom = await _IWorker.tbl_Rooms.GetAsync(fw => fw.RoomId == Id);

                if (objRoom != null)
                {
                    return objRoom;
                }

                return null!;

            }
            catch (Exception ex)
            {
                return null!;
            }
           
        }

        public async Task<APIResponse> IsUniqueRoom(string RoomName)
        {
            
            try
            {
                var objRoom = await _IWorker.tbl_Rooms.GetAsync(fw => fw.RoomName.ToLower() == RoomName.ToLower());

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
                _APIResponse.Message =  ex.Message + " " + SD.SystemMessage.ContactAdmin;
                return _APIResponse;
            }
        }

        [HttpPost]
        public async Task<APIResponse> RemoveAsync(int Id)
        {
            Room objRoom = new();

            try
            {
                objRoom = await _IWorker.tbl_Rooms.GetAsync(fw => fw.RoomId == Id);

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
                _APIResponse.IsSuccess= false;
                _APIResponse.Message = ex.Message + " " + SD.SystemMessage.ContactAdmin; 
                return _APIResponse;
            }
        }

        [HttpPost]
        public async Task<APIResponse> UpdateAsync(Room objRoom)
        {
            try
            {
                var is_exists = await _IWorker.tbl_Rooms.GetAsync(fw => fw.RoomId == objRoom.RoomId);

                if (is_exists == null || objRoom == null)
                {
                    _APIResponse.IsSuccess = false;
                    _APIResponse.Message = SD.CrudTransactionsMessage.RecordNotFound;
                    return _APIResponse;
                }

                await _IWorker.tbl_Rooms.UpdateAsync(objRoom);
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
    }
}
