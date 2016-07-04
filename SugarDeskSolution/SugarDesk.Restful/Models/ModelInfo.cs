using System;
using System.Collections.Generic;

namespace SugarDesk.Restful.Models
{
    public class ModelInfo
    {
        public string ModelName { get; set; }
        public Type Type { get; set; }
        public List<ModelProperty> ModelProperties { get; set; }
    }
}
