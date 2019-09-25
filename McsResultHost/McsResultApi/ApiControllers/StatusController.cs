using Coherent.McsResultHost.McsResultApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coherent.McsResultHost.McsResultApi.ApiControllers {  

    [Route("service-status")]
    public class StatusController : ControllerBase {
    [HttpGet]
    public IActionResult Get() {
      var status = new ServiceStatus {
        Version = "1.0",
        Status = "OK"
      };
      return new ObjectResult(status);
    }
  }
}
