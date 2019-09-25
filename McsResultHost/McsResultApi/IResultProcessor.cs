using Coherent.McsResultHost.McsResultApi.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coherent.McsResultHost.McsResultApi {
  public interface IResultProcessor {
    void Process(CollectionResult collectionResult);
  }
}
