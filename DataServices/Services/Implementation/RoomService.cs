using DataServices.Common.DTO;
using DataServices.Services.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        [HttpPost]
        public async Task<bool> CreateAsync(Room objRoom)
        {
            var is_exists = await _IWorker.tbl_Rooms.GetAsync(fw => fw.RoomName.ToLower() == objRoom.RoomName.ToLower());

            if (is_exists != null || objRoom == null)            
            {
                return false;
            }

            await _IWorker.tbl_Rooms.CreateAync(objRoom);
            await _IWorker.tbl_Rooms.SaveAsync();
            return true;          

        }

        [HttpPost]
        public async Task<bool> DeleteAsync(int Id)
        {

            return false;
        }

        [HttpGet]
        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            IEnumerable<Room> objRooms;

            try
            {

                objRooms = await _IWorker.tbl_Rooms.GetAllAsync();

                if (objRooms != null)
                {
                    return objRooms;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task<Room> GetAsync(int Id)
        {
            Room objRoom;
            try
            {

                objRoom = await _IWorker.tbl_Rooms.GetAsync(fw => fw.RoomId == Id);
                if (objRoom != null)
                {
                    return objRoom;
                }
                return null;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<bool> RemoveAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Room objRoom)
        {
            var is_exists = await _IWorker.tbl_Rooms.GetAsync(fw => fw.RoomId == objRoom.RoomId);

            if (is_exists == null || objRoom == null)
            {
                return false;
            }

            await _IWorker.tbl_Rooms.UpdateAsync(objRoom);
            await _IWorker.tbl_Rooms.SaveAsync();
            return true;          

        }
    }
}
