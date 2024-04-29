using DataServices.Common.DTO;
using DataServices.Common.RepositoryInterface;
using DataServices.Services.Interface;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
            bool recordExists = await _IWorker.tbl_Rooms.AnyAsync(fw => fw.RoomName.ToLower() == objRoom.RoomName.ToLower());

            if(recordExists) 
            {
                return false;
            }            

            if (objRoom != null)
            {
                await _IWorker.tbl_Rooms.AddAsync(objRoom);
                await _IWorker.tbl_Rooms.SaveAsync();
                return true;
            }

            return false;
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
            catch(Exception ex)
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


        public async Task<bool> UpdateAsync(Room objRoom)
        {
            bool isRecordExists = await _IWorker.tbl_Rooms.AnyAsync(fw => fw.RoomId == objRoom.RoomId);

            try
            {

                if (isRecordExists || objRoom != null)
                {
                    await _IWorker.tbl_Rooms.UpdateAsync(objRoom);
                    await _IWorker.tbl_Rooms.SaveAsync();
                    return true;
                }
                return false;

            }

            catch (Exception e)
            {
               
            }
            return false;
        }
    }
}
