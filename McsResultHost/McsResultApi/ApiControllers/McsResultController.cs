using Coherent.McsResultHost.McsResultApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coherent.McsResultHost.McsResultApi.ApiControllers {
  public class McsResultController : ControllerBase {
    public McsResultController(IResultProcessor processor) {
      this.processor = processor;
    }
    [HttpPost]
    [Route("collection-result")]
    public IActionResult CollectionRequest([FromBody]CollectionResult collectionResult) {
      if (collectionResult == null) throw new ArgumentNullException("collectionResult");
      processor.Process(collectionResult);
      return new OkResult();
    }
    IResultProcessor processor;
  }
}
