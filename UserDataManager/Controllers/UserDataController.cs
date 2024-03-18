using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Text.Json;
using UserDataManager.EntityFramework.Context;
using UserDataManager.EntityFramework.DTO;
using UserDataManager.EntityFramework.Models;
using UserDataManager.Services;
using UserDataManager.Services.Interface;

namespace UserDataManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDataController : ControllerBase
    {
        private IUserDataClientServices _userDataClientServices;
        private IDataInsertServices<UserData.UserDataResponse, UserDataInsertDTO, UserData.Address, AdressDataInsertDTO> _userDataInsertServices;
        private IReadDataServices<UserDataDTO> _readDataServices;
        private IDataUpdateServices<UserDataDTO> _dataUpdateServices;
        private IDataDeleteServices _dataDeleteServices;

        public UserDataController( IUserDataClientServices userDataClientServices, IDataInsertServices<UserData.UserDataResponse, UserDataInsertDTO, UserData.Address, AdressDataInsertDTO> userDataCRUDServices,
            IReadDataServices<UserDataDTO> readDataServices,
            IDataUpdateServices<UserDataDTO> dataUpdateServices,
            IDataDeleteServices dataDeleteServices
            )
        {
            _userDataClientServices = userDataClientServices;
            _userDataInsertServices = userDataCRUDServices;
            _readDataServices = readDataServices;
            _dataUpdateServices = dataUpdateServices;
            _dataDeleteServices = dataDeleteServices;
        }

        [HttpPost("DownloadUserDataClient")]
        public async Task<ActionResult<UserData.UserDataResponse>> SetUserDataClient()
        {
            var userDataResponse = await _userDataClientServices.SetUserDataClient();
            var userDataResult = await _userDataInsertServices.AsignDataClient(userDataResponse);

            if (userDataResult == null)
            {
                return NotFound();
            }

            return StatusCode(201, userDataResult);
        }
        [HttpPost("InsertUserData")]
        public async Task<ActionResult<UserData.UserDataResponse>> InsertUserData(UserDataInsertDTO UserDataInsertDTO)
        {
            var userDataResponse = await _userDataInsertServices.AddUserData(UserDataInsertDTO);
        
            if (userDataResponse == null)
            {
                return NotFound();
            }
        
            return StatusCode(201, userDataResponse);
        }
        [HttpPost("InsertAdressData")]
        public async Task<ActionResult<UserData.Address>> InsertAdressData(AdressDataInsertDTO adressDataInsertDTO)
        {
            var adressDataResponse = await _userDataInsertServices.AddAdressData(adressDataInsertDTO);
        
            if (adressDataResponse == null)
            {
                return NotFound();
            }
        
            return StatusCode(201, adressDataResponse);
        }
        [HttpGet("ReadAllUserDataList")]
        public async Task<ActionResult<IEnumerable<UserDataDTO>>> ReadAllUserDataList()
        {
            var userDataReadDTO = await _readDataServices.ReadAllUserDataList();

            if (userDataReadDTO == null)
            {
                return NotFound();
            }

            return Ok(userDataReadDTO);
        }
        [HttpGet("ReadUserData/{id}")]
        public async Task<ActionResult<UserDataDTO>> ReadUserDataList(int id)
        {
            var userData = await _readDataServices.ReadUserData(id);
            if (userData == null) 
            {
                NotFound();
            }

            return Ok(userData);
        }
        [HttpPut("UpdateUserData")]
        public async Task<ActionResult<UserDataDTO>> UpdateUserData(UserDataDTO userDataReadDTO)
        {
            var userData = await _dataUpdateServices.UpdateUserData(userDataReadDTO);

            if (userData == null)
            {
                NotFound();
            }

            return Ok(userData);
        }
        [HttpDelete("RemoveUserData/{id}")]
        public async Task<ActionResult> RemoveUserData(int id)
        {
            var userData = _dataDeleteServices.RemoveData(id);
            if (userData == null)
            {
                NotFound();
            }

            return NoContent();
        }
        [HttpDelete("RemoveAdressData/{id}")]
        public async Task<ActionResult> RemoveAdressData(int id)
        {
            var adressData = _dataDeleteServices.RemoveOtherData(id);
            if (adressData == null)
            {
                NotFound();
            }

            return Ok();
        }
        [HttpDelete("ClearAllUserData")]
        public async Task<ActionResult> ClearAllUserDataClient()
        {
            bool isClearData = await _dataDeleteServices.ClearAllUserDataClient();
            
            if (isClearData) 
            {
                return Ok();
            }

            return NoContent();
        }
    }
}
