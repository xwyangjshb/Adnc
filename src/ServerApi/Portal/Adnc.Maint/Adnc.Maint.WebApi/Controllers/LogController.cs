﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Adnc.Maint.Application.Services;
using Adnc.Maint.Application.Dtos;
using Adnc.Infr.Common;
using Adnc.Application.Shared.Dtos;
using Adnc.WebApi.Shared;

namespace Adnc.Maint.WebApi.Controllers
{
    /// <summary>
    /// 操作日志
    /// </summary>
    [Route("maint")]
    public class LogController : AdncControllerBase
    {
        private readonly ILogAppService _logService;
        private readonly UserContext _userContext;

        public LogController(ILogAppService logService
            , UserContext userContext)
        {
            _logService = logService;
            _userContext = userContext;
        }

        /// <summary>
        /// 查询操作日志
        /// </summary>
        /// <param name="searchDto">查询条件</param>
        /// <returns></returns>
        [HttpGet("opslogs")]
        [Permission("opsLog")]
        public async Task<ActionResult<PageModelDto<OpsLogDto>>> GetOpsLogsPaged([FromQuery] LogSearchDto searchDto)
        {
            return Result(await _logService.GetOpsLogsPaged(searchDto));
        }

        /// <summary>
        /// 查询用户操作日志
        /// </summary>
        /// <param name="searchDto">查询条件</param>
        /// <returns></returns>
        [HttpGet("users/opslogs")]
        public async Task<ActionResult<PageModelDto<OpsLogDto>>> GetUserOpsLogsPaged(BaseSearchDto searchDto)
        {
            var logSearchDto = new LogSearchDto()
            {
                Account = _userContext.Account,
                PageIndex = searchDto.PageIndex,
                PageSize = searchDto.PageSize
            };
            return Result(await _logService.GetOpsLogsPaged(logSearchDto));
        }

        /// <summary>
        /// 查询登录日志
        /// </summary>
        /// <param name="searchDto">查询条件</param>
        /// <returns></returns>
        [HttpGet("loginlogs")]
        [Permission("loginLog")]
        public async Task<ActionResult<PageModelDto<LoginLogDto>>> GetLoginLogsPaged([FromQuery] LogSearchDto searchDto)
        {
            return Result(await _logService.GetLoginLogsPaged(searchDto));
        }

        /// <summary>
        /// 查询Nlog日志
        /// </summary>
        /// <param name="searchDto">查询条件</param>
        /// <returns></returns>
        [HttpGet("nloglogs")]
        [Permission("nlogLog")]
        public async Task<ActionResult<PageModelDto<NlogLogDto>>> GetNlogLogsPaged([FromQuery] LogSearchDto searchDto)
        {
            return Result(await _logService.GetNlogLogsPaged(searchDto));
        }
    }
}
