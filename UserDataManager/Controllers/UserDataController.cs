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
        private IUserDataCRUDServices<UserData.UserDataResponse, UserDataDTO, UserDataInsertDTO, UserData.Address, AdressDataInsertDTO> _userDataCRUDServices;

        public UserDataController( IUserDataClientServices userDataClientServices, IUserDataCRUDServices<UserData.UserDataResponse, UserDataDTO, UserDataInsertDTO, UserData.Address, AdressDataInsertDTO> userDataCRUDServices)
        {
            _userDataClientServices = userDataClientServices;
            _userDataCRUDServices = userDataCRUDServices;
        }

        [HttpPost("DownloadUserDataClient")]
        public async Task<ActionResult<UserData.UserDataResponse>> SetUserDataClient()
        {
            var userDataResponse = await _userDataClientServices.SetUserDataClient();
            var userDataResult = await _userDataCRUDServices.AsignDataClient(userDataResponse);

            if (userDataResult == null)
            {
                return NotFound();
            }

            return StatusCode(201, userDataResult);
        }
        [HttpPost("InsertUserData")]
        public async Task<ActionResult<UserData.UserDataResponse>> InsertUserData(UserDataInsertDTO UserDataInsertDTO)
        {
            var userDataResponse = await _userDataCRUDServices.AddUserData(UserDataInsertDTO);
        
            if (userDataResponse == null)
            {
                return NotFound();
            }
        
            return StatusCode(201, userDataResponse);
        }
        [HttpPost("InsertAdressData")]
        public async Task<ActionResult<UserData.Address>> InsertAdressData(AdressDataInsertDTO adressDataInsertDTO)
        {
            var adressDataResponse = await _userDataCRUDServices.AddAdressData(adressDataInsertDTO);
        
            if (adressDataResponse == null)
            {
                return NotFound();
            }
        
            return StatusCode(201, adressDataResponse);
        }
        [HttpGet("ReadAllUserDataList")]
        public async Task<ActionResult<IEnumerable<UserDataDTO>>> ReadAllUserDataList()
        {
            var userDataReadDTO = await _userDataCRUDServices.ReadAllUserDataList();

            if (userDataReadDTO == null)
            {
                return NotFound();
            }

            return Ok(userDataReadDTO);
        }
        [HttpGet("ReadUserData/{id}")]
        public async Task<ActionResult<UserDataDTO>> ReadUserDataList(int id)
        {
            var userData = await _userDataCRUDServices.ReadUserData(id);
            if (userData == null) 
            {
                NotFound();
            }

            return Ok(userData);
        }
        [HttpPut("UpdateUserData")]
        public async Task<ActionResult<UserDataDTO>> UpdateUserData(UserDataDTO userDataReadDTO)
        {
            var userData = await _userDataCRUDServices.UpdateUserData(userDataReadDTO);

            if (userData == null)
            {
                NotFound();
            }

            return Ok(userData);
        }
        [HttpDelete("RemoveUserData/{id}")]
        public async Task<ActionResult> RemoveUserData(int id)
        {
            var userData = _userDataCRUDServices.RemoveData(id);
            if (userData == null)
            {
                NotFound();
            }

            return NoContent();
        }
        [HttpDelete("RemoveAdressData/{id}")]
        public async Task<ActionResult> RemoveAdressData(int id)
        {
            var adressData = _userDataCRUDServices.RemoveOtherData(id);
            if (adressData == null)
            {
                NotFound();
            }

            return Ok();
        }
        [HttpDelete("ClearAllUserData")]
        public async Task<ActionResult> ClearAllUserDataClient()
        {
            bool isClearData = await _userDataCRUDServices.ClearAllUserDataClient();
            
            if (isClearData) 
            {
                return Ok();
            }

            return NoContent();
        }
    }
}
